// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;
using System.ComponentModel;

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