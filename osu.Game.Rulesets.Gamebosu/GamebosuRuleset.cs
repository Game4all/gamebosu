// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Gamebosu.Beatmaps;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.Graphics;
using osu.Game.Rulesets.Gamebosu.UI;
using osu.Game.Rulesets.Gamebosu.UI.Configuration;
using osu.Game.Rulesets.Gamebosu.UI.Input;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using System;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Gamebosu
{
    public class GamebosuRuleset : Ruleset
    {
        public override string Description => "gamebosu!";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableGamebosuRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new GamebosuBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) =>
            new GamebosuDifficultyCalculator(this, beatmap);

        //this exists for the sole purpose of disabling the red tint on playfield when health is low.
        public override HealthProcessor CreateHealthProcessor(double drainStartTime) => new AccumulatingHealthProcessor(1f);

        public override IEnumerable<Mod> GetModsFor(ModType type) => Array.Empty<Mod>();

        public override string ShortName => "gamebosu";

        public override string PlayingVerb => $"Playing gameboy";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.Left, GamebosuAction.DPadLeft),
            new KeyBinding(InputKey.Right, GamebosuAction.DPadRight),
            new KeyBinding(InputKey.Up, GamebosuAction.DPadUp),
            new KeyBinding(InputKey.Down, GamebosuAction.DPadDown),

            new KeyBinding(InputKey.PageUp, GamebosuAction.ButtonIncrementClockRate),
            new KeyBinding(InputKey.PageDown, GamebosuAction.ButtonDecrementClockRate),

            new KeyBinding(InputKey.A, GamebosuAction.ButtonA),
            new KeyBinding(InputKey.B, GamebosuAction.ButtonB),
            new KeyBinding(InputKey.Enter, GamebosuAction.ButtonSelect),
            new KeyBinding(InputKey.BackSpace, GamebosuAction.ButtonStart),
        };

        public override Drawable CreateIcon() => new RulesetIcon(this)
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
        };

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) => new GamebosuConfigManager(settings, RulesetInfo);

        public override RulesetSettingsSubsection CreateSettings() => new GamebosuSettingsSubsection(this);
    }
}