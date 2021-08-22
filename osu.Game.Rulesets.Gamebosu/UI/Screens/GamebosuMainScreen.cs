// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osu.Game.Rulesets.Gamebosu.IO;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuMainScreen : ScreenWithCyclingBeatmapBackground
    {
        private readonly GamebosuScreenStack screenStack;

        private readonly GamebosuRuleset ruleset;

        public GamebosuMainScreen(GamebosuRuleset ruleset)
        {
            this.ruleset = ruleset;
            InternalChild = screenStack = new GamebosuScreenStack();
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var container = new DependencyContainer(parent);

            container.Cache(ruleset);
            container.Cache(container.Get<RulesetConfigCache>().GetConfigFor(ruleset));
            container.Cache(new RomStore(container.Get<Storage>()));

            return container;
        }

        public override void OnEntering(IScreen last)
        {
            screenStack.Push(new DisclaimerSubScreen
            {
                Complete = () => screenStack.Push(new ListingSubScreen())
            });

            base.OnEntering(last);
        }

        public override bool OnExiting(IScreen next)
        {
            if (screenStack.CurrentScreen is GameplaySubScreen)
            {
                screenStack.Exit();
                return true;
            }
          
            return base.OnExiting(next);
        }
    }
}