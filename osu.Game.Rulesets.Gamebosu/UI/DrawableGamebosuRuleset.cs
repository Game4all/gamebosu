// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Framework.Logging;
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

namespace osu.Game.Rulesets.Gamebosu.UI
{
    [Cached]
    public class DrawableGamebosuRuleset : DrawableRuleset<GamebosuHitObject>
    {
        public DrawableGamebosuRuleset(GamebosuRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new GamebosuPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new GamebosuFramedReplayInputHandler(replay);

        public override DrawableHitObject<GamebosuHitObject> CreateDrawableRepresentation(GamebosuHitObject h) => new DrawableGamebosuHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new GamebosuInputManager(Ruleset?.RulesetInfo);

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var deps = new DependencyContainer(base.CreateChildDependencies(parent));
            deps.Cache(new RomStore(deps.Get<Storage>()));
            return deps;
        }
    }
}
