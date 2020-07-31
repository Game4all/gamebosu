// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Game.Online.API;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuScreenStack : ScreenStack
    {
        [Resolved]
        private IAPIProvider api { get; set; }

        public GamebosuScreenStack()
            : base(false)
        {
            RelativeSizeAxes = Framework.Graphics.Axes.Both;
            ScreenPushed += screenChanged;
        }

        private void screenChanged(IScreen lastScreen, IScreen newScreen)
        {
            var act = (newScreen as GamebosuScreen).ScreenActivity;

            if (act != null)
                api.Activity.Value = act;
        }
    }
}