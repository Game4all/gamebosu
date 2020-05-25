// gamebosu! ruleset. Licensed under GPLv3.
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