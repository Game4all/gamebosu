﻿using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class DisclaimerScreen : GamebosuScreen
    {
        private OsuTextFlowContainer textFlow;

        /// <summary>
        /// Called when the disclaimer finished displaying.
        /// </summary>
        public Action<GamebosuScreen> Complete;

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
            textFlow.AddParagraph("Disclaimer", t =>
            {
                t.Font = OsuFontExtensions.With(t.Font, Typeface.Torus, size: 30, weight: FontWeight.Bold);
            });
            textFlow.AddParagraph("This is a WIP, so don't expect things to work as expected.");
            textFlow.AddParagraph("For now, please use beatmaps without breaks for the best experience!", t =>
            {
                t.Colour = color.Blue;
            });
            textFlow.NewParagraph();
            textFlow.AddParagraph("Please also disable the HUD for a better immersion.", t =>
            {
                t.Colour = color.Blue;
            });
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            Scheduler.AddDelayed(() => Complete?.Invoke(this), 5000, false);
        }
    }
}
