﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Gamebosu.Objects;
using osu.Game.Rulesets.Replays;

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
