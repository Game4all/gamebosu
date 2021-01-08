// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy;
using Emux.GameBoy.Cartridge;
using Emux.GameBoy.Input;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Game.Rulesets.Gamebosu.Audio;
using osu.Game.Rulesets.Gamebosu.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    public class DrawableGameboy : CompositeDrawable, IKeyBindingHandler<GamebosuAction>
    {
        private readonly ICartridge cartridge;

        private readonly DrawableGameboyClock clock;

        private readonly DrawableGameboyScreen screen;

        private readonly CrashScreenCover crashScreenCover;

        private GameBoy gameBoy;

        private IEnumerable<BASSAudioChannelOutput> audioChannels;

        private Bindable<double> clockRate;

        private Bindable<bool> soundPlayback;

        public DrawableGameboy(ICartridge cart)
        {
            AutoSizeAxes = Axes.Both;
            Masking = true;
            CornerRadius = 5;

            cartridge = cart;

            InternalChildren = new Drawable[]
            {
                clock = new DrawableGameboyClock(),
                screen = new DrawableGameboyScreen
                {
                    Size = new osuTK.Vector2(160, 144),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                crashScreenCover = new CrashScreenCover
                {
                    Alpha = 0
                }
            };
        }

        public bool OnPressed(GamebosuAction action)
        {
            if (gameBoy == null) return false;

            gameBoy.KeyPad.PressedButtons |= getFromAction(action);

            return true;
        }

        public void OnReleased(GamebosuAction action)
        {
            if (gameBoy == null) return;

            gameBoy.KeyPad.PressedButtons &= ~getFromAction(action);
        }

        public void Start()
        {
            screen.Clear();

            if (!gameBoy.Cpu.Running)
                gameBoy.Run();
        }

        private GameBoyPadButton getFromAction(GamebosuAction action) => action switch
        {
            GamebosuAction.ButtonA => GameBoyPadButton.A,
            GamebosuAction.ButtonB => GameBoyPadButton.B,
            GamebosuAction.DPadUp => GameBoyPadButton.Up,
            GamebosuAction.DPadDown => GameBoyPadButton.Down,
            GamebosuAction.DPadRight => GameBoyPadButton.Right,
            GamebosuAction.DPadLeft => GameBoyPadButton.Left,
            GamebosuAction.ButtonStart => GameBoyPadButton.Start,
            GamebosuAction.ButtonSelect => GameBoyPadButton.Select,
            _ => 0
        };

        [BackgroundDependencyLoader]
        private void load(GamebosuConfigManager cfg, AudioManager mng)
        {
            var forceGBCMode = cartridge.GameBoyColorFlag == GameBoyColorFlag.GameBoyColorOnly ? true : cfg.Get<bool>(GamebosuSetting.PreferGBCMode);

            gameBoy = new GameBoy(cartridge, clock, forceGBCMode);
            gameBoy.Gpu.VideoOutput = screen;

            gameBoy.Terminated += (_, e) =>
            {
                if (e.Crashed)
                {
                    Schedule(() =>
                    {
                        screen.Clear();
                        crashScreenCover.FadeIn(300, Easing.OutQuint);
                    });
                    Logger.Log($"Emulation crashed with exception: {e.Exception}", LoggingTarget.Runtime);
                }
            };

            foreach (var channel in gameBoy.Spu.Channels)
            {
                var Bchannel = new BASSAudioChannelOutput();
                channel.ChannelOutput = Bchannel;
                mng.AddItem(Bchannel);
            }

            audioChannels = gameBoy.Spu.Channels.Select(t => t.ChannelOutput).OfType<BASSAudioChannelOutput>();

            clockRate = cfg.GetBindable<double>(GamebosuSetting.ClockRate);
            clock.Rate.BindTo(clockRate);

            soundPlayback = cfg.GetBindable<bool>(GamebosuSetting.EnableSoundPlayback);
            soundPlayback.BindValueChanged(t =>
            {
                if (t.NewValue)
                    audioChannels.ForEach(ch => ch.Play());
                else
                    audioChannels.ForEach(ch => ch.Stop());
            }, true);
        }

        protected override void Dispose(bool isDisposing)
        {
            gameBoy?.Terminate();
            gameBoy?.Dispose();

            foreach (var channel in audioChannels)
                channel.Dispose();

            base.Dispose(isDisposing);
        }
    }
}