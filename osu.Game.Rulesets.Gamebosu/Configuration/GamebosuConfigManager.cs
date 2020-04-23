// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Gamebosu.Configuration
{
    public class GamebosuConfigManager : RulesetConfigManager<GamebosuSetting>
    {
        public GamebosuConfigManager(SettingsStore settings, RulesetInfo ruleset)
            : base(settings, ruleset, 0)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(GamebosuSetting.ClockRate, 1, 0.1, 5, 0.1);
            Set(GamebosuSetting.PreferGBCMode, true);
            Set(GamebosuSetting.GameboyScale, 2f, 1f, 5f, 0.1f);
            base.InitialiseDefaults();
        }
    }

    public enum GamebosuSetting
    {
        ClockRate,
        GameboyScale,
        PreferGBCMode
    }
}