using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics.Containers;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class DisclaimerScreen : GamebosuScreen
    {
        private OsuTextFlowContainer textFlow;

        public DisclaimerScreen()
        {
            Child = textFlow = new OsuTextFlowContainer()
            {
                RelativeSizeAxes = Framework.Graphics.Axes.Both,
                Origin = Framework.Graphics.Anchor.Centre,
                Anchor = Framework.Graphics.Anchor.Centre,
                TextAnchor = Framework.Graphics.Anchor.Centre,
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
        }
    }
}
