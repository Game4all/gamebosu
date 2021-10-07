// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class RomSelector : CompositeDrawable, IKeyBindingHandler<GamebosuAction>
    {
        public const double FADE_TIME = 300;
        public const Easing EASING = Easing.OutQuint;

        private readonly Container<SelectionCard> selectionContainer;
        private readonly SpriteIcon selectionLeft;
        private readonly SpriteIcon selectionRight;
        private readonly NoRomAvailableMessage noRomPopup;

        private Sample selectSample;
        private Sample confirmSelectSample;

        private BindableInt selection = new BindableInt(0)
        {
            MinValue = 0,
        };

        /// <summary>
        /// Called when the ROM has been selected.
        /// </summary>
        public Action<string> RomSelected;

        /// <summary>
        /// The available roms for use.
        /// Will update the selectable rom cards when updated.
        /// </summary>
        public readonly Bindable<IEnumerable<string>> AvailableRoms = new Bindable<IEnumerable<string>>(Enumerable.Empty<string>());

        /// <summary>
        /// Displays an error popup on the selected card indicating that the coresponding cartridge is unavailable.
        /// </summary>
        public void MarkUnavailable() => Scheduler.Add(() => getDrawableCardAtIndex(selection.Value)?.MarkUnavailable());

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
                            noRomPopup = new NoRomAvailableMessage(),
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
                            selectionContainer = new Container<SelectionCard>
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
        private void load(AudioManager audio)
        {
            selectSample = audio.Samples.Get("UI/generic-hover-soft");
            confirmSelectSample = audio.Samples.Get("UI/notification-pop-in");

            selection.BindValueChanged(updateSelection, true);

            AvailableRoms.BindValueChanged(roms =>
            {
                noRomPopup.State.Value = roms.NewValue.Count() > 0 ? Visibility.Hidden : Visibility.Visible;

                selectionContainer.Clear();
                selectionContainer.AddRange(roms.NewValue.Select(rom => new SelectionCard(rom)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0,
                }));

                selection.MaxValue = (roms.NewValue.Count() - 1) > 0 ? (roms.NewValue.Count() - 1) : 0;
                selection.TriggerChange();
            }, true);

            selection.BindValueChanged(updateSelectedDrawableCard, true);
        }

        /// <summary>
        /// Updates the arrows from the selector, depending of whether there are other roms available.
        /// </summary>
        private void updateSelection(ValueChangedEvent<int> selection)
        {
            selectSample?.Play();

            selectionLeft.FadeIn(FADE_TIME, EASING);
            selectionRight.FadeIn(FADE_TIME, EASING);

            if (selection.NewValue == this.selection.MaxValue)
                selectionRight.FadeOut(FADE_TIME, EASING);

            if (selection.NewValue == 0)
                selectionLeft.FadeOut(FADE_TIME, EASING);
        }

        /// <summary>
        /// Set the current selected rom card as the one at the given index.
        /// </summary>
        private void setSelection(int idx)
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

        /// <summary>
        /// Updates the visibility of the currently selected drawable card.
        /// </summary>
        private void updateSelectedDrawableCard(ValueChangedEvent<int> e)
        {
            getDrawableCardAtIndex(e.OldValue)?.FadeOut(FADE_TIME, EASING);
            getDrawableCardAtIndex(e.NewValue)?.FadeIn(2 * FADE_TIME, EASING);
        }

        private SelectionCard getDrawableCardAtIndex(int index) => (selectionContainer.Count < index || selectionContainer.Count == 0) ? null : selectionContainer[index];

        public bool OnPressed(KeyBindingPressEvent<GamebosuAction> action)
        {
            switch (action.Action)
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
                    var rom = AvailableRoms.Value.ElementAtOrDefault(selection.Value);

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

        public void OnReleased(KeyBindingReleaseEvent<GamebosuAction> action)
        {
        }
    }
}