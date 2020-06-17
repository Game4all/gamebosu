// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class RomSelector : CompositeDrawable, IKeyBindingHandler<GamebosuAction>
    {
        private IEnumerable<string> avalaible_roms;

        public IEnumerable<string> AvalaibleRoms
        {
            set
            {
                if (value.Count() == 0)
                    return;

                avalaible_roms = value;

                foreach (var item in avalaible_roms)
                {
                    selectionContainer.Add(new SelectionCard(item)
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    });
                }

                selection = new BindableInt(0)
                {
                    MinValue = 0,
                    MaxValue = ((avalaible_roms.Count() - 1) >= 0 ? (avalaible_roms.Count() - 1) : 0)
                };

                noRomContainer.Hide();
                selectionContainer.Current.BindTo(selection);
                selection.BindValueChanged(updateSelection, true);
                selectionContainer.Current.TriggerChange();
            }
        }

        public Action<EmulatedCartridge> Selected;

        /// <summary>
        /// Called when the ROM has been selected.
        /// </summary>
        public Action<string> RomSelected;

        private const double fade_time = 300;
        private const Easing easing = Easing.OutQuint;

        private readonly SelectionContainer selectionContainer;
        private readonly SpriteIcon selectionLeft;
        private readonly SpriteIcon selectionRight;
        private readonly Container noRomContainer;

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
                        Height = 400,
                        Children = new Drawable[]
                        {
                            noRomContainer = new Container
                            {
                                RelativeSizeAxes = Axes.Y,
                                AutoSizeAxes = Axes.X,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Children = new Drawable[]
                                {
                                    new SpriteIcon
                                    {
                                        Icon = FontAwesome.Solid.SadCry,
                                        RelativeSizeAxes = Axes.Y,
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Size = new osuTK.Vector2(120)
                                    },
                                    new OsuSpriteText
                                    {
                                        Anchor = Anchor.BottomCentre,
                                        Origin = Anchor.BottomCentre,
                                        Margin = new MarginPadding() { Bottom = 20 },
                                        Font = OsuFont.GetFont(Typeface.Torus, 28, FontWeight.Bold),
                                        Text = "Sadly there's no usable ROM avalaible ...",
                                    },
                                    new OsuSpriteText
                                    {
                                        Anchor = Anchor.BottomCentre,
                                        Origin = Anchor.BottomCentre,
                                        Font = OsuFont.GetFont(Typeface.Torus, 16, FontWeight.Regular),
                                        Text = "Go grab some ROM files and put 'em in the roms folder",
                                    }
                                }
                            },
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
                            selectionContainer = new SelectionContainer
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
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
                }
            };
        }

        public void MarkUnavalaible() => Scheduler.Add(() => selectionContainer.GetSelection(selection.Value)?.MarkUnavalaible());

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            selectSample = audio.Samples.Get("UI/generic-hover-soft");
            confirmSelectSample = audio.Samples.Get("SongSelect/confirm-selection");
        }

        private void updateSelection(ValueChangedEvent<int> selection)
        {
            selectSample?.Play();

            selectionLeft.FadeIn(fade_time, easing);
            selectionRight.FadeIn(fade_time, easing);

            if (selection.NewValue == this.selection.MaxValue)
                selectionRight.FadeOut(fade_time, easing);

            if (selection.NewValue == 0)
                selectionLeft.FadeOut(fade_time, easing);
        }

        private void setSelection(int idx)
        {
            try
            {
                selection.Value += idx;
                if (idx == 1)
                {
                    selectionRight
                        .ScaleTo(1.5f, 150, Easing.OutQuint)
                        .Then(0)
                        .ScaleTo(1, 150, Easing.OutQuint);
                }
                else
                {
                    selectionLeft
                        .ScaleTo(1.5f, 150, Easing.OutQuint)
                        .Then(0)
                        .ScaleTo(1, 150, Easing.OutQuint);
                }
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
                    var rom = avalaible_roms?.ElementAtOrDefault(selection.Value);

                    if (rom == null)
                        goto default;

                    confirmSelectSample?.Play();
                    RomSelected?.Invoke(rom);
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