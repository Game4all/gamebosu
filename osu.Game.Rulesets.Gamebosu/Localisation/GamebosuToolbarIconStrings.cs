using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Gamebosu.Localisation
{
    public static class GamebosuToolbarIconStrings
    {
        private const string prefix = @"osu.Game.Rulesets.Gamebosu.Localisation.GamebosuToolbarIcon";

        /// <summary>
        /// "gamebosu"
        /// </summary>
        public static LocalisableString TooltipString => new TranslatableString(getKey(@"tooltip_string"), @"gamebosu");

        /// <summary>
        /// "Open the ROM selection screen"
        /// </summary>
        public static LocalisableString TooltipDescString => new TranslatableString(getKey(@"tooltip_desc_string"), @"Open the ROM selection screen");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}