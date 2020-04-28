﻿// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.IO;
using osuTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class RomSelector : CompositeDrawable, IKeyBindingHandler<GamebosuAction>
    {
        private IEnumerable<string> avalaible_roms;

        public Action<EmulatedCartridge> Selected;

        private readonly Sprite cartridge;

        private const double fade_time = 300;
        private const Easing easing = Easing.OutQuint;

        private RomStore roms;
        private readonly OsuSpriteText romName;
        private readonly SpriteIcon selectionLeft;
        private readonly SpriteIcon selectionRight;

        private SampleChannel selectSample;
        private SampleChannel confirmSelectSample;

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
                                Alpha = 0,
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
                                Alpha = 0,
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
        private void load(TextureStore store, RomStore roms, AudioManager audio)
        {
            this.roms = roms;

            selectSample = audio.Samples.Get("UI/generic-hover-soft");
            confirmSelectSample = audio.Samples.Get("SongSelect/confirm-selection");

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
            selectSample?.Play();

            selectionLeft.FadeIn(400, Easing.OutQuint);
            selectionRight.FadeIn(400, Easing.OutQuint);

            if (selection.NewValue == this.selection.MaxValue)
                selectionRight.FadeOut(400, Easing.OutQuint);

            if (selection.NewValue == 0)
                selectionLeft.FadeOut(400, Easing.OutQuint);

            var text = avalaible_roms.ElementAtOrDefault(selection.NewValue);

            if (text != null)
                setSelectionText(text, Color4.White);
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

        private void setSelectionText(string text, Color4 color)
        {
            romName.FadeOut(fade_time, easing)
                   .OnComplete(t =>
                   {
                       t.Text = text;
                       t.FadeColour(color, fade_time, Easing.OutQuint);
                       t.FadeIn(fade_time, Easing.OutQuint);
                   });
        }

        private void finalizeSelection(Task<EmulatedCartridge> cartridge)
        {
            confirmSelectSample?.Play();

            if (cartridge.Result != null)
                Selected?.Invoke(cartridge.Result);
            else
            {
                setSelectionText("Couldn't load cartridge. Please make sure it is a valid gameboy (color) ROM file.", Color4.Red);
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
                    {
                        roms.GetAsync(rom)
                            .ContinueWith(finalizeSelection);
                    }
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