// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Gamebosu.Graphics
{
    /// <summary>
    /// A <see cref="OsuSpriteText"/> that scrolls if its width is bigger than its parent.
    /// </summary>
    public class ScrollingSpriteText : OsuSpriteText
    {
        private bool done;

        private const double transform_time = 250;

        protected override void Update()
        {
            //if the width goes off of its parent width, let's just make it slide from left to right
            if (DrawWidth > Parent?.Width && !done)
            {
                Schedule(() =>
                {
                    Anchor = Anchor.CentreLeft;
                    Origin = Anchor.CentreLeft;

                    var speedRatio = DrawWidth / Parent.DrawWidth * 8;

                    this.MoveToX(-(DrawWidth + 50), speedRatio * DrawWidth)
                        .Then()
                        .FadeOut(transform_time)
                        .Then()
                        .MoveToX(0)
                        .Delay(transform_time)
                        .FadeIn(transform_time)
                        .Loop();
                });

                done = true;
            }

            base.Update();
        }
    }
}