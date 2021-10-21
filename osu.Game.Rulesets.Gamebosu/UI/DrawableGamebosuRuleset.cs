// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Framework.Platform;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Gamebosu.IO;
using osu.Game.Rulesets.Gamebosu.Objects;
using osu.Game.Rulesets.Gamebosu.Objects.Drawables;
using osu.Game.Rulesets.Gamebosu.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Gamebosu.UI
{
    [Cached]
    public class DrawableGamebosuRuleset : DrawableRuleset<GamebosuHitObject>
    {
        public DrawableGamebosuRuleset(GamebosuRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
            //should permit opening overlays while playing.
            HasReplayLoaded.Value = true;
        }

        protected override Playfield CreatePlayfield() => new GamebosuPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new GamebosuFramedReplayInputHandler(replay);

        public override DrawableHitObject<GamebosuHitObject> CreateDrawableRepresentation(GamebosuHitObject h) => new DrawableGamebosuHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new GamebosuInputManager(Ruleset?.RulesetInfo);

        public override bool AllowGameplayOverlays => false;
    }
}