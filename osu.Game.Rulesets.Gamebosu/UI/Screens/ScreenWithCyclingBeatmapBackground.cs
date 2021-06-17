// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Game.Screens.Play;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    /// <summary>
    /// A screen with a blured background which automatically cycles to the current beatmap's background upon changing beatmap background.
    /// </summary>
    public class ScreenWithCyclingBeatmapBackground : ScreenWithBeatmapBackground
    {
        private const float blur_factor = 20;

        private void updateBackground(ValueChangedEvent<WorkingBeatmap> beatmap)
        {
            Schedule(() =>
            {
                ApplyToBackground(background =>
                {
                    background.BlurAmount.Value = blur_factor;
                    background.Beatmap = beatmap.NewValue;
                });
            });
        }

        protected override void LoadComplete()
        {
            Beatmap.BindValueChanged(updateBackground, true);
            base.LoadComplete();
        }
    }
}