using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Gameplay
{
    /// <summary>
    /// An always present drawable handling global actions for the clock rate indicator.
    /// This is required as hidden drawables won't receive keybindings actions.
    /// </summary>
    public class ClockRateIndicatorControlReceptor : Drawable, IKeyBindingHandler<GamebosuAction>
    {
        public Action<double> AdjustAction;

        public bool OnPressed(GamebosuAction action)
        {
            switch (action)
            {
                case GamebosuAction.ButtonIncrementClockRate:
                    AdjustAction(0.1);
                    return true;

                case GamebosuAction.ButtonDecrementClockRate:
                    AdjustAction(-0.1);
                    return true;

                default:
                    return false;
            }
        }

        public void OnReleased(GamebosuAction action)
        {
        }
    }
}