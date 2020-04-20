// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under MIT. 
// See LICENSE at root of repo for more information on licensing.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;
using osuTK;

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
