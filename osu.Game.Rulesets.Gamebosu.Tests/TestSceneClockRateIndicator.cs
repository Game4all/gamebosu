// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Bindables;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Gameplay;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Gamebosu.Tests
{
    public class TestSceneClockRateIndicator : OsuTestScene
    {
        private readonly ClockRateIndicator indic;

        private readonly BindableDouble val = new BindableDouble
        {
            MinValue = 0,
            MaxValue = 10,
            Value = 5,
        };

        public TestSceneClockRateIndicator()
        {
            Child = indic =  new ClockRateIndicator
            {
                Anchor = Framework.Graphics.Anchor.BottomCentre,
                Origin = Framework.Graphics.Anchor.BottomCentre,
                Margin = new Framework.Graphics.MarginPadding { Bottom = 20 }
            };

            indic.Rate.BindTo(val);

            AddStep("show", () => indic.Show());
            AddSliderStep("slider", 0.0, 10.0, 5.0, t => val.Value = t);
        }
    }
}
