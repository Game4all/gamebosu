// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.UI.Screens;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Gamebosu.UI
{
    [Cached]
    public class GamebosuPlayfield : Playfield
    {
        private GamebosuScreenStack stack;

        [Resolved]
        private GamebosuConfigManager gamebosuConfig { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                HitObjectContainer,
                stack = new GamebosuScreenStack()
            });
        }

        private void displaySelection() => stack.Push(new RomSelectionSubScreen());

        protected override void LoadComplete()
        {
            if (!gamebosuConfig.Get<bool>(GamebosuSetting.DisableDisplayingThatAnnoyingDisclaimer))
            {
                stack.Push(new DisclaimerSubScreen
                {
                    Complete = displaySelection
                });
            }
            else
            {
                displaySelection();
            }

            base.LoadComplete();
        }
    }
}