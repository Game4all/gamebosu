// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Game.Configuration;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.IO;
using osu.Game.Rulesets.Gamebosu.UI.Screens;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Gamebosu.Tests.Screens
{
    public abstract class TestSceneGamebosuScreenStack : OsuTestScene
    {
        protected abstract GamebosuSubScreen CreateSubScreen();

        protected GamebosuScreenStack Stack { get; private set; }

        public TestSceneGamebosuScreenStack()
        {
            Child = Stack = new GamebosuScreenStack();
        }

        [SetUpSteps]
        public virtual void SetUpSteps() 
        {
            AddStep("create screen", () => Stack.Push(CreateSubScreen()));
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var deps = new DependencyContainer(base.CreateChildDependencies(parent));

            deps.Cache(new RomStore(LocalStorage));
            deps.Cache(new GamebosuConfigManager(parent.Get<SettingsStore>(), new GamebosuRuleset().RulesetInfo));

            return deps;
        }
    }
}
