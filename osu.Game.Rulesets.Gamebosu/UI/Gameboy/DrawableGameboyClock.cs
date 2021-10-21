// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cpu;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    /// <summary>
    /// A gameboy CPU <see cref="IClock"/> wrapped into a <see cref="Component"/> to access <see cref="Drawable.Clock"/> for timing purposes.
    /// </summary>
    public class DrawableGameboyClock : Component, IClock
    {
        private double lastTickTime;

        private const int clock_rate = 60;

        public event EventHandler Tick;

        public bool IsActive { get; private set; }

        public void Start() => IsActive = true;

        public void Stop() => IsActive = false;

        public readonly BindableDouble Rate = new BindableDouble(1)
        {
            MinValue = 0.01,
            MaxValue = 5
        };

        protected override void Update()
        {
            if (!IsActive) return;

            var timeDelta = Time.Current - lastTickTime;

            if (timeDelta > 1000 / (clock_rate * Rate.Value))
            {
                Tick?.Invoke(null, EventArgs.Empty);
                lastTickTime = Time.Current;
            }
        }
    }
}