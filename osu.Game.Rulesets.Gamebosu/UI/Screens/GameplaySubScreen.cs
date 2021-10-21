// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.UI.Gameboy;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Gameplay;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GameplaySubScreen : GamebosuSubScreen
    {
        private DrawableGameboy gameboy;
        private Container container;
        private ClockRateIndicator indicator;

        private readonly ICartridge cartridge;

        private Bindable<float> gameboyScale;

        public GameplaySubScreen(ICartridge cart)
        {
            cartridge = cart;
        }

        [BackgroundDependencyLoader]
        private void load(GamebosuConfigManager config)
        {
            Children = new Drawable[]
            {
                container = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Both,
                    Child = gameboy = new DrawableGameboy(cartridge)
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                },
                new ClockRateIndicatorControlReceptor
                {
                    AdjustAction = (f) => indicator.AdjustRate(f),
                },
                indicator = new ClockRateIndicator
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Bottom = 20 },
                    Alpha = 0,
                }
            };

            gameboyScale = config.GetBindable<float>(GamebosuSetting.GameboyScale);
            gameboyScale.BindValueChanged(e => container.ScaleTo(e.NewValue, 400, Easing.OutQuint), true);
            config.BindWith(GamebosuSetting.ClockRate, indicator.Rate);

            gameboy.Start();
        }
    }
}