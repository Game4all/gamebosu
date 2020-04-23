// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osuTK;

namespace osu.Game.Rulesets.Gamebosu.Objects.Drawables
{
    public class DrawableGamebosuHitObject : DrawableHitObject<GamebosuHitObject>
    {
        public DrawableGamebosuHitObject(GamebosuHitObject hitObject)
            : base(hitObject)
        {
            Size = Vector2.Zero;
            Origin = Anchor.Centre;
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
        }

        protected override void UpdateStateTransforms(ArmedState state)
        {
        }
    }
}