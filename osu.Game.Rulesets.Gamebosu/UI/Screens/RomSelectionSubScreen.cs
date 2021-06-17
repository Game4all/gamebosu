// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Framework.Threading;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.IO;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;
using osuTK;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class RomSelectionSubScreen : GamebosuSubScreen
    {
        private RomSelector romSelector;
        private RomStore store;

        private ScheduledDelegate romListUpdateDelegate;

        [BackgroundDependencyLoader]
        private void load(RomStore roms, DrawableGamebosuRuleset ruleset)
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
                    ruleset.Ruleset.CreateIcon()
                    .With(t => t.Anchor = Anchor.TopCentre)
                    .With(t => t.Origin = Anchor.TopCentre),

                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Text = "Game selection",
                        Font = OsuFont.GetFont(Typeface.Torus, 32, FontWeight.Bold)
                    },
                    romSelector = new RomSelector
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 300,
                        RomSelected = loadRom,
                        AvailableRoms = { Value = roms.GetAvailableResources() }
                    },
                }
            };

            store = roms;
        }

        private void loadRom(string romName)
        {
            if (romName == null)
            {
                romSelector.MarkUnavailable();
                return;
            }

            store.GetAsync(romName).ContinueWith(t =>
            {
                if (t.Result != null)
                    this.Push(new GameplaySubScreen(t.Result));
                else
                    romSelector.MarkUnavailable();
            });
        }

        private void fetchRomList()
        {
            var list = store.GetAvailableResources();

            if (!Enumerable.SequenceEqual(list, romSelector.AvailableRoms.Value))
                romSelector.AvailableRoms.Value = list;
        }

        public override void OnEntering(IScreen last)
        {
            romListUpdateDelegate = Scheduler.AddDelayed(fetchRomList, 5000, true);
            base.OnEntering(last);
        }

        public override bool OnExiting(IScreen next)
        {
            romListUpdateDelegate?.Cancel();
            return base.OnExiting(next);
        }
    }
}