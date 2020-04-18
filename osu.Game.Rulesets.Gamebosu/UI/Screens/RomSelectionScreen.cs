using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class RomSelectionScreen : GamebosuScreen
    {
        private FillFlowContainer flow;
        private ScrollContainer<FillFlowContainer> scrolling;
        private FillFlowContainer romFlow;

        public RomSelectionScreen()
        {
            Child = flow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Icon = FontAwesome.Solid.Gamepad,
                        Size = new osuTK.Vector2(40)
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Text = "Game selection",
                        Font = OsuFont.GetFont(Typeface.Torus, 32, FontWeight.Bold)
                    }
                }
            };
        }
    }
}
