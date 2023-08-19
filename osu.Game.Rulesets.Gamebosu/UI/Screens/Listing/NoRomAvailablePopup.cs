// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.Localisation;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Listing
{
    public partial class NoRomAvailablePopup : VisibilityContainer
    {
        private const double fade_time = 300;
        private const Easing easing = Easing.OutQuint;

        private const int sprite_size = 120;

        protected override bool StartHidden => true;

        public NoRomAvailablePopup()
        {
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Masking = true;
            CornerRadius = 16;

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Colour4.Gray.Opacity(0.4f),
                    RelativeSizeAxes = Axes.Both
                },
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Margin = new MarginPadding(16),
                    Spacing = new osuTK.Vector2(0, 10),
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new SpriteIcon
                        {
                            Icon = FontAwesome.Solid.SadCry,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new osuTK.Vector2(sprite_size)
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = OsuFont.GetFont(Typeface.Torus, 28, FontWeight.Bold),
                            Text = NoRomAvailablePopupStrings.NoRomAvailable,
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = OsuFont.GetFont(Typeface.Torus, 18, FontWeight.Regular),
                            Text = NoRomAvailablePopupStrings.AddInstructions,
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = OsuFont.GetFont(Typeface.Torus, 16, FontWeight.Regular),
                            Text = NoRomAvailablePopupStrings.AddTip,
                        }
                    }
                }
            };
        }

        protected override void PopIn() => Content.FadeIn(2 * fade_time, easing);

        protected override void PopOut() => Content.FadeOut(2 * fade_time, easing);
    }
}