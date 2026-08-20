using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using LiveSplit.Web;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;

namespace LiveSplit.UI.Components
{
    public partial class AutoStreamMarkerSettings : UserControl
    {
        public const string DefaultObsUrl = "ws://127.0.0.1:4455";
        public const string TwitchClientID = "1202m5dzaxw5ohdw0r39ddfpuvcfdd";

        bool Listening = true;
        public string TwitchOAuth { get; set; }
        public bool MarkEverySplit { get; set; }
        public bool MarkResets { get; set; }
        public bool WarnOffline { get; set; }
        public bool ObsEnabled { get; set; }
        public string ObsUrl { get; set; }
        public string ObsPassword { get; set; }
        public bool LogEnabled { get; set; }
        public string LogFolder { get; set; }

        private WebClient Web { get; set; }
        private ObsWebSocketClient ObsTestClient { get; set; }

        public AutoStreamMarkerSettings()
        {
            InitializeComponent();

            TwitchOAuth = "";
            MarkEverySplit =
            MarkResets =
            WarnOffline = true;

            ObsEnabled = false;
            ObsUrl = DefaultObsUrl;
            ObsPassword = "";

            LogEnabled = false;
            LogFolder = "";

            Web = new WebClient();
            Web.Headers.Add("Client-ID", TwitchClientID);
            Web.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");
            //Web.Encoding = Encoding.UTF8;

            chkMarkEverySplit.DataBindings.Add("Checked", this, "MarkEverySplit");
            chkMarkResets.DataBindings.Add("Checked", this, "MarkResets");
            chkWarnOffline.DataBindings.Add("Checked", this, "WarnOffline");
            chkObsEnabled.DataBindings.Add("Checked", this, "ObsEnabled");
            txtObsUrl.DataBindings.Add("Text", this, "ObsUrl");
            txtObsPassword.DataBindings.Add("Text", this, "ObsPassword");
            chkLogEnabled.DataBindings.Add("Checked", this, "LogEnabled");
            txtLogFolder.DataBindings.Add("Text", this, "LogFolder");

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            TwitchOAuth = SettingsHelper.ParseString(element["TwitchOAuth"]);
            MarkEverySplit = SettingsHelper.ParseBool(element["MarkEverySplit"]);
            MarkResets = SettingsHelper.ParseBool(element["MarkResets"]);
            WarnOffline = element["WarnOffline"] == null || SettingsHelper.ParseBool(element["WarnOffline"]);
            ObsEnabled = element["ObsEnabled"] != null && SettingsHelper.ParseBool(element["ObsEnabled"]);
            ObsUrl = element["ObsUrl"] != null ? SettingsHelper.ParseString(element["ObsUrl"]) : DefaultObsUrl;
            ObsPassword = element["ObsPassword"] != null ? SettingsHelper.ParseString(element["ObsPassword"]) : "";
            LogEnabled = element["LogEnabled"] != null && SettingsHelper.ParseBool(element["LogEnabled"]);
            LogFolder = element["LogFolder"] != null ? SettingsHelper.ParseString(element["LogFolder"]) : "";

            if (!String.IsNullOrEmpty(TwitchOAuth))
            {
                FetchUser();
            }
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0") ^
            SettingsHelper.CreateSetting(document, parent, "TwitchOAuth", TwitchOAuth) ^
            SettingsHelper.CreateSetting(document, parent, "MarkEverySplit", MarkEverySplit) ^
            SettingsHelper.CreateSetting(document, parent, "MarkResets", MarkResets) ^
            SettingsHelper.CreateSetting(document, parent, "WarnOffline", WarnOffline) ^
            SettingsHelper.CreateSetting(document, parent, "ObsEnabled", ObsEnabled) ^
            SettingsHelper.CreateSetting(document, parent, "ObsUrl", ObsUrl) ^
            SettingsHelper.CreateSetting(document, parent, "ObsPassword", ObsPassword) ^
            SettingsHelper.CreateSetting(document, parent, "LogEnabled", LogEnabled) ^
            SettingsHelper.CreateSetting(document, parent, "LogFolder", LogFolder);
        }

        private async void TestObsConnection(object sender, EventArgs e)
        {
            btnObsTest.Enabled = false;
            btnObsTest.Text = "Connecting...";
            try
            {
                if (ObsTestClient == null)
                {
                    ObsTestClient = new ObsWebSocketClient();
                }

                await ObsTestClient.EnsureConnectedAsync(ObsUrl, ObsPassword);
                btnObsTest.Text = "Connected!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                btnObsTest.Text = "Connection failed";
                MessageBox.Show(
                    "Could not connect to OBS WebSocket:\n" + ex.Message,
                    "Auto Stream Marker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnObsTest.Enabled = true;
            }
        }

        private void BrowseLogFolder(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder to save stream session logs to.";
                if (!String.IsNullOrEmpty(LogFolder))
                {
                    dialog.SelectedPath = LogFolder;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtLogFolder.Text = dialog.SelectedPath;
                }
            }
        }

        private void ShowLogin(object sender, EventArgs e)
        {
                System.Diagnostics.Process.Start("https://id.twitch.tv/oauth2/authorize?response_type=token&redirect_uri=http://localhost:45000/code&scope=user:edit:broadcast&force_verify=true&client_id=" + TwitchClientID);

                Listen();
        }

        private void FetchUser()
        {
            try
            {
                Web.Headers["Authorization"] = "Bearer " + TwitchOAuth;
                Console.WriteLine(Web.Headers.Get("Authorization"));

                dynamic channel = JSON.FromString(Web.DownloadString("https://api.twitch.tv/helix/users"));
                if (channel.data != null && channel.data.Count > 0)
                {
                    Avatar.ImageLocation = channel.data[0].profile_image_url;
                    Username.Text = String.Format("User: {0}", channel.data[0].display_name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async void Listen()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 45000);
            listener.Start();
            Listening = true;
            while (Listening)
            {
                Socket sock = listener.AcceptSocket();
                Thread.Sleep(100);
                byte[] buffer = new byte[32];
                String response = "", request = "", status = "200 OK";
                while (sock.Available > 0)
                {
                    int bytes = sock.Receive(buffer);
                    request += Encoding.ASCII.GetString(buffer, 0, bytes);
                }

                if (request.Contains("GET /code"))
                {
                    response = "<html><body onload=\"document.location.href = document.location.hash.replace('#','/token?');\"></body></html>";
                }
                else if (request.Contains("GET /token"))
                {
                    TwitchOAuth = Regex.Match(request, "access_token=([^&]+)").Groups[1]?.Value ?? "";
                    response = "<html style='display:table; width:100%; height:100%'><body style='display:table-cell; vertical-align:middle; text-align:center; font-family:Sans-serif'><h1>Done, you can close this window now...</h1></body></html>";
                    FetchUser();
                    Listening = false;
                }
                else
                {
                    status = "404 Not Found";
                    response = "Not Found";
                }

                sock.Send(Encoding.ASCII.GetBytes(
                    $"HTTP/1.1 {status}\r\n" +
                    "Content-Type: text/html\r\n" +
                    "Content-Length: " + response.Length.ToString() + "\r\n\n" +
                    response
                ));
                sock.Shutdown(SocketShutdown.Both);
                sock.Close();
            }
            listener.Stop();
        }
    }
}
