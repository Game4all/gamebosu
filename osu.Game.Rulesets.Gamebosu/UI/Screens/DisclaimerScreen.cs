using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
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
        private void load(OsuColour color)
        {
            textFlow.AddIcon(FontAwesome.Solid.InfoCircle, t =>
            {
                t.Font = t.Font.With(size: 50);
            });
            textFlow.NewParagraph();
            textFlow.AddParagraph("Disclaimer", delegate (SpriteText t)
            {
                t.Font = OsuFontExtensions.With(t.Font, Typeface.Torus, size: 30, weight: FontWeight.Bold);
            });
            textFlow.AddParagraph("This is a WIP, so don't expect things to work as expected.");
            textFlow.AddParagraph("For now, please use beatmaps without breaks for the best experience!", delegate (SpriteText t)
            {
                t.Colour = color.Blue;
            });
            textFlow.NewParagraph();
        }
    }
}
