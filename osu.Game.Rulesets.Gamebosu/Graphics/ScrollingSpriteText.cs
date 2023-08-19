// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Transforms;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Gamebosu.Graphics
{
    /// <summary>
    /// A <see cref="OsuSpriteText"/> that scrolls if its width is bigger than its parent.
    /// </summary>
    public partial class ScrollingSpriteText : OsuSpriteText
    {
        private TransformSequence<ScrollingSpriteText> scrollTransformSequence;

        private const double transform_time = 250;

        protected override void Update()
        {
            //if the width goes off of its parent width, let's just make it slide from left to right
            if (DrawWidth > Parent?.Width && scrollTransformSequence == null)
            {
                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                var speedRatio = DrawWidth / Parent.DrawWidth * 8;

                scrollTransformSequence = this.MoveToX(-(DrawWidth + 20), speedRatio * DrawWidth)
                                              .Then()
                                              .FadeOut(transform_time)
                                              .Then()
                                              .MoveToX(0)
                                              .Delay(transform_time)
                                              .FadeIn(transform_time)
                                              .Loop();
            }

            base.Update();
        }
    }
}