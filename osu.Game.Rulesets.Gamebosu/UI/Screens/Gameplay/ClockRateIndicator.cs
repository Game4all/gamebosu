// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Threading;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osuTK.Graphics;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Gameplay
{
    public partial class ClockRateIndicator : VisibilityContainer
    {
        public readonly BindableDouble Rate = new BindableDouble();

        private readonly OsuSpriteText rateText;

        private ScheduledDelegate hideDelegate;

        private Bindable<bool> lockClockRate;

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
                    BarColour = Color4.White
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
                rateText = new OsuSpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = "1x",
                    Font = OsuFont.GetFont(Typeface.Torus, 16, FontWeight.Bold, true),
                    Margin = new MarginPadding { Bottom = 25 }
                }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load(GamebosuConfigManager config)
        {
            Rate.BindValueChanged(updateValue, true);

            lockClockRate = config?.GetBindable<bool>(GamebosuSetting.LockClockRate);
            lockClockRate?.BindValueChanged(e => State.Value = e.NewValue ? Visibility.Hidden : Visibility.Visible, true);
        }

        private void updateValue(ValueChangedEvent<double> e)
        {
            if (Rate.Disabled)
                return;

            rateText.Text = $"{Math.Round(e.NewValue, 2, MidpointRounding.AwayFromZero)}x";
            Show();
        }

        public void AdjustRate(double delta)
        {
            if (Rate.Disabled)
                return;

            try
            {
                Rate.Value += delta;
            }
            catch (Exception)
            {
            }
        }

        private void scheduleHide()
        {
            hideDelegate?.Cancel();
            this.Delay(2000).Schedule(Hide, out hideDelegate);
        }

        protected override void PopIn()
        {
            Content.FadeIn(300, Easing.OutQuint);
            scheduleHide();
        }

        protected override void PopOut() => Content.FadeOut(600, Easing.OutQuint);
    }
}