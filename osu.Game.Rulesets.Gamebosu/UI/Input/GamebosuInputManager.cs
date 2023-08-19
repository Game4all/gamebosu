// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Game.Input.Bindings;

namespace osu.Game.Rulesets.Gamebosu.UI.Input
{
    public partial class GamebosuInputManager : PassThroughInputManager
    {
        private readonly RulesetKeyBindingContainer keybindingContainer;

        protected override Container<Drawable> Content => content;

        private readonly Container content;

        public GamebosuInputManager(RulesetInfo ruleset)
        {
            InternalChild = (keybindingContainer = new RulesetKeyBindingContainer(ruleset, 0, SimultaneousBindingMode.All).WithChild(content = new Container
            {
                RelativeSizeAxes = Axes.Both
            }));
        }

        private partial class RulesetKeyBindingContainer : DatabasedKeyBindingContainer<GamebosuAction>
        {
            protected override bool HandleRepeats => false;

            public RulesetKeyBindingContainer(RulesetInfo ruleset, int variant, SimultaneousBindingMode unique)
                : base(ruleset, variant, unique, KeyCombinationMatchingMode.Any)
            {
            }
        }
    }
}