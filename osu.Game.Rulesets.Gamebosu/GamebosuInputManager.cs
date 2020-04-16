// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Gamebosu
{
    public class GamebosuInputManager : RulesetInputManager<GamebosuAction>
    {
        public GamebosuInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum GamebosuAction
    {
        [Description("DPad Right")]
        DPadRight,

        [Description("DPad Left")]
        DPadLeft,

        [Description("DPad Up")]
        DPadUp,

        [Description("DPad Down")]
        DPadDown,

        [Description("A Button")]
        ButtonA,

        [Description("B Button")]
        ButtonB,

        [Description("Start Button")]
        ButtonStart,

        [Description("Select Button")]
        ButtonSelect,
    }
}
