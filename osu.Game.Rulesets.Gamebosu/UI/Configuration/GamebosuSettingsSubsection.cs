// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Gamebosu.Configuration;

namespace osu.Game.Rulesets.Gamebosu.UI.Configuration
{
    public class GamebosuSettingsSubsection : RulesetSettingsSubsection
    {
        public GamebosuSettingsSubsection(Ruleset ruleset) 
            : base(ruleset)
        {
        }

        protected override string Header => "gamebosu!";

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = Config as GamebosuConfigManager;

            Children = new Drawable[]
            {
                new SettingsSlider<double>
                {
                    LabelText = "Gameboy Clock rate",
                    Bindable = config.GetBindable<double>(GamebosuSetting.ClockRate)
                },
                new SettingsCheckbox
                {
                    LabelText = "Prefer Gameboy Color mode when launching original gameboy ROMs",
                    Bindable =  config.GetBindable<bool>(GamebosuSetting.PreferGBCMode)
                }
            };
        }
    }
}
