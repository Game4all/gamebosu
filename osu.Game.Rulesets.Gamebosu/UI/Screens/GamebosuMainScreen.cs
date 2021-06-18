// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Platform;
using osu.Game.Configuration;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.IO;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuMainScreen : ScreenWithCyclingBeatmapBackground
    {
        public GamebosuMainScreen()
        {
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var deps = new DependencyContainer(base.CreateChildDependencies(parent));

            deps.Cache(new RomStore(parent.Get<Storage>()));
            deps.Cache(new GamebosuConfigManager(parent.Get<SettingsStore>(), new GamebosuRuleset().RulesetInfo));

            return deps;
        }
    }
}