﻿using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Screens;
using osu.Game.Overlays.Toolbar;
using osu.Game.Rulesets.Gamebosu.UI.Screens;
using osu.Game.Screens.Play;

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
        private void load(OsuGame game, ILocalUserPlayInfo playing, TextureStore textures)
        {
            SetIcon(new Sprite
            {
                Texture = textures.Get("Textures/gamebosu_toolbar.png")
            });

            Action = () =>
            {

                if (playing.PlayingState.Value == LocalUserPlayingState.NotPlaying)
                    game.PerformFromScreen(scr => scr.Push(new GamebosuMainScreen(ruleset)));
            };
        }
    }
}
