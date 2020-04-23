// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Gamebosu.Objects;
using osu.Game.Rulesets.Replays;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Gamebosu.Replays
{
    public class GamebosuAutoGenerator : AutoGenerator
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public new Beatmap<GamebosuHitObject> Beatmap => (Beatmap<GamebosuHitObject>)base.Beatmap;

        public GamebosuAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
            Replay = new Replay();
        }

        public override Replay Generate()
        {
            Frames.Add(new GamebosuReplayFrame());

            foreach (GamebosuHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new GamebosuReplayFrame
                {
                    Time = hitObject.StartTime,
                });
            }

            return Replay;
        }
    }
}