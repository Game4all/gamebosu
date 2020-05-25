// gamebosu! ruleset. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.Graphics;
using osu.Game.Rulesets.Gamebosu.IO;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;
using osuTK;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class RomSelectionScreen : GamebosuScreen
    {
        private readonly RomSelector romSelector;
        private RomStore store;

        public RomSelectionScreen()
        {
            Child = new FillFlowContainer
            {
                Margin = new MarginPadding { Top = 100 },
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Spacing = new Vector2(0, 20),
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new RulesetIcon
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Text = "Game selection",
                        Font = OsuFont.GetFont(Typeface.Torus, 32, FontWeight.Bold)
                    },
                    romSelector = new RomSelector()
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 300,
                        RomSelected = loadRom
                    },
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(RomStore roms)
        {
            store = roms;
            romSelector.AvalaibleRoms = roms.GetAvailableResources();
        }

        private void loadRom(string romName)
        {
            if (romName == null)
            {
                romSelector.MarkUnavalaible();
                return;
            }

            store.GetAsync(romName).ContinueWith(t =>
            {
                if (t.Result != null)
                    PushGameplay(t.Result);
                else
                    romSelector.MarkUnavalaible();
            });
        }

        protected virtual void PushGameplay(EmulatedCartridge e) => this.Push(new GameplayScreen(e));
    }
}