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
        
        private LiveSplitState State { get; set; }
        private DynamicJsonConverter Converter { get; set; }
        private AutoStreamMarkerSettings Settings { get; set; }
        private NotifyIcon Notification { get; set; }
        public WebClient Web { get; set; }
        public String Action { get; set; }
        public dynamic User { get; set; }
        public dynamic Stream { get; set; }

        public AutoStreamMarkerComponent(LiveSplitState state)
        {
            Activated = true;

            State = state;
            Settings = new AutoStreamMarkerSettings();

            State.OnStart += State_OnStart;
            State.OnSplit += State_OnSplit;
            State.OnReset += State_OnReset;

            Notification = new NotifyIcon()
            {
                Visible = false,
                BalloonTipTitle = "Auto Stream Marker",
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            };

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            Web = new WebClient();
            Web.Headers.Add("Client-ID", Settings.TwitchClientID);
            Web.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");
        }

        public override void Dispose()
        {
            State.OnStart -= State_OnStart;
            State.OnSplit -= State_OnSplit;
            State.OnReset -= State_OnReset;
            Notification.Dispose();
            Web.Dispose();
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Notification.Visible = Settings.Notify;
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
            Settings.SetSettings(settings);
        }

        private void State_OnStart(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => { StreamMarker("started"); });
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended)
            {
                Task.Factory.StartNew(() => { StreamMarker("finished"); });
            }
            else if(State.CurrentPhase == TimerPhase.Running && Settings.MarkEverySplit)
            {
                StreamMarker(String.Format("split \"{0}\"", State.CurrentSplit.Name));
            }
        }

        private void State_OnReset(object sender, TimerPhase e)
        {
            if (e != TimerPhase.Ended && Settings.MarkResets)
            {
                Task.Factory.StartNew(() => { StreamMarker("reseted"); });
            }
        }

        private void StreamMarker(string action)
        {
            Action = action;
            try
            {
                Web.Headers["Authorization"] = "Bearer " + Settings.TwitchOAuth;
                HandleUser(Web.DownloadString(new Uri("https://api.twitch.tv/helix/users")));
                Console.WriteLine("ok");
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void HandleUser(String data)
        {
            try
            {
                User = JSON.FromString(data);
                Console.WriteLine(data);

                if (User.data is List<Object> && User.data.Count > 0)
                {
                    HandleStream(Web.DownloadString(new Uri(String.Format("https://api.twitch.tv/helix/streams?user_id={0}", User.data[0].id))));
                    return;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Notification.BalloonTipText = "Your need to login with your Twitch account in the Auto Stream Marker layout settings...";
            Notification.ShowBalloonTip(1000);
        }
        private void HandleStream(String data)
        {
            //if (e.Error == null && e.Cancelled == false)
            try
            {
                Console.WriteLine("wegsooSd");
                Stream = JSON.FromString(data);
                if (Stream.data is List<Object> && Stream.data.Count > 0 && String.Equals(Stream.data[0].type, "live"))
                {
                    String title = String.Format("Run #{0} {1}: {2} - {3}", State.Run.AttemptCount, Action, State.Run.GameName, State.Run.CategoryName);
                    NameValueCollection values = new NameValueCollection
                    {
                        { "user_id", User.data[0].id },
                        { "description", title }
                    };
                    Console.WriteLine("wegsood");
                    Console.WriteLine(Web.UploadValues(new Uri("https://api.twitch.tv/helix/streams/markers"), "POST", values));
                        
                    Notification.BalloonTipText = title;
                    Notification.ShowBalloonTip(1000);
                }
                else
                {
                    Notification.BalloonTipText = "Your channel isn't live, start your stream to auto mark the runs in your vods...";
                    Notification.ShowBalloonTip(1000);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(data + ex.Message);
            }
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}
