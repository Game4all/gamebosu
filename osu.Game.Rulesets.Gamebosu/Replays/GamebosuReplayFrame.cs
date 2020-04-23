// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Rulesets.Replays;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Gamebosu.Replays
{
    public class GamebosuReplayFrame : ReplayFrame
    {
        public List<GamebosuAction> Actions = new List<GamebosuAction>();

        public GamebosuReplayFrame(GamebosuAction? button = null)
        {
            if (button.HasValue)
                Actions.Add(button.Value);
        }
    }
}