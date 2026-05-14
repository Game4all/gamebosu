// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy;
using Emux.GameBoy.Cartridge;
using Emux.GameBoy.Input;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.UI.Input;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    public partial class DrawableGameboy : CompositeDrawable, IKeyBindingHandler<GamebosuAction>
    {
        private readonly ICartridge cartridge;

        private readonly DrawableGameboyClock clock;

        private readonly DrawableGameboyScreen screen;

        private readonly CrashScreenCover crashScreenCover;

        private readonly Sprite cutoutSprite;

        private GameBoy gameBoy;

        private Bindable<double> clockRate;

        public DrawableGameboy(ICartridge cart)
        {
            AutoSizeAxes = Axes.Both;
            Masking = true;
            CornerRadius = 5;

            cartridge = cart;

            InternalChildren = new Drawable[]
            {
                clock = new DrawableGameboyClock(),
                cutoutSprite = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Scale = new osuTK.Vector2(0.5f),
                },
                new Container
                {
                    Padding = new MarginPadding { Left = 2 },
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        screen = new DrawableGameboyScreen
                        {
                            Size = new osuTK.Vector2(160, 144),
                            Margin = new MarginPadding()
                            {
                                Top = 20
                            },
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre
                        },
                        crashScreenCover = new CrashScreenCover
                        {
                            Alpha = 0
                        }
                    }
                },
            };
        }

        public bool OnPressed(KeyBindingPressEvent<GamebosuAction> action)
        {
            if (gameBoy == null) return false;

            gameBoy.KeyPad.PressedButtons |= getFromAction(action.Action);

            return true;
        }

        public void OnReleased(KeyBindingReleaseEvent<GamebosuAction> action)
        {
            if (gameBoy == null) return;

            gameBoy.KeyPad.PressedButtons &= ~getFromAction(action.Action);
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
        private void load(GamebosuConfigManager cfg, TextureStore textures)
        {
            var forceGbcMode = cartridge.GameBoyColorFlag == GameBoyColorFlag.GameBoyColorOnly ? true : cfg.Get<bool>(GamebosuSetting.PreferGBCMode);

            gameBoy = new GameBoy(cartridge, clock, forceGbcMode);
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

            var tex = textures.Get("Textures/dmg_sprite.png");
            cutoutSprite.Texture = tex;
            cutoutSprite.Margin = new MarginPadding
            {
                Top = tex.DisplayHeight / 2,
            };

            // deactivate all sound channels.
            gameBoy.Spu.DeactivateAllChannels();
        }

        protected override void Dispose(bool isDisposing)
        {
            gameBoy?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}