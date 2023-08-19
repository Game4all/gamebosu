using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Game.Overlays.Toolbar;
using osu.Game.Rulesets.Gamebosu.UI.Screens;

namespace osu.Game.Rulesets.Gamebosu.Graphics
{
    public partial class GamebosuToolbarIcon : ToolbarButton
    {
        private readonly GamebosuRuleset ruleset;

        public GamebosuToolbarIcon(GamebosuRuleset ruleset)
        {
            this.ruleset = ruleset;
            TooltipMain = "gamebosu";
            TooltipSub = "Open the ROM selection screen";
        }

        [BackgroundDependencyLoader]
        private void load(OsuGame game)
        {
            SetIcon("Textures/gamebosu_toolbar.png");
            Action = () =>
            {
                if (!game.LocalUserPlaying.Value)
                    game.PerformFromScreen(scr => scr.Push(new GamebosuMainScreen(ruleset)));
            };
        }
    }
}
