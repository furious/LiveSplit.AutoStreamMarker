using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(TwitchHighlighterFactory))]

namespace LiveSplit.UI.Components
{
    public class TwitchHighlighterFactory : IComponentFactory
    {
        public string ComponentName => "Twitch Highlighter";

        public string Description => "Automatic creates Twitch markers (highlights) in the current livestream.";

        public ComponentCategory Category => ComponentCategory.Other;

        public IComponent Create(LiveSplitState state) => new TwitchHighlighterComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "https://furious.github.io/LiveSplit.TwitchHighligther/update.xml";

        public string UpdateURL => "https://github.com/furious/LiveSplit.TwitchHighligther/releases/";

        public Version Version => Version.Parse("1.0.0");
    }
}
