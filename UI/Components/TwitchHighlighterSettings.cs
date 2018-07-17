using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using LiveSplit.Web;
using System.IO;

namespace LiveSplit.UI.Components
{
    public partial class TwitchHighlighterSettings : UserControl
    {
        public string TwitchClientID = "6arow4uftfxfu50giby5wx15l0r1x2";
        public string TwitchOAuth { get; set; }
        public string Channel { get; set; }
        public bool Notify { get; set; }
        public bool MarkEverySplit { get; set; }
        public bool MarkResets { get; set; }


        public TwitchHighlighterSettings()
        {
            InitializeComponent();

            TwitchOAuth =
            Channel = "";
            Notify =
            MarkEverySplit =
            MarkResets = true;
            
            chkNotify.DataBindings.Add("Checked", this, "Notify");
            chkMarkEverySplit.DataBindings.Add("Checked", this, "MarkEverySplit");
            chkMarkResets.DataBindings.Add("Checked", this, "MarkResets");

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            TwitchOAuth = SettingsHelper.ParseString(element["TwitchOAuth"]);
            Channel = SettingsHelper.ParseString(element["Channel"]);
            Notify = SettingsHelper.ParseBool(element["Notify"]);
            MarkEverySplit = SettingsHelper.ParseBool(element["MarkEverySplit"]);
            MarkResets = SettingsHelper.ParseBool(element["MarkResets"]);

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
            SettingsHelper.CreateSetting(document, parent, "Notify", Notify) ^
            SettingsHelper.CreateSetting(document, parent, "MarkEverySplit", MarkEverySplit) ^
            SettingsHelper.CreateSetting(document, parent, "MarkResets", MarkResets);
        }

        private void ShowLogin(object sender, EventArgs e)
        {
            if(tabc_th.SelectedTab == tab_th_login)
            {
                Browser.Show();
                Browser.Navigate("https://id.twitch.tv/oauth2/authorize?response_type=token&redirect_uri=https://dev.twitch.tv/login-callback&scope=channel_editor+chat_login+user_read&login_type=login&force_verify=true&client_id=" + TwitchClientID);
            }

        }
       
        private void FetchUser()
        {
            using (WebClient Web = new WebClient())
            {
                Web.Headers.Add("Client-ID", TwitchClientID);
                Web.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");
                Web.Headers.Add("Authorization", "OAuth " + TwitchOAuth);
                dynamic channel = JSON.FromString(Web.DownloadString("https://api.twitch.tv/kraken/channel"));
                if (!String.IsNullOrEmpty(channel.name))
                {
                    Avatar.ImageLocation = channel.logo;
                    Username.Text = String.Format("User: {0}", channel.display_name);
                    dynamic editors = JSON.FromString(Web.DownloadString(String.Format("https://api.twitch.tv/v5/permissions/channels/{0}/editable_channels", channel._id)));

                    Channels.Items.Clear();
                    Channels.Items.Add(channel.name);
                    if (!Object.ReferenceEquals(null, editors.editable_channels)) foreach(dynamic c in editors.editable_channels)
                    {
                        Channels.Items.Add(c.login);
                    }
                    Channels.SelectedIndex = 0;
                }
                Web.Dispose();
            }
        }

        private void Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

            WebBrowser browser = ((WebBrowser) sender);
            Match match = Regex.Match(browser.Url.Fragment, @"access_token=([^\&]+)");
            if (match.Success)
            {
                TwitchOAuth = match.Groups[1].Value;
                FetchUser();
                tabc_th.SelectedTab = tab_th_settings;
            }
        }

        private void TwitchChannel(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://twitch.tv/furious");
        }
    }
}
