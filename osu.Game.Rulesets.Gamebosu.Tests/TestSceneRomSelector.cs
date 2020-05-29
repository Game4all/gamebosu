// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using NUnit.Framework;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Gamebosu.Tests
{
    public class TestSceneRomSelector : OsuTestScene
    {
        private RomSelector romSelector;

        public TestSceneRomSelector()
        {
        }

        
        [SetUp]
        public void SetUp()
        {
            Child = romSelector = new RomSelector()
            {
                RelativeSizeAxes = Framework.Graphics.Axes.Both
            };
        }

        [Test]
        public void TestRomSelection()
        {
            AddStep("add roms", () =>
            {
                var roms = new string[]
                {
                    "rom.gb",
                    "rom.gba",
                    "yes.gba",
                };

                romSelector.AvalaibleRoms = roms;
            });

            AddStep("select next rom", () => romSelector.OnPressed(GamebosuAction.DPadRight));
            AddStep("go back to previous rom", () => romSelector.OnPressed(GamebosuAction.DPadLeft));
            AddStep("show rom as unavalaible", () => romSelector.MarkUnavalaible());
        }

        [Test]
        public void TestNoRom()
        {
        }
    }
}
