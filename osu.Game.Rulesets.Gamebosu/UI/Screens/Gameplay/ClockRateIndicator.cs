using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osuTK.Graphics;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Gameplay
{
    public class ClockRateIndicator : CompositeDrawable
    {
        public readonly BindableDouble Rate = new BindableDouble();

        private readonly OsuSpriteText rate_text;

        private const float icon_pos = 0.40f;

        public ClockRateIndicator() 
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                new SpriteIcon
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativePositionAxes = Axes.X,
                    X = -icon_pos,
                    Size = new osuTK.Vector2(20),
                    Icon = FontAwesome.Solid.Bicycle
                },
                new ValueGauge
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Width = 0.75f,
                    Height = 15,
                    Value = new BindableDouble { BindTarget = Rate },
                    BarColour = ColourInfo.GradientHorizontal(Color4.Green, Color4.LightGreen)
                },
                new SpriteIcon
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativePositionAxes = Axes.X,
                    X = icon_pos,
                    Size = new osuTK.Vector2(20),
                    Icon = FontAwesome.Solid.Truck
                },
                rate_text = new OsuSpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = "1x",
                    Font = OsuFont.GetFont(Typeface.Torus, 16, FontWeight.Bold, true),
                    Margin = new MarginPadding { Bottom = 30 }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Rate.BindValueChanged(updateText, true);
        }

        private void updateText(ValueChangedEvent<double> e)
        {
            rate_text.Text = $"{Math.Round(e.NewValue, 2, MidpointRounding.AwayFromZero)}x";
        }
    }
}
