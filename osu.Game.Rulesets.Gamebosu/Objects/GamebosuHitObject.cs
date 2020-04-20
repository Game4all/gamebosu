// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under MIT. 
// See LICENSE at root of repo for more information on licensing.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;

namespace osu.Game.Rulesets.Gamebosu.Objects
{
    public class GamebosuHitObject : HitObject
    {
        public override Judgement CreateJudgement() => new Judgement();
    }
}
