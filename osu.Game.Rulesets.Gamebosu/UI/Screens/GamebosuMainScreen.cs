// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osu.Game.Audio.Effects;
using osu.Game.Rulesets.Gamebosu.Configuration;
using osu.Game.Rulesets.Gamebosu.IO;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    public class GamebosuMainScreen : ScreenWithCyclingBeatmapBackground
    {
        private readonly GamebosuScreenStack screenStack;

        private readonly GamebosuRuleset ruleset;

        private GamebosuConfigManager config;

        private AudioFilter lowPassFilter;

        public override bool HideOverlaysOnEnter => true;

        public GamebosuMainScreen(GamebosuRuleset ruleset)
        {
            this.ruleset = ruleset;
            InternalChild = new GamebosuInputManager(ruleset.RulesetInfo)
            {
                Child = screenStack = new GamebosuScreenStack()
            };
        }

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            lowPassFilter = new AudioFilter(audio.TrackMixer);
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var container = new DependencyContainer(parent);

            container.Cache(ruleset);
            container.Cache(config = (GamebosuConfigManager)container.Get<IRulesetConfigCache>().GetConfigFor(ruleset));

            container.Get<TextureStore>().AddStore(new TextureLoaderStore(ruleset.CreateResourceStore()));

            container.Cache(new RomStore(container.Get<Storage>()));

            return container;
        }

        public override void OnEntering(IScreen last)
        {
            lowPassFilter.CutoffTo(1000, 1200, Easing.OutQuint);

            var displayDisclaimer = !config.Get<bool>(GamebosuSetting.DisableDisplayingThatAnnoyingDisclaimer);
            screenStack.Push(displayDisclaimer
                ? new DisclaimerSubScreen
                {
                    Complete = () => screenStack.Push(new ListingSubScreen())
                }
                : (GamebosuSubScreen)new ListingSubScreen());

            base.OnEntering(last);
        }

        public override bool OnExiting(IScreen next)
        {
            lowPassFilter.CutoffTo(AudioFilter.MAX_LOWPASS_CUTOFF, 300);

            if (screenStack.CurrentScreen is GameplaySubScreen)
            {
                screenStack.Exit();
                return true;
            }

            return base.OnExiting(next);
        }
    }
}