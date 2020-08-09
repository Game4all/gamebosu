// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.Online;
using osu.Game.Rulesets.Gamebosu.UI.Gameboy;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Gameplay;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GameplayScreen : GamebosuScreen
    {
        private readonly DrawableGameboy gameboy;
        private readonly Container container;
        private readonly ClockRateIndicator indicator;

        private readonly ICartridge cartridge;

        private Bindable<float> gameboyScale;

        public override UserActivityGamebosu ScreenActivity => new UserActivityGamebosuPlaying(cartridge.GameTitle);

        public GameplayScreen(ICartridge cart)
        {
            cartridge = cart;

            Children = new Drawable[]
            {
                container = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Both,
                    Child = gameboy = new DrawableGameboy(cart)
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
        }

        [BackgroundDependencyLoader]
        private void load(GamebosuConfigManager config)
        {
            gameboyScale = config.GetBindable<float>(GamebosuSetting.GameboyScale);
            gameboyScale.BindValueChanged(e => container.ScaleTo(e.NewValue, 400, Easing.OutQuint), true);
            config.BindWith(GamebosuSetting.ClockRate, indicator.Rate);

            gameboy.Start();
        }
    }
}