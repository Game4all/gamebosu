// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Gameplay
{
    public class ValueGauge : CompositeDrawable
    {
        public BindableDouble Value = new BindableDouble(0);

        private const double transition_time = 400;

        private readonly Container sliderContainer;

        private readonly Box bgBox;

        private readonly Box gaugeBox;

        public ColourInfo BackgroundColor
        {
            get => bgBox.Colour;
            set => bgBox.Colour = value;
        }

        public ColourInfo BarColour
        {
            get => gaugeBox.Colour;
            set => gaugeBox.Colour = value;
        }

        public ValueGauge()
        {
            Masking = true;
            CornerRadius = 8;
            InternalChildren = new Drawable[]
            {
                bgBox = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Gray
                },
                sliderContainer = new Container
                {
                    Masking = true,
                    CornerRadius = 8,
                    RelativeSizeAxes = Axes.Both,
                    Child = gaugeBox =  new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.White,
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Value.BindValueChanged(updateGauge, true);
        }

        private void updateGauge(ValueChangedEvent<double> e)
        {
            var width = (float)Math.Max(0, (e.NewValue / Value.MaxValue));
            sliderContainer.ResizeWidthTo(width, 1.5 * transition_time, Easing.OutQuint);
        }
    }
}