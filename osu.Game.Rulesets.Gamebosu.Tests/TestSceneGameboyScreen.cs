using osu.Game.Rulesets.Gamebosu.UI.Gameboy;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Gamebosu.Tests
{
    public class TestSceneGameboyScreen : OsuTestScene
    {
        private readonly DrawableGameboyScreen screen;

        public TestSceneGameboyScreen()
        {
            Child = screen = new DrawableGameboyScreen
            {
                Size = new osuTK.Vector2(160, 144),
                Anchor = Framework.Graphics.Anchor.Centre,
                Origin = Framework.Graphics.Anchor.Centre
            };

            screen.Clear();
        }
    }
}
