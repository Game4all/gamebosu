// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osu.Game.Overlays;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Gamebosu.Configuration;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Configuration
{
    public class GamebosuSettingsSubsection : RulesetSettingsSubsection
    {
        private SettingsSlider<double> clockRate;
        private Bindable<bool> lockClockRate;

        public GamebosuSettingsSubsection(Ruleset ruleset)
            : base(ruleset)
        {
        }

        protected override string Header => "gamebosu!";

        [BackgroundDependencyLoader]
        private void load(Storage storage, DialogOverlay dialog)
        {
            var config = Config as GamebosuConfigManager;
            lockClockRate = config.GetBindable<bool>(GamebosuSetting.LockClockRate);

            Children = new Drawable[]
            {
                clockRate = new SettingsSlider<double>
                {
                    LabelText = "Gameboy Clock rate",
                    Bindable = config.GetBindable<double>(GamebosuSetting.ClockRate)
                },
                new SettingsCheckbox
                {
                    LabelText = "Lock gameboy clock rate",
                    Bindable = lockClockRate
                },
                new SettingsCheckbox
                {
                    LabelText = "Prefer Gameboy Color mode when launching original gameboy ROMs",
                    Bindable =  config.GetBindable<bool>(GamebosuSetting.PreferGBCMode)
                },
                new SettingsSlider<float>
                {
                    LabelText = "Gameboy Scale",
                    Bindable = config.GetBindable<float>(GamebosuSetting.GameboyScale)
                },
                new SettingsButton
                {
                    Text = "Open ROMs folder",
                    Action = () => storage.GetStorageForDirectory("roms")?.OpenInNativeExplorer()
                },
                new DangerousSettingsButton
                {
                    Text = "Delete ROM save data",
                    Action = () =>
                    {
                        Action deleteAction = delegate
                        {
                            var saves = storage.GetStorageForDirectory("roms/saves");
                            var files = saves.GetFiles(".");
                            try 
                            {
                                files.ForEach(file => saves.Delete(file));
                            } 
                            catch (Exception)
                            {
                                dialog.Push(new DeleteDataErrorDialog
                                {
                                    BodyText = $"Couldn't delete ROM save data (save data may be used by the currently loaded ROM). Try deleting save data from the main menu"
                                });
                            }
                        };

                        dialog.Push(new DeleteDataDialog(deleteAction));
                    }
                },
                new SettingsCheckbox
                {
                    LabelText = "Enable Sound Playback (VERY EXPERIMENTAL)",
                    Bindable = config.GetBindable<bool>(GamebosuSetting.EnableSoundPlayback)
                }
            };

            lockClockRate.BindValueChanged(e => clockRate.Bindable.Disabled = e.NewValue, true);
        }
    }
}