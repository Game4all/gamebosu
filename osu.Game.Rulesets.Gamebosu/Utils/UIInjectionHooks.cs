using System.Reflection;
using osu.Framework.Graphics.Containers;
using osu.Game.Overlays.Toolbar;
using osu.Game.Rulesets.Gamebosu.Graphics;

namespace osu.Game.Rulesets.Gamebosu.Utils
{
    internal static class UIInjectionHook
    {
        /// <summary>
        /// Inject a clickable icon into the game toolbar.
        /// </summary>
        public static void InjectToolbarIcon(OsuGame game, GamebosuRuleset ruleset)
        {
            // we're hooking the toolbar load
            game.Toolbar.OnLoadComplete += _ =>
            {
                var userToolbarButton = typeof(Toolbar)
                                        .GetField("userButton", BindingFlags.Instance | BindingFlags.NonPublic)?
                                        .GetValue(game.Toolbar) as ToolbarUserButton;

                if (userToolbarButton?.Parent is FillFlowContainer flow)
                    flow.Insert(-1, new GamebosuToolbarIcon(ruleset));
            };
        }
    }
}
