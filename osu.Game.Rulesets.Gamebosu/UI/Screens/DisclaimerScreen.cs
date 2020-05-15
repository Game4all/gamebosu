// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class DisclaimerScreen : GamebosuScreen, IKeyBindingHandler<GamebosuAction>
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
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                TextAnchor = Anchor.Centre,
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
                t.Font = t.Font.With(size: 30f);
            });

            textFlow.AddParagraph("This is a WIP, so don't expect things to work as expected.");

            textFlow.NewParagraph();
            textFlow.NewParagraph();
            textFlow.NewParagraph();

            textFlow.AddParagraph("Press your (A) (B), (Select) (Start) button to skip this.", t =>
            {
                t.Colour = color.YellowLighter;
                t.Font = t.Font.With(size: 12f);
            });
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            Scheduler.AddDelayed(schedulePush, 5000);
        }

        public bool OnPressed(GamebosuAction action)
        {
            if (action >= GamebosuAction.ButtonA)
            {
                Scheduler.CancelDelayedTasks();
                schedulePush();
            }

            return true;
        }

        private void schedulePush()
        {
            Content.ScaleTo(0.25f, 300, Easing.OutQuint)
                   .FadeOut(300, Easing.Out)
                   .OnComplete(t => Complete?.Invoke(this));
        }

        public void OnReleased(GamebosuAction action)
        {
        }
    }
}