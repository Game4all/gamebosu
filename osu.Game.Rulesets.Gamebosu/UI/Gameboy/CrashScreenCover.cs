// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    public class CrashScreenCover : Sprite
    {
        private readonly float scale = 0.8f;

        public CrashScreenCover()
        {
            RelativeSizeAxes = Axes.Both;
            Scale = new osuTK.Vector2(scale);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Texture = textures.Get("Textures/emu_went_brrr");
        }
    }
}