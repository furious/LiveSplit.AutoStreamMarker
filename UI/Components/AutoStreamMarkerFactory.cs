using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(AutoStreamMarkerFactory))]

namespace LiveSplit.UI.Components
{
    public class AutoStreamMarkerFactory : IComponentFactory
    {
        public string ComponentName => "Auto Stream Marker";

        public string Description => "Automatic creates Twitch VOD markers in the current livestream.";

        public ComponentCategory Category => ComponentCategory.Other;

        public IComponent Create(LiveSplitState state) => new AutoStreamMarkerComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "https://furious.github.io/LiveSplit.AutoStreamMarker/update.xml";

        public string UpdateURL => "https://github.com/furious/LiveSplit.AutoStreamMarker/releases/";

        public Version Version => Version.Parse("1.0.0");
    }
}
