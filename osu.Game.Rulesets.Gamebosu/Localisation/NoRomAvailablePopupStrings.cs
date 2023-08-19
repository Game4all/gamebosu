using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Gamebosu.Localisation
{
    public static class NoRomAvailablePopupStrings
    {
        private const string prefix = @"osu.Game.Rulesets.Gamebosu.Localisation.NoRomAvailablePopup";

        /// <summary>
        /// "Sadly there&#39;s no usable ROM avalaible ..."
        /// </summary>
        public static LocalisableString NoRomAvailable => new TranslatableString(getKey(@"no_rom_available"), @"Sadly there's no usable ROM avalaible ...");

        /// <summary>
        /// "Go grab some ROM files and put &#39;em in the roms folder"
        /// </summary>
        public static LocalisableString AddInstructions => new TranslatableString(getKey(@"add_instructions"), @"Go grab some ROM files and put 'em in the roms folder");

        /// <summary>
        /// "(You can open the roms folder from the settings or drag n&#39; drop the rom files into osu! window)"
        /// </summary>
        public static LocalisableString AddTip => new TranslatableString(getKey(@"add_tip"), @"(You can open the roms folder from the settings or drag n' drop the rom files into osu! window)");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}