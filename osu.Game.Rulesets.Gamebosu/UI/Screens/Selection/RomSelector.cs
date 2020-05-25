// gamebosu! ruleset. Licensed under GPLv3.
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
using osu.Game.Rulesets.Gamebosu.IO;
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

        private const double fade_time = 300;
        private const Easing easing = Easing.OutQuint;

        private RomStore roms;
        private readonly SelectionContainer selectionContainer;
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
                        Height = 400,
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

        [BackgroundDependencyLoader]
        private void load(TextureStore store, RomStore roms, AudioManager audio)
        {
            this.roms = roms;

            selectSample = audio.Samples.Get("UI/generic-hover-soft");
            confirmSelectSample = audio.Samples.Get("SongSelect/confirm-selection");

            avalaible_roms = roms.GetAvailableResources();

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

            selectionContainer.Current.BindTo(selection);
            selection.BindValueChanged(updateSelection, true);
            selectionContainer.Current.TriggerChange();
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
            }
            catch (Exception)
            {
            }
        }

        private void finalizeSelection(Task<EmulatedCartridge> cartridge)
        {
            confirmSelectSample?.Play();

            if (cartridge.Result != null)
                Selected?.Invoke(cartridge.Result);
            else
                selectionContainer[selection.Value].MarkUnavalaible();
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