using System.ComponentModel;

namespace osu.Game.Rulesets.Gamebosu.UI.Input
{
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

        [Description("Increment clock rate")]
        ButtonIncrementClockRate,

        [Description("Decrement clock rate")]
        ButtonDecrementClockRate
    }
}
