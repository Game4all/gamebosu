// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using NUnit.Framework;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Gamebosu.UI.Input;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;
using osu.Game.Tests.Visual;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.Tests.Screens.Selection
{
    public partial class TestSceneRomSelector : OsuTestScene
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

                romSelector.AvailableRoms.Value = roms;
            });

            AddStep("select next rom", () => romSelector.OnPressed(new KeyBindingPressEvent<GamebosuAction>(new Framework.Input.States.InputState(), GamebosuAction.DPadRight)));
            AddStep("go back to previous rom", () => romSelector.OnPressed(new KeyBindingPressEvent<GamebosuAction>(new Framework.Input.States.InputState(), GamebosuAction.DPadLeft)));
            AddStep("show rom as unavalaible", () => romSelector.MarkUnavailable());
            AddStep("clear roms", () => romSelector.AvailableRoms.Value = Enumerable.Empty<string>());
        }

        [Test]
        public void TestNoRom()
        {
        }
    }
}
