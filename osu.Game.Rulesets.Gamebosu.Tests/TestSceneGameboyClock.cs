// gamebosu! ruleset. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Gamebosu.UI.Gameboy;
using osu.Game.Tests.Visual;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Gamebosu.Tests
{
    public class TestSceneGameboyClock : OsuTestScene
    {
        private readonly DrawableGameboyClock clock;
        private readonly Box box;

        public TestSceneGameboyClock()
        {
            Children = new Drawable[]
            {
                clock = new DrawableGameboyClock(),
                box = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White
                }
            };

            AddSliderStep<double>("clock rate", 0, 2, 1, t =>
            {
                clock.Rate.Value = t;
            });

            AddToggleStep("enable clock", t =>
            {
                if (t)
                    clock.Start();
                else
                    clock.Stop();
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            clock.Tick += (_, __) => box.FlashColour(Color4.Yellow, 100);
            clock.Start();
        }
    }
}
