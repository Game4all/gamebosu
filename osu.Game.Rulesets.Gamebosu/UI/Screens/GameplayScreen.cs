// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.UI.Gameboy;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GameplayScreen : GamebosuScreen
    {
        private readonly DrawableGameboy gameboy;

        private Bindable<float> gameboyScale;

        public GameplayScreen(ICartridge cart)
        {
            Child = gameboy = new DrawableGameboy(cart)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
            };
        }

        [BackgroundDependencyLoader]
        private void load(GamebosuConfigManager config)
        {
            gameboyScale = config.GetBindable<float>(GamebosuSetting.GameboyScale);
            gameboyScale.BindValueChanged(e => gameboy.ScaleTo(e.NewValue, 400, Easing.OutQuint), true);
            gameboy.Start();
        }
    }
}