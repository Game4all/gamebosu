// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under MIT. 
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class RomSelector : CompositeDrawable, IKeyBindingHandler<GamebosuAction>
    {
        private IEnumerable<string> avalaible_roms;

        public Action<EmulatedCartridge> Selected;

        private readonly Sprite cartridge;

        private RomStore roms;
        private readonly OsuSpriteText romName;
        private readonly SpriteIcon selectionLeft;
        private readonly SpriteIcon selectionRight;

        private BindableInt selection;

        public RomSelector()
        {
            RelativeSizeAxes = Axes.Both;

            InternalChild = new FillFlowContainer
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Spacing = new osuTK.Vector2(0, 0.1f),
                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 300,
                        Children = new Drawable[]
                        {
                            selectionLeft = new SpriteIcon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativePositionAxes = Axes.X,
                                X = -0.25f,
                                Size = new osuTK.Vector2(40),
                                Icon = FontAwesome.Solid.ChevronLeft,
                            },
                            cartridge = new Sprite
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Scale = new osuTK.Vector2(2f)
                            },
                            selectionRight = new SpriteIcon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativePositionAxes = Axes.X,
                                X = 0.25f,
                                Size = new osuTK.Vector2(40),
                                Icon = FontAwesome.Solid.ChevronRight,
                            },
                        }
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Child = romName = new OsuSpriteText
                        {
                            Font = OsuFont.GetFont(Typeface.Torus, 24, FontWeight.SemiBold),
                            Text = "No avalaible rom found!",
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore store, RomStore roms)
        {
            this.roms = roms;

            cartridge.Texture = store.Get("cartridge");
            avalaible_roms = roms.GetAvailableResources();

            selection = new BindableInt(0)
            {
                MinValue = 0,
                MaxValue = ((avalaible_roms.Count() - 1) >= 0 ? (avalaible_roms.Count() - 1) : 0)
            };

            selection.BindValueChanged(updateSelection, true);
        }

        private void updateSelection(ValueChangedEvent<int> selection)
        {
            selectionLeft.FadeIn(200, Easing.OutQuint);
            selectionRight.FadeIn(200, Easing.OutQuint);

            if (selection.NewValue == this.selection.MaxValue)
                selectionRight.FadeOut(200, Easing.OutQuint);

            if (selection.NewValue == 0)
                selectionLeft.FadeOut(200, Easing.OutQuint);

            romName
                .FadeOut(200, Easing.OutQuint)
                .OnComplete(t =>
                {
                    var text = avalaible_roms.ElementAtOrDefault(selection.NewValue);

                    if (text != null)
                        t.Text = text;

                    t.FadeIn(200, Easing.OutQuint);
                });
        }

        private void setSelection(int idx)
        {
            try
            {
                selection.Value += idx;
            }
            catch (Exception)
            {
            }
        }

        public bool OnPressed(GamebosuAction action)
        {
            switch (action)
            {
                case GamebosuAction.DPadRight:
                    setSelection(1);
                    break;
                case GamebosuAction.DPadLeft:
                    setSelection(-1);
                    break;

                case GamebosuAction.ButtonA:
                case GamebosuAction.ButtonStart:
                case GamebosuAction.ButtonSelect:
                    var rom = avalaible_roms.ElementAtOrDefault(selection.Value);
                    if (rom != null)
                        roms.GetAsync(rom)
                            .ContinueWith(t => Selected?.Invoke(t.Result));
                    break;

                default:
                    break;
            }

            return true;
        }

        public void OnReleased(GamebosuAction action)
        {
        }
    }
}