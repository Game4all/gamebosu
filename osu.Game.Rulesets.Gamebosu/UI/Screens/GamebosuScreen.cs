using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuScreen : Container, IScreen
    {
        public override bool RemoveWhenNotAlive => false;

        public GamebosuScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
        }

        public bool ValidForResume { get; set; } = false;
        public bool ValidForPush { get; set; } = true;

        public void OnEntering(IScreen last)
        {
            Content
                .ScaleTo(0.5f)
                .ScaleTo(1, 500, Easing.OutQuint)
                .FadeInFromZero(500, Easing.OutQuint);
        }

        public bool OnExiting(IScreen next)
        {
            return false;
        }

        public void OnResuming(IScreen last)
        {
        }

        public void OnSuspending(IScreen next)
        {
        }
    }
}
