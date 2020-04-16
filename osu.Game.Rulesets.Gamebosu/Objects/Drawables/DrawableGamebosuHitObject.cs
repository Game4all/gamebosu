// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

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
