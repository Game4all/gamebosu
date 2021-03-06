﻿// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
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

        public bool ValidForResume { get; set; } = false;
        public bool ValidForPush { get; set; } = true;

        public virtual void OnEntering(IScreen last)
        {
            Content
                .ScaleTo(0.5f)
                .ScaleTo(1, 500, Easing.OutQuint)
                .FadeInFromZero(500, Easing.OutQuint);
        }

        public virtual bool OnExiting(IScreen next)
        {
            return false;
        }

        public virtual void OnResuming(IScreen last)
        {
        }

        public virtual void OnSuspending(IScreen next)
        {
        }
    }
}