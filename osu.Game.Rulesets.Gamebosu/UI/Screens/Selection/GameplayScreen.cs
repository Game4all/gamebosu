// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under MIT. 
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Game.Rulesets.Gamebosu.UI.Gameboy;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class GameplayScreen : GamebosuScreen
    {
        private readonly DrawableGameboy gameboy;

        public GameplayScreen(ICartridge cart)
        {
            Child = gameboy = new DrawableGameboy(cart)
            {
                Anchor = Framework.Graphics.Anchor.Centre,
                Origin = Framework.Graphics.Anchor.Centre,
                RelativeSizeAxes = Framework.Graphics.Axes.Both,
                Scale = new osuTK.Vector2(3f)
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            gameboy.Start();
        }
    }
}
