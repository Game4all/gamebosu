// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.Graphics;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Selection;
using osuTK;

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
                Spacing = new Vector2(0, 5),
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
                    new RomSelector()
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 300,
                        Selected = PushGameplay
                    },
                }
            };
        }

        protected virtual void PushGameplay(EmulatedCartridge e) => this.Push(new GameplayScreen(e));
    }
}