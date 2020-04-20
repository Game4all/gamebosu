// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

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
