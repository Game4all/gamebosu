﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Game.Rulesets.Gamebosu.UI.Screens;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Gamebosu.UI
{
    [Cached]
    public class GamebosuPlayfield : Playfield
    {
        private GamebosuScreenStack stack;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                HitObjectContainer,
                stack = new GamebosuScreenStack()
            });
        }

        protected override void LoadComplete()
        {
            stack?.Push(new DisclaimerScreen
            {
                Complete = (sc) => sc.Push(new RomSelectionScreen())
            });
            base.LoadComplete();
        }
    }
}
