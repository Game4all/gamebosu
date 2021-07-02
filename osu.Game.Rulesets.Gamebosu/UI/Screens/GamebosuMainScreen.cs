// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osu.Game.Configuration;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.IO;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuMainScreen : ScreenWithCyclingBeatmapBackground
    {
        private GamebosuScreenStack screenStack;

        [Cached]
        private GamebosuConfigManager configManager { get; set; } 

        [Cached]
        private RomStore romStore { get; set; }

        public GamebosuMainScreen()
        {
        }
        
        [BackgroundDependencyLoader]
        private void load(SettingsStore store, Storage storage)
        {
            configManager = new GamebosuConfigManager(store, new GamebosuRuleset().RulesetInfo);
            romStore = new RomStore(storage);
            InternalChild = screenStack = new GamebosuScreenStack();
        }

        public override void OnEntering(IScreen last)
        {
            screenStack.Push(new DisclaimerSubScreen
            {
                Complete = () => screenStack.Push(new RomSelectionSubScreen())
            });
            base.OnEntering(last);
        }
    }
}