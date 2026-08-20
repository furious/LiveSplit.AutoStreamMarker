using LiveSplit.Model;
using LiveSplit.Options;
using LiveSplit.Web;
using LiveSplit.Web.Share;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;


namespace LiveSplit.UI.Components
{
    public class AutoStreamMarkerComponent : LogicComponent, IDeactivatableComponent
    {
        public override string ComponentName => "Auto Stream Marker";

        public bool Activated { get; set; }
        public bool ShowLoginInfo { get; set; }
        public bool ShowStreamInfo { get; set; }
        public bool ShowObsInfo { get; set; }
        public bool ShowLogInfo { get; set; }

        private LiveSplitState State { get; set; }
        private DynamicJsonConverter Converter { get; set; }
        private AutoStreamMarkerSettings Settings { get; set; }
        private NotifyIcon Notification { get; set; }
        private ObsWebSocketClient Obs { get; set; }
        private StreamSessionLogger SessionLogger { get; set; }
        private StreamSessionLogger RecordFallbackLogger { get; set; }
        public WebClient Web { get; set; }
        public String Action { get; set; }
        public dynamic User { get; set; }
        public dynamic Stream { get; set; }

        public AutoStreamMarkerComponent(LiveSplitState state)
        {
            Activated = true;
            ShowLoginInfo = true;
            ShowStreamInfo = true;
            ShowObsInfo = true;
            ShowLogInfo = true;

            State = state;
            Settings = new AutoStreamMarkerSettings();
            Obs = new ObsWebSocketClient();
            SessionLogger = new StreamSessionLogger();
            RecordFallbackLogger = new StreamSessionLogger();

            Notification = new NotifyIcon
            {
                Icon = System.Drawing.SystemIcons.Information,
                Visible = true,
                BalloonTipTitle = "Auto Stream Marker"
            };

            State.OnStart += State_OnStart;
            State.OnSplit += State_OnSplit;
            State.OnReset += State_OnReset;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            Web = new WebClient();
            Web.Headers.Add("Client-ID", AutoStreamMarkerSettings.TwitchClientID);
            Web.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");
            Web.Encoding = Encoding.UTF8;
        }

        public override void Dispose()
        {
            State.OnStart -= State_OnStart;
            State.OnSplit -= State_OnSplit;
            State.OnReset -= State_OnReset;
            Web.Dispose();
            Obs.Dispose();
            Notification.Dispose();
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public override void SetSettings(XmlNode settings)
        {
            ShowLoginInfo =
            ShowStreamInfo = true;
            Settings.SetSettings(settings);
        }

        private void State_OnStart(object sender, EventArgs e)
        {
            Mark("started");
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended)
            {
                Mark("finished");
            }
            else if(State.CurrentPhase == TimerPhase.Running && Settings.MarkEverySplit)
            {
                Mark(String.Format("split \"{0}\"", State.CurrentSplit.Name));
            }
        }

        private void State_OnReset(object sender, TimerPhase e)
        {
            if (e != TimerPhase.Ended && Settings.MarkResets)
            {
                Mark("reseted");
            }
        }
        private void Notify(String message)
        {
            if (!Settings.NotificationsEnabled)
            {
                return;
            }

            Notification.BalloonTipText = message;
            Notification.ShowBalloonTip(5000);
        }

        private void Mark(string action)
        {
            Action = String.Format(
                "Run #{0} {1}: {2} - {3}",
                State.Run.AttemptCount, action,
                String.IsNullOrEmpty(State.Run.GameName) ? "No Game" : State.Run.GameName,
                String.IsNullOrEmpty(State.Run.CategoryName) ? "No Category" : State.Run.CategoryName
            );

            Task.Run(() => StreamMarker(Action));

            if (Settings.ObsEnabled)
            {
                Task.Run(() => TriggerObsChapterAsync(Action));
            }

            if (Settings.LogEnabled)
            {
                Task.Run(() => TriggerStreamLogAsync(Action));
            }
        }

        private void StreamMarker(string action)
        {
            try
            {
                Web.Headers["Authorization"] = "Bearer " + Settings.TwitchOAuth;
                HandleUser(Web.DownloadString(new Uri("https://api.twitch.tv/helix/users")));
                Console.WriteLine(action);
            }
            catch (WebException ex)
            {
                if (ShowLoginInfo)
                {
                    ShowLoginInfo = false;
                    Notify("Your need to login with your Twitch account in the Auto Stream Marker layout settings...");
                }
                Console.WriteLine(ex.Message);
            }
        }

        private async Task TriggerObsChapterAsync(string chapterName)
        {
            try
            {
                await Obs.EnsureConnectedAsync(Settings.ObsUrl, Settings.ObsPassword);

                if (await Obs.RecordingSupportsChaptersAsync())
                {
                    await Obs.CreateRecordChapterAsync(chapterName);
                    Console.WriteLine("OBS chapter marker created: " + chapterName);
                }
                else if (!String.IsNullOrEmpty(Settings.LogFolder))
                {
                    dynamic recordStatus = await Obs.GetRecordStatusAsync();
                    bool isRecording = recordStatus != null && recordStatus.outputActive != null && (bool)recordStatus.outputActive;
                    double durationMs = (recordStatus != null && recordStatus.outputDuration != null) ? Convert.ToDouble(recordStatus.outputDuration) : 0;
                    RecordFallbackLogger.AppendMark(isRecording, durationMs, Settings.LogFolder, chapterName);
                    Console.WriteLine("OBS recording format doesn't support chapters, wrote mark to file instead: " + chapterName);
                }
                else
                {
                    Console.WriteLine("OBS recording format doesn't support chapters and no log folder is configured, mark not saved: " + chapterName);
                }

                ShowObsInfo = true;
            }
            catch (Exception ex)
            {
                if (ShowObsInfo)
                {
                    ShowObsInfo = false;
                    Notify("Could not create an OBS chapter marker. Check your OBS WebSocket settings...");
                }
                Console.WriteLine(ex.Message);
            }
        }

        private async Task TriggerStreamLogAsync(string description)
        {
            try
            {
                await Obs.EnsureConnectedAsync(Settings.ObsUrl, Settings.ObsPassword);
                dynamic status = await Obs.GetStreamStatusAsync();
                bool isActive = status != null && status.outputActive != null && (bool)status.outputActive;
                double durationMs = (status != null && status.outputDuration != null) ? Convert.ToDouble(status.outputDuration) : 0;
                SessionLogger.AppendMark(isActive, durationMs, Settings.LogFolder, description);
                ShowLogInfo = true;
            }
            catch (Exception ex)
            {
                if (ShowLogInfo)
                {
                    ShowLogInfo = false;
                    Notify("Could not log the stream session. Check your OBS WebSocket and log folder settings...");
                }
                Console.WriteLine(ex.Message);
            }
        }

        private void HandleUser(String data)
        {
            try
            {
                User = JSON.FromString(data);
                Console.WriteLine(data);

                if (User.data != null && User.data.Count > 0)
                {
                    ShowLoginInfo = true;
                    HandleStream(Web.DownloadString(new Uri(String.Format("https://api.twitch.tv/helix/streams?user_id={0}", User.data[0].id))));
                    return;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void HandleStream(String data)
        {
            try
            {
                Stream = JSON.FromString(data);
                if (Stream.data != null && Stream.data.Count > 0 && String.Equals(Stream.data[0].type, "live"))
                {
                    ShowStreamInfo = true;
                    NameValueCollection values = new NameValueCollection
                    {
                        { "user_id", User.data[0].id },
                        { "description", Action }
                    };
                    Console.WriteLine(Web.UploadValues(new Uri("https://api.twitch.tv/helix/streams/markers"), "POST", values));
                }
                else if (ShowStreamInfo)
                {
                    ShowStreamInfo = false;
                    Notify("Your channel isn't live! Start your stream to auto mark the runs in your VODs...");
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}
