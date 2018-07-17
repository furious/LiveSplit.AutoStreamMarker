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
using System.Collections.Generic;


namespace LiveSplit.UI.Components
{
    public class TwitchHighlighterComponent : LogicComponent, IDeactivatableComponent
    {
        public override string ComponentName => "Twitch Highlighter";

        public bool Activated { get; set; }
        
        private LiveSplitState State { get; set; }
        private DynamicJsonConverter Converter { get; set; }
        private TwitchHighlighterSettings Settings { get; set; }
        private NotifyIcon Notification { get; set; }
        public WebClient Web { get; set; }
        public String Action { get; set; }
        public dynamic User { get; set; }
        public dynamic Stream { get; set; }

        public TwitchHighlighterComponent(LiveSplitState state)
        {
            Activated = true;

            State = state;
            Settings = new TwitchHighlighterSettings();

            State.OnStart += State_OnStart;
            State.OnSplit += State_OnSplit;
            State.OnReset += State_OnReset;

            Notification = new NotifyIcon()
            {
                Visible = false,
                BalloonTipTitle = "Twitch Highlighter",
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
            TwitchHighlight("started");
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended)
            {
                TwitchHighlight("finished");
            }
            else if(State.CurrentPhase == TimerPhase.Running && Settings.MarkEverySplit)
            {
                TwitchHighlight(String.Format("split \"{0}\"", State.CurrentSplit.Name));
            }
        }

        private void State_OnReset(object sender, TimerPhase e)
        {
            if (e != TimerPhase.Ended && Settings.MarkResets)
            {
                TwitchHighlight("reseted");
            }
        }

        private void TwitchHighlight(string action)
        {
            Action = action;
            try
            {
                using(Web = new WebClient())
                {
                    Web.Headers.Add("Authorization", "OAuth " + Settings.TwitchOAuth);
                    Web.DownloadStringCompleted += HandleUser;
                    Web.DownloadStringAsync(new Uri("https://api.twitch.tv/kraken/user"));
                }
            }
            catch (WebException ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void HandleUser(object s, DownloadStringCompletedEventArgs e)
        {
            //MessageBox.Show(e.Result);
            if(e.Error == null && e.Cancelled == false)
            try
            {
                User = JSON.FromString(e.Result);

                if (!Object.Equals(null, User) && !String.IsNullOrEmpty(User.name))
                {
                    Web.DownloadStringCompleted -= HandleUser;
                    Web.DownloadStringCompleted += HandleStream;
                    Web.DownloadStringAsync(new Uri(String.Format("https://api.twitch.tv/kraken/streams/{0}", User.name)));
                    return;
                }
            }
            catch (WebException ex)
            {
                    //MessageBox.Show(e.Result + ex.ToString());
            }
            Notification.BalloonTipText = "Your need to login with your Twitch account in the Twitch Highlighter layout settings...";
            Notification.ShowBalloonTip(1000);
        }
        private void HandleStream(object s, DownloadStringCompletedEventArgs e)
        {
            //MessageBox.Show(e.Result);
            if (e.Error == null && e.Cancelled == false)
            try
            {
                Stream = JSON.FromString(e.Result).stream;
                if (!Object.Equals(null, Stream) && !String.IsNullOrEmpty(Stream.stream_type) && String.Equals(Stream.stream_type, "live"))
                {
                    String title = String.Format("Run #{0} {1}: {2} - {3}", State.Run.AttemptCount, Action, State.Run.GameName, State.Run.CategoryName);
                    String data = "[{\"operationName\":\"DashboardCreateVideoBookmark\",\"variables\":{\"input\":{\"broadcastID\":\"" + Stream._id + "\",\"description\":\"" + title.Replace("\"", "\\\"") + "\",\"medium\":\"live_dashboard_button\",\"platform\":\"web\"}},\"extensions\":{\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"414ea8133c174d305012208b538cec58c27bd6ad5d9598dd0e6c1aeb9044cf08\"}}}]";

                    Web.DownloadStringCompleted -= HandleStream;
                    Web.UploadStringAsync(new Uri("https://gql.twitch.tv/gql"), "POST", data);
                        
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
                MessageBox.Show(e.Result + ex.ToString());
            }
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}
