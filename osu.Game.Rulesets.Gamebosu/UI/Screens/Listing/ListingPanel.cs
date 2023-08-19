// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Listing
{
    public partial class ListingPanel : OsuClickableContainer
    {
        public ListingPanel(string romName) 
            : base(HoverSampleSet.Button)
        {
            AutoSizeAxes = Framework.Graphics.Axes.X;
            Height = 380;
            Child = new SelectionCard(romName);
        }
    }
}
