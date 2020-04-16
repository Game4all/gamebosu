// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Gamebosu.Replays
{
    public class GamebosuFramedReplayInputHandler : FramedReplayInputHandler<GamebosuReplayFrame>
    {
        public GamebosuFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(GamebosuReplayFrame frame) => frame.Actions.Any();

        public override List<IInput> GetPendingInputs()
        {
            return new List<IInput>
            {
                new ReplayState<GamebosuAction>
                {
                    PressedActions = CurrentFrame?.Actions ?? new List<GamebosuAction>(),
                }
            };
        }
    }
}
