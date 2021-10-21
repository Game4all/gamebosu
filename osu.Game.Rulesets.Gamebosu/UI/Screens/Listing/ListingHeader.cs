// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Overlays;
using osu.Game.Screens;
using osuTK;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Listing
{
    public class ListingHeader : Container
    {
        public const float HEIGHT = 80;

        private const float spacing = 6;

        private readonly OsuSpriteText dot;
        private readonly OsuSpriteText romListing;

        public ListingHeader()
        {
            RelativeSizeAxes = Axes.X;
            Height = HEIGHT;
            Padding = new MarginPadding { Left = -WaveOverlayContainer.WIDTH_PADDING };

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4Extensions.FromHex(@"#1f1921"),
                },
                new Container
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Left = WaveOverlayContainer.WIDTH_PADDING + OsuScreen.HORIZONTAL_OVERFLOW_PADDING },
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            AutoSizeAxes = Axes.Both,
                            Spacing = new Vector2(spacing, 0),
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Direction = FillDirection.Horizontal,
                            Children = new Drawable[]
                            {
                                new GamebosuRuleset().CreateIcon().With(t => 
                                {
                                    t.Anchor = Anchor.CentreLeft;
                                    t.Origin = Anchor.CentreLeft;
                                    t.Scale = new Vector2(0.75f);
                                    t.Margin = new MarginPadding { Right = 10 };
                                }),
                                new OsuSpriteText
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Font = OsuFont.GetFont(size: 24),
                                    Text = "gamebosu"
                                },
                                dot = new OsuSpriteText
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Font = OsuFont.GetFont(size: 48),
                                    Text = "·"
                                },
                                romListing = new OsuSpriteText
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Font = OsuFont.GetFont(size: 24),
                                    Text = "rom listing"
                                }
                            }
                        },
                    },
                },
            };

        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            romListing.Colour = dot.Colour = colours.Yellow;
        }
    }
}