// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Gamebosu.Objects;
using osu.Game.Rulesets.Objects;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Gamebosu.Beatmaps
{
    public class GamebosuBeatmapConverter : BeatmapConverter<GamebosuHitObject>
    {
        public GamebosuBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => true;

        protected override IEnumerable<GamebosuHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            yield return new GamebosuHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
            };
        }
    }
}