// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class NoRomAvailableMessage : VisibilityContainer
    {
        private const int sprite_size = 120;
        private const int text_padding = 20;

        public NoRomAvailableMessage()
        {
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Alpha = 0;

            Children = new Drawable[]
            {
                new SpriteIcon
                {
                    Icon = FontAwesome.Solid.SadCry,
                    RelativeSizeAxes = Axes.Y,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new osuTK.Vector2(sprite_size)
                },
                new OsuSpriteText
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding() { Bottom = text_padding },
                    Font = OsuFont.GetFont(Typeface.Torus, 28, FontWeight.Bold),
                    Text = "Sadly there's no usable ROM avalaible ...",
                },
                new OsuSpriteText
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = OsuFont.GetFont(Typeface.Torus, 16, FontWeight.Regular),
                    Text = "Go grab some ROM files and put 'em in the roms folder",
                }
            };
        }

        protected override void PopIn() => Content.FadeIn(2 * RomSelector.FADE_TIME, RomSelector.EASING);

        protected override void PopOut() => Content.FadeOut(2 * RomSelector.FADE_TIME, RomSelector.EASING);
    }
}