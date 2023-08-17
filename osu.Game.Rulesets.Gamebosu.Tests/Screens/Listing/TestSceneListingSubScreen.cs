// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Rulesets.Gamebosu.UI.Screens;

namespace osu.Game.Rulesets.Gamebosu.Tests.Screens.Listing
{
    public partial class TestSceneListingSubScreen : TestSceneGamebosuScreenStack
    {
        protected override GamebosuSubScreen CreateSubScreen() => new ListingSubScreen();
    }
}
