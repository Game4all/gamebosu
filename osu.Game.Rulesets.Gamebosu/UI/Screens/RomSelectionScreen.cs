using Emux.GameBoy.Cartridge;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class RomSelectionScreen : GamebosuScreen
    {
        public RomSelectionScreen()
        {
            Child = new FillFlowContainer
            {
                Margin = new MarginPadding { Top = 150 },
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
                    },
                    new RomSelector()
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 300,
                        Selected = PushGameplay
                    },
                }
            };
        }

        protected virtual void PushGameplay(EmulatedCartridge e) => Logger.Log($"Selected rom : {e.GameTitle}", LoggingTarget.Runtime, LogLevel.Debug);
    }
}
