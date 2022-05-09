// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuSubScreen : Container, IScreen
    {
        public override bool RemoveWhenNotAlive => false;

        public GamebosuSubScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
        }

        public virtual void OnEntering(ScreenTransitionEvent e)
        {
            Content
                .ScaleTo(0.5f)
                .ScaleTo(1, 500, Easing.OutQuint)
                .FadeInFromZero(500, Easing.OutQuint);
        }

        public virtual bool OnExiting(ScreenExitEvent e)
        {
            Content
                .ScaleTo(1)
                .ScaleTo(0.5f, 500, Easing.OutQuint)
                .FadeOutFromOne(500, Easing.OutQuint);

            return false;
        }

        public virtual void OnResuming(ScreenTransitionEvent e)
        {
        }

        public virtual void OnSuspending(ScreenTransitionEvent e)
        {
        }

        public bool ValidForResume { get; set; } = true;
        public bool ValidForPush { get; set; } = true;

    }
}