// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Gamebosu.Objects;
using osu.Game.Rulesets.Gamebosu.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Scoring;
using osu.Game.Users;

namespace osu.Game.Rulesets.Gamebosu.Mods
{
    public class GamebosuModAutoplay : ModAutoplay<GamebosuHitObject>
    {
        public override Score CreateReplayScore(IBeatmap beatmap) => new Score
        {
            ScoreInfo = new ScoreInfo
            {
                User = new User { Username = "sample" },
            },
            Replay = new GamebosuAutoGenerator(beatmap).Generate(),
        };
    }
}