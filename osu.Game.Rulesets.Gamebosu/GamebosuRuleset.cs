// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Gamebosu.Beatmaps;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.Mods;
using osu.Game.Rulesets.Gamebosu.UI;
using osu.Game.Rulesets.Gamebosu.UI.Configuration;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Gamebosu
{
    public class GamebosuRuleset : Ruleset
    {
        public override string Description => "a very gamebosuruleset ruleset";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableGamebosuRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new GamebosuBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) =>
            new GamebosuDifficultyCalculator(this, beatmap);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new GamebosuModAutoplay() };

                default:
                    return new Mod[] { null };
            }
        }

        public override string ShortName => "gamebosuruleset";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.Left, GamebosuAction.DPadLeft),
            new KeyBinding(InputKey.Right, GamebosuAction.DPadRight),
            new KeyBinding(InputKey.Up, GamebosuAction.DPadUp),
            new KeyBinding(InputKey.Down, GamebosuAction.DPadDown),

            new KeyBinding(InputKey.A, GamebosuAction.ButtonA),
            new KeyBinding(InputKey.B, GamebosuAction.ButtonB)
        };

        public override Drawable CreateIcon() => new Icon(ShortName[0]);

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) => new GamebosuConfigManager(settings, RulesetInfo);

        public override RulesetSettingsSubsection CreateSettings() => new GamebosuSettingsSubsection(this);

        public class Icon : CompositeDrawable
        {
            public Icon(char c)
            {
                InternalChildren = new Drawable[]
                {
                    new Circle
                    {
                        Size = new Vector2(20),
                        Colour = Color4.White,
                    },
                    new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = c.ToString(),
                        Font = OsuFont.Default.With(size: 18)
                    }
                };
            }
        }
    }
}
