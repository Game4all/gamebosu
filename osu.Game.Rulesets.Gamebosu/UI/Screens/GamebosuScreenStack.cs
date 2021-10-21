// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Screens;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuScreenStack : ScreenStack
    {
        public GamebosuScreenStack()
            : base(true)
        {
            RelativeSizeAxes = Framework.Graphics.Axes.Both;
        }
    }
}