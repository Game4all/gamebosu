using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Gamebosu.Localisation
{
    public static class ListingHeaderStrings
    {
        private const string prefix = @"osu.Game.Rulesets.Gamebosu.Localisation.ListingHeader";

        /// <summary>
        /// "rom listing"
        /// </summary>
        public static LocalisableString ListingTitle => new TranslatableString(getKey(@"listing_title"), @"rom listing");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}