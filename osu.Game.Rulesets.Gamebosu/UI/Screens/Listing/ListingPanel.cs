// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Listing
{
    public class ListingPanel : OsuClickableContainer
    {
        private readonly string romName;

        public ListingPanel(string romName) 
            : base(HoverSampleSet.Button)
        {
            //AutoSizeAxes = Framework.Graphics.Axes.X;
            //Height = 400;

            //this.romName = romName;
            //Child = new SelectionCard(romName);
        }
    }
}
