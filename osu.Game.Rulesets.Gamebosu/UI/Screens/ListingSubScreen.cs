// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Listing;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class ListingSubScreen : GamebosuSubScreen
    {
        public ListingSubScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Padding = new MarginPadding { Horizontal = -80 };

            var backgroundColour = Color4Extensions.FromHex(@"3e3a44");

            InternalChild = new WaveContainer
            {
                State = { Value = Visibility.Visible },
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = backgroundColour,
                    },
                    new ListingHeader()
                }
            };
        }

        public override void OnEntering(IScreen last)
        {
        }
    }
}