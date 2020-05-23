using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class SelectionContainer : Container
    {
        private const double fade_time = 300;
        private const Easing easing = Easing.OutQuint;

        public readonly BindableInt Current = new BindableInt(0)
        {
            MinValue = 0,
        };

        public SelectionContainer()
        {
        }

        public override void Add(Drawable drawable)
        {
            drawable.Hide();
            base.Add(drawable);
        }

        private void updateVisibleDrawable(ValueChangedEvent<int> e)
        {
            this[e.OldValue].FadeOut(fade_time, easing);
            this[e.NewValue].FadeIn(2*fade_time, easing);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Current.BindValueChanged(updateVisibleDrawable, false);
        }
    }
}