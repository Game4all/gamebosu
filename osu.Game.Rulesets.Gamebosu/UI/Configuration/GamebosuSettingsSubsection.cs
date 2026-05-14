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
using osu.Game.Graphics.UserInterfaceV2;
using osu.Game.Overlays;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.UI.Screens;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Configuration
{
    public partial class GamebosuSettingsSubsection : RulesetSettingsSubsection
    {
        private const string github_url = "https://github.com/Game4all/gamebosu/releases";

        private SettingsItemV2 clockRateSlider;

        private Bindable<double> clockRateBindable;

        private Bindable<bool> lockClockRateBindable;

        private readonly GamebosuRuleset ruleset;

        public GamebosuSettingsSubsection(GamebosuRuleset ruleset)
            : base(ruleset)
        {
            this.ruleset = ruleset;
        }

        protected override LocalisableString Header => "gamebosu!";

        [BackgroundDependencyLoader]
        private void load(Storage storage, IDialogOverlay dialog, OsuGame game, OsuColour colors)
        {
            var config = Config as GamebosuConfigManager;
            clockRateBindable = config.GetBindable<double>(GamebosuSetting.ClockRate);
            lockClockRateBindable = config.GetBindable<bool>(GamebosuSetting.LockClockRate);

            var lockedNote = new SettingsNote.Data(Text: "Clock rate is locked. Disable 'Lock Gameboy Clock Rate' if you want to modify it.", Type: SettingsNote.Type.Informational);

            Children = new Drawable[]
            {
                clockRateSlider = new SettingsItemV2(new FormSliderBar<double>
                {
                    Caption = "Gameboy Clock Rate",
                    HintText = "Controls the clock rate of the emulated gameboy. Baseline is 1x. Higher means faster.",
                    Current = clockRateBindable
                }),
                new SettingsItemV2(new FormCheckBox
                {
                    Caption = "Lock Gameboy Clock Rate",
                    HintText = "Lock the clock rate from being modified.",
                    Current = lockClockRateBindable
                }),
                new SettingsItemV2(new FormCheckBox
                {
                    Caption = "Prefer Gameboy Color mode when launching original gameboy ROMs",
                    HintText = "When launching DMG-01 (Original Gameboy) ROMS, prefer running the ROM in Gameboy Color mode",
                    Current =  config.GetBindable<bool>(GamebosuSetting.PreferGBCMode)
                }),
                new SettingsItemV2(new FormSliderBar<float>
                {
                    Caption = "Gameboy Scale",
                    HintText = "Scale of the gameboy",
                    Current = config.GetBindable<float>(GamebosuSetting.GameboyScale)
                }),
                new SettingsButtonV2
                {
                    Text = "Open ROMs folder",
                    Action = () => storage.GetStorageForDirectory("roms")?.PresentExternally()
                },
                new DangerousSettingsButtonV2
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
                new SettingsItemV2(new FormCheckBox
                {
                    Caption = "Disable launch disclaimer",
                    HintText = "Disables that annoying disclaimer popping up everytime you open the overlay",
                    Current = config.GetBindable<bool>(GamebosuSetting.DisableDisplayingThatAnnoyingDisclaimer)
                }),
                new SettingsButtonV2
                {

                    Text = "Open ROM listing",
                    BackgroundColour = colors.YellowDark,
                    Height = 60,
                    Action = () => game?.PerformFromScreen(scr => scr.Push(new GamebosuMainScreen(ruleset)))
                },
                new SettingsButtonV2
                {
                    Text = "Checkout Github project releases",
                    Action = () => game?.OpenUrlExternally(github_url),
                }
            };

            lockClockRateBindable.BindValueChanged(e =>
            {
                clockRateBindable.Disabled = e.NewValue;
                clockRateSlider.Note.Value = e.NewValue ? lockedNote : null;
            }, true);
        }
    }
}