using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Gamebosu.Localisation
{
    public static class DisclaimerSubScreenStrings
    {
        private const string prefix = @"osu.Game.Rulesets.Gamebosu.Localisation.DisclaimerSubScreen";

        /// <summary>
        /// "You can delete ROM save data from the settings overlay, try searching for &#39;delete ROM save data&#39;!"
        /// </summary>
        public static LocalisableString Tip1 => new TranslatableString(getKey(@"tip_1"), @"You can delete ROM save data from the settings overlay, try searching for 'delete ROM save data'!");

        /// <summary>
        /// "Try pressing Page-up or Page-down to change the ROM emulation speed!"
        /// </summary>
        public static LocalisableString Tip2 => new TranslatableString(getKey(@"tip_2"), @"Try pressing Page-up or Page-down to change the ROM emulation speed!");

        /// <summary>
        /// "You can customize the gameboy screen scale from the settings overlay, try searching for &#39;gameboy scale&#39;!"
        /// </summary>
        public static LocalisableString Tip3 => new TranslatableString(getKey(@"tip_3"), @"You can customize the gameboy screen scale from the settings overlay, try searching for 'gameboy scale'!");

        /// <summary>
        /// "You can open the ROM folder from the settings overlay, try searching for &#39;open rom folder&#39;!"
        /// </summary>
        public static LocalisableString Tip4 => new TranslatableString(getKey(@"tip_4"), @"You can open the ROM folder from the settings overlay, try searching for 'open rom folder'!");

        /// <summary>
        /// "You can enable audio playback of the gameboy speaker in the settings, but don&#39;t do for the time being. It currently sounds more like noise."
        /// </summary>
        public static LocalisableString Tip5 => new TranslatableString(getKey(@"tip_5"), @"You can enable audio playback of the gameboy speaker in the settings, but don't do for the time being. It currently sounds more like noise.");

        /// <summary>
        /// "You can disable this disclaimer in the settings, try searching for &#39;disable that annoying startup disclaimer&#39;!"
        /// </summary>
        public static LocalisableString Tip6 => new TranslatableString(getKey(@"tip_6"), @"You can disable this disclaimer in the settings, try searching for 'disable that annoying startup disclaimer'!");

        /// <summary>
        /// "Disclaimer"
        /// </summary>
        public static LocalisableString Disclaimer => new TranslatableString(getKey(@"disclaimer"), @"Disclaimer");

        /// <summary>
        /// "This is a WIP, so don&#39;t expect things to work as expected."
        /// </summary>
        public static LocalisableString WipDisclaimer => new TranslatableString(getKey(@"wip_disclaimer"), @"This is a WIP, so don't expect things to work as expected.");

        /// <summary>
        /// "Press your (A) (B), (Select) (Start) button to skip this."
        /// </summary>
        public static LocalisableString PressToContinue => new TranslatableString(getKey(@"press_to_continue"), @"Press your (A) (B), (Select) (Start) button to skip this.");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}