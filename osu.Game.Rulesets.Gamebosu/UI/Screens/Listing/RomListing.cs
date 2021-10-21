// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Containers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Listing
{
    public class RomListing : Container
    {
        private readonly Bindable<IEnumerable<string>> availableRoms = new Bindable<IEnumerable<string>>(Enumerable.Empty<string>());

        private readonly FillFlowContainer fillFlowContainer;
        private readonly OsuScrollContainer scrollContainer;
        private readonly NoRomAvailablePopup romNotFound;

        /// <summary>
        /// A callback called upon selection of a ROM by the user.
        /// </summary>
        public Action<string> RomSelected;

        public IEnumerable<string> AvailableRoms
        {
            set
            {
                if (!Enumerable.SequenceEqual(value, availableRoms.Value))
                    availableRoms.Value = value;
            }
        }

        public RomListing()
        {
            Child = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    romNotFound = new NoRomAvailablePopup(),
                    scrollContainer = new OsuScrollContainer(Direction.Vertical)
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = fillFlowContainer = new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding { Horizontal = 15 },
                            Spacing = new osuTK.Vector2(15, 0),
                        }
                    },                    
                }
            };
        }

        protected override void LoadComplete()
        {
            availableRoms.BindValueChanged(refreshDisplay, true);
            base.LoadComplete();
        }

        private void refreshDisplay(ValueChangedEvent<IEnumerable<string>> roms)
        {
            fillFlowContainer.Clear();
            romNotFound.State.Value = roms.NewValue.Count() > 0 ? Visibility.Hidden : Visibility.Visible;
            roms.NewValue.ForEach(each => fillFlowContainer.Add(new ListingPanel(each)
            {
                Action = () => RomSelected(each),
                Margin = new MarginPadding { Top = 5 }
            }));
        }

    }
}
