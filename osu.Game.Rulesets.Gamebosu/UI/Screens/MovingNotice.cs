// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class MovingNotice : Container
    {
        private readonly OsuTextFlowContainer textFlow;

        public MovingNotice()
        {
            Masking = true;
            CornerRadius = 15;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Gray.Opacity(0.4f)
                },
                textFlow = new OsuTextFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    TextAnchor = Anchor.Centre,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(SettingsOverlay settings)
        {
            textFlow.AddIcon(FontAwesome.Solid.DoorOpen, t =>
            {
                t.Font = t.Font.With(size: 50);
            });

            textFlow.NewLine();

            textFlow.AddParagraph("gamebosu! moved to the settings overlay", t => 
            {
                t.Font = t.Font.With(size: 24);
                t.Colour = Color4.Yellow;
            });
            textFlow.AddParagraph("Open the settings to access the rom listing", t => t.Font = t.Font.With(size: 16));
            textFlow.AddParagraph("Search for \"open rom listing\" ", t => t.Font = t.Font.With(size: 12));
        }
    }
}