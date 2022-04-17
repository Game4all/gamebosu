// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Gamebosu.Graphics
{
    public class RulesetIcon : CompositeDrawable
    {
        protected override bool CanBeFlattened => true;

        public RulesetIcon(TextureStore store)
        {
            AutoSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Icon = FontAwesome.Regular.Circle,
                    Size = new Vector2(60),
                    Colour = Color4.White
                },
                new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Texture = store.Get("Textures/logo_pixelated.png"),
                    FillMode = FillMode.Fit,
                    Size = new Vector2(40)
                }
            };
        }
    }
}