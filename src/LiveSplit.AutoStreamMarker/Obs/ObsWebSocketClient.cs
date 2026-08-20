using LiveSplit.Web;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveSplit.UI.Components
{
    /// <summary>
    /// Minimal obs-websocket v5 client, just enough to identify/authenticate
    /// and issue requests such as "CreateRecordChapter".
    /// See https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md
    /// </summary>
    public class ObsWebSocketClient : IDisposable
    {
        private const int OpHello = 0;
        private const int OpIdentify = 1;
        private const int OpIdentified = 2;
        private const int OpRequest = 6;
        private const int OpRequestResponse = 7;

        private const int RpcVersion = 1;
        private static readonly TimeSpan IdentifyTimeout = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(5);

        private readonly SemaphoreSlim ConnectLock = new SemaphoreSlim(1, 1);
        private readonly ConcurrentDictionary<string, TaskCompletionSource<dynamic>> PendingRequests =
            new ConcurrentDictionary<string, TaskCompletionSource<dynamic>>();

        private ClientWebSocket Socket;
        private CancellationTokenSource Cts;
        private TaskCompletionSource<bool> IdentifiedTcs;
        private string ConnectedUrl;
        private string ConnectedPassword;

        public bool IsConnected => Socket != null && Socket.State == WebSocketState.Open;

        /// <summary>
        /// Connects (or reconnects, if the url/password changed) and waits until
        /// the session has been identified with the server.
        /// </summary>
        public async Task EnsureConnectedAsync(string url, string password)
        {
            if (IsConnected && ConnectedUrl == url && ConnectedPassword == password)
            {
                return;
            }

            await ConnectLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (IsConnected && ConnectedUrl == url && ConnectedPassword == password)
                {
                    return;
                }

                Reset();

                Socket = new ClientWebSocket();
                Cts = new CancellationTokenSource();
                IdentifiedTcs = new TaskCompletionSource<bool>();

                await Socket.ConnectAsync(new Uri(url), Cts.Token).ConfigureAwait(false);

                var receiveLoop = ReceiveLoopAsync(password, Cts.Token);

                var completed = await Task.WhenAny(IdentifiedTcs.Task, Task.Delay(IdentifyTimeout, Cts.Token)).ConfigureAwait(false);
                if (completed != IdentifiedTcs.Task)
                {
                    throw new TimeoutException("Timed out waiting for OBS WebSocket to identify.");
                }

                await IdentifiedTcs.Task.ConfigureAwait(false);

                ConnectedUrl = url;
                ConnectedPassword = password;

                // Observe the background receive loop's exceptions without blocking on it.
                _ = receiveLoop.ContinueWith(t => Console.WriteLine(t.Exception?.Flatten().InnerException?.Message),
                    TaskContinuationOptions.OnlyOnFaulted);
            }
            catch
            {
                Reset();
                throw;
            }
            finally
            {
                ConnectLock.Release();
            }
        }

        public Task CreateRecordChapterAsync(string chapterName)
        {
            dynamic requestData = new DynamicJsonObject();
            requestData.chapterName = chapterName;
            return SendRequestAsync("CreateRecordChapter", requestData);
        }

        private async Task SendRequestAsync(string requestType, dynamic requestData)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Not connected to OBS WebSocket.");
            }

            string requestId = Guid.NewGuid().ToString();

            dynamic message = new DynamicJsonObject();
            message.op = OpRequest;

            dynamic d = new DynamicJsonObject();
            d.requestType = requestType;
            d.requestId = requestId;
            d.requestData = requestData;
            message.d = d;

            var tcs = new TaskCompletionSource<dynamic>();
            PendingRequests[requestId] = tcs;

            try
            {
                await SendAsync(message.ToString(), Cts.Token).ConfigureAwait(false);

                var completed = await Task.WhenAny(tcs.Task, Task.Delay(RequestTimeout)).ConfigureAwait(false);
                if (completed != tcs.Task)
                {
                    throw new TimeoutException($"Timed out waiting for a response to \"{requestType}\".");
                }

                dynamic response = await tcs.Task.ConfigureAwait(false);
                dynamic status = response.requestStatus;
                bool result = status != null && status.result != null && (bool)status.result;
                if (!result)
                {
                    string comment = (status != null && status.comment != null) ? (string)status.comment : "unknown error";
                    throw new Exception($"OBS request \"{requestType}\" failed: {comment}");
                }
            }
            finally
            {
                PendingRequests.TryRemove(requestId, out _);
            }
        }

        private async Task ReceiveLoopAsync(string password, CancellationToken token)
        {
            var buffer = new byte[8192];
            try
            {
                while (!token.IsCancellationRequested && Socket.State == WebSocketState.Open)
                {
                    string json = await ReceiveMessageAsync(buffer, token).ConfigureAwait(false);
                    if (json == null)
                    {
                        break;
                    }

                    dynamic message = JSON.FromString(json);
                    int op = Convert.ToInt32(message.op);

                    switch (op)
                    {
                        case OpHello:
                            await HandleHelloAsync(message.d, password, token).ConfigureAwait(false);
                            break;
                        case OpIdentified:
                            IdentifiedTcs?.TrySetResult(true);
                            break;
                        case OpRequestResponse:
                            HandleRequestResponse(message.d);
                            break;
                    }
                }
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                // Expected on Dispose()/reconnect.
            }
            finally
            {
                IdentifiedTcs?.TrySetException(new Exception("OBS WebSocket connection closed before it could be identified."));
                foreach (var pending in PendingRequests.Values)
                {
                    pending.TrySetException(new Exception("OBS WebSocket connection closed."));
                }
            }
        }

        private async Task<string> ReceiveMessageAsync(byte[] buffer, CancellationToken token)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), token).ConfigureAwait(false);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        return null;
                    }
                    stream.Write(buffer, 0, result.Count);
                } while (!result.EndOfMessage);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private async Task HandleHelloAsync(dynamic hello, string password, CancellationToken token)
        {
            dynamic identify = new DynamicJsonObject();
            identify.rpcVersion = RpcVersion;
            identify.eventSubscriptions = 0;

            if (hello.authentication != null)
            {
                string challenge = hello.authentication.challenge;
                string salt = hello.authentication.salt;
                identify.authentication = CreateAuthenticationString(password ?? "", salt, challenge);
            }

            dynamic message = new DynamicJsonObject();
            message.op = OpIdentify;
            message.d = identify;

            await SendAsync(message.ToString(), token).ConfigureAwait(false);
        }

        private void HandleRequestResponse(dynamic d)
        {
            string requestId = d.requestId;
            if (requestId != null && PendingRequests.TryGetValue(requestId, out var tcs))
            {
                tcs.TrySetResult(d);
            }
        }

        private static string CreateAuthenticationString(string password, string salt, string challenge)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] secretHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                string secretBase64 = Convert.ToBase64String(secretHash);

                byte[] authHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(secretBase64 + challenge));
                return Convert.ToBase64String(authHash);
            }
        }

        private Task SendAsync(string json, CancellationToken token)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            return Socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, token);
        }

        public async Task DisconnectAsync()
        {
            if (Socket != null && Socket.State == WebSocketState.Open)
            {
                try
                {
                    await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).ConfigureAwait(false);
                }
                catch
                {
                    // Best-effort close.
                }
            }
            Reset();
        }

        private void Reset()
        {
            try { Cts?.Cancel(); } catch { /* ignore */ }
            Cts?.Dispose();
            Cts = null;

            Socket?.Dispose();
            Socket = null;

            ConnectedUrl = null;
            ConnectedPassword = null;
        }

        public void Dispose()
        {
            Reset();
        }
    }
}
