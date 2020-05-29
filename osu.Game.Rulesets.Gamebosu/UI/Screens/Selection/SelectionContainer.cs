// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class SelectionContainer : Container<SelectionCard>
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

        public override void Add(SelectionCard drawable)
        {
            drawable.Hide();
            base.Add(drawable);
        }

        public SelectionCard GetSelection(int index)
        {
            if (Count < index || Count == 0)
                return null;

            return this[index];
        }

        private void updateVisibleDrawable(ValueChangedEvent<int> e)
        {
            GetSelection(e.OldValue)?.FadeOut(fade_time, easing);
            GetSelection(e.NewValue)?.FadeIn(2 * fade_time, easing);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Current.BindValueChanged(updateVisibleDrawable, false);
        }
    }
}