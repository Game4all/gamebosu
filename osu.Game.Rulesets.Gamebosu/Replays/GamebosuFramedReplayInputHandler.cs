// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Input.StateChanges;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Gamebosu.Replays
{
    public class GamebosuFramedReplayInputHandler : FramedReplayInputHandler<GamebosuReplayFrame>
    {
        public GamebosuFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(GamebosuReplayFrame frame) => frame.Actions.Any();

        public override void CollectPendingInputs(List<IInput> inputs)
        {
        }
    }
}