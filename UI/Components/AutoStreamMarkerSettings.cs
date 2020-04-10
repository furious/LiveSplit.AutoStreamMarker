using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using LiveSplit.Web;
using System.IO;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace LiveSplit.UI.Components
{
    public partial class AutoStreamMarkerSettings : UserControl
    {
        public HttpListener Listener;
        public string TwitchClientID = "1202m5dzaxw5ohdw0r39ddfpuvcfdd";
        public string TwitchOAuth { get; set; }
        public string Channel { get; set; }
        public bool Notify { get; set; }
        public bool MarkEverySplit { get; set; }
        public bool MarkResets { get; set; }


        public AutoStreamMarkerSettings()
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
                System.Diagnostics.Process.Start("https://id.twitch.tv/oauth2/authorize?response_type=token&redirect_uri=http://localhost:45000/code&scope=user:edit:broadcast&force_verify=true&client_id=" + TwitchClientID);

                Listener = new HttpListener();
                Listener.Prefixes.Add("http://*:45000/");
                Listener.Start();
                Listen();
        }
       
        private void FetchUser()
        {
            try
            {
                using (WebClient Web = new WebClient())
                {
                    //Web.Headers.Add("Client-ID", TwitchClientID);
                    Web.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");
                    Web.Headers.Add("Authorization", "Bearer " + TwitchOAuth);
                    Console.WriteLine(Web.Headers.Get("Authorization"));
                    
                    dynamic channel = JSON.FromString(Web.DownloadString("https://api.twitch.tv/helix/users"));
                    if (channel.data is List<Object> && channel.data.Count > 0)
                    {
                        Avatar.ImageLocation = channel.data[0].profile_image_url;
                        Username.Text = String.Format("User: {0}", channel.data[0].display_name);
                        /*dynamic editors = JSON.FromString(Web.DownloadString(String.Format("https://api.twitch.tv/v5/permissions/channels/{0}/editable_channels", channel._id)));

                        Channels.Items.Clear();
                        Channels.Items.Add(channel.name);
                        if (!Object.ReferenceEquals(null, editors.editable_channels)) foreach (dynamic c in editors.editable_channels)
                            {
                                Channels.Items.Add(c.login);
                            }
                        Channels.SelectedIndex = 0;*/
                    }
                    Web.Dispose();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
       
        private async void Listen()
        {
            bool listen = true;
            while (listen)
            {
                var context = await Listener.GetContextAsync();
                Console.WriteLine("Response" + context.Request.Url.AbsolutePath);

                //dynamic channel = JSON.FromString(Web.DownloadString("https://api.twitch.tv/kraken/channel"));

                String response = "";

                context.Response.StatusCode = 200;
                if (context.Request.Url.AbsolutePath == "/code")
                {
                    response = "<html><body onload=\"document.location.href = document.location.hash.replace('#','/token?');\"></body></html>";
                }
                else if (context.Request.Url.AbsolutePath == "/token")
                {
                    response = "<html style='display:table; width:100%; height:100%'><body style='display:table-cell; vertical-align:middle; text-align:center; font-family:Sans-serif'><h1>Done, you can close this window now...</h1></body></html>";
                    TwitchOAuth = context.Request.QueryString.Get("access_token");
                    FetchUser();
                    listen = false;
                }

                context.Response.ContentLength64 = response.Length;
                context.Response.OutputStream.Write(Encoding.UTF8.GetBytes(response), 0, response.Length);
                context.Response.OutputStream.Close();
            }
            Listener.Stop();
        }
    }
}
