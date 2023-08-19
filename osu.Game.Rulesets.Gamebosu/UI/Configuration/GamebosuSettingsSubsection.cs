// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Overlays;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.UI.Screens;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Configuration
{
    public partial class GamebosuSettingsSubsection : RulesetSettingsSubsection
    {
        private SettingsSlider<double> clockRate;
        private Bindable<bool> lockClockRate;

        private readonly GamebosuRuleset ruleset;

        public GamebosuSettingsSubsection(GamebosuRuleset ruleset)
            : base(ruleset)
        {
            this.ruleset = ruleset;
        }

        protected override LocalisableString Header => "gamebosu!";

        [BackgroundDependencyLoader]
        private void load(Storage storage, IDialogOverlay dialog, OsuGame game)
        {
            var config = Config as GamebosuConfigManager;
            lockClockRate = config.GetBindable<bool>(GamebosuSetting.LockClockRate);

            Children = new Drawable[]
            {
                clockRate = new SettingsSlider<double>
                {
                    LabelText = "Gameboy Clock rate",
                    Current = config.GetBindable<double>(GamebosuSetting.ClockRate)
                },
                new SettingsCheckbox
                {
                    LabelText = "Lock gameboy clock rate",
                    Current = lockClockRate
                },
                new SettingsCheckbox
                {
                    LabelText = "Prefer Gameboy Color mode when launching original gameboy ROMs",
                    Current =  config.GetBindable<bool>(GamebosuSetting.PreferGBCMode)
                },
                new SettingsSlider<float>
                {
                    LabelText = "Gameboy Scale",
                    Current = config.GetBindable<float>(GamebosuSetting.GameboyScale)
                },
                new SettingsButton
                {
                    Text = "Open ROMs folder",
                    Action = () => storage.GetStorageForDirectory("roms")?.PresentExternally()
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
                    Current = config.GetBindable<bool>(GamebosuSetting.EnableSoundPlayback)
                },
                new SettingsCheckbox
                {
                    LabelText = "Disable that annoying disclaimer when launching gamebosu!",
                    Current = config.GetBindable<bool>(GamebosuSetting.DisableDisplayingThatAnnoyingDisclaimer)
                },
                new YellowSettingsButton
                {
                    Text = "Open ROM listing",
                    Action = () => game?.PerformFromScreen(scr => scr.Push(new GamebosuMainScreen(ruleset)))
                },
            };

            lockClockRate.BindValueChanged(e => clockRate.Current.Disabled = e.NewValue, true);
        }

        private partial class YellowSettingsButton : SettingsButton
        {
            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                Height = 60;
                BackgroundColour = colours.Yellow;
            }
        }
    }
}