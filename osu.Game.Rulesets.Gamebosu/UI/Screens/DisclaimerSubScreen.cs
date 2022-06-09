// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osu.Framework.Utils;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osuTK.Graphics;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class DisclaimerSubScreen : GamebosuSubScreen, IKeyBindingHandler<GamebosuAction>
    {
        private readonly OsuTextFlowContainer textFlow;

        private static readonly string[] disclaimer_tips =
        {
            "You can delete ROM save data from the settings overlay, try searching for 'delete ROM save data'!",
            "Try pressing Page-up or Page-down to change the ROM emulation speed!",
            "You can customize the gameboy screen scale from the settings overlay, try searching for 'gameboy scale'!",
            "You can open the ROM folder from the settings overlay, try searching for 'open rom folder'!",
            "You can enable audio playback of the gameboy speaker in the settings, but don't do for the time being. It currently sounds more like noise.",
            "You can disable this disclaimer in the settings, try searching for 'disable that annoying startup disclaimer'!"
        };

        /// <summary>
        /// Called when the disclaimer finished displaying.
        /// </summary>
        public Action Complete;

        public DisclaimerSubScreen()
        {
            Child = textFlow = new OsuTextFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                TextAnchor = Anchor.Centre,
            };

            Child = new Container
            {
                AutoSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Masking = true,
                CornerRadius = 16,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Gray.Opacity(0.4f)
                    },
                    textFlow = new OsuTextFlowContainer
                    {
                        Margin = new MarginPadding(16),
                        AutoSizeAxes = Axes.Both,
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        TextAnchor = Anchor.Centre,
                    }
                }
            };

            ValidForResume = false;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour color)
        {
            textFlow.AddIcon(FontAwesome.Solid.Wrench, t =>
            {
                t.Font = t.Font.With(size: 50);
            });

            textFlow.NewParagraph();

            textFlow.AddParagraph("Disclaimer", t =>
            {
                t.Font = t.Font.With(size: 30f);
            });

            textFlow.AddParagraph("This is a WIP, so don't expect things to work as expected.");

            textFlow.AddParagraph("Tip: " + disclaimer_tips[RNG.Next(0, disclaimer_tips.Length)], t =>
            {
                t.Colour = color.BlueLighter;
            });

            textFlow.NewParagraph();

            textFlow.AddParagraph("Press your (A) (B), (Select) (Start) button to skip this.", t =>
            {
                t.Colour = color.YellowLighter;
                t.Font = t.Font.With(size: 12f);
            });
        }

        public override void OnEntering(ScreenTransitionEvent last)
        {
            base.OnEntering(last);
            Scheduler.AddDelayed(schedulePush, 5000);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Scheduler.CancelDelayedTasks();
            schedulePush();
            return base.OnClick(e);
        }

        public bool OnPressed(KeyBindingPressEvent<GamebosuAction> action)
        {
            if (action.Action >= GamebosuAction.ButtonA && GamebosuAction.ButtonIncrementClockRate > action.Action)
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
                   .OnComplete(t => Complete?.Invoke());
        }

        public void OnReleased(KeyBindingReleaseEvent<GamebosuAction> action)
        {
        }
    }
}