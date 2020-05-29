// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Gamebosu.Graphics
{
    public class RulesetIcon : CompositeDrawable
    {
        protected override bool CanBeFlattened => true;

        public RulesetIcon()
        {
            AutoSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Icon = FontAwesome.Regular.Circle,
                    Size = new Vector2(40),
                    Colour = Color4.White
                },
                new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Icon = FontAwesome.Solid.Gamepad,
                    Size = new Vector2(20),
                    Colour = Color4.White
                }
            };
        }
    }
}