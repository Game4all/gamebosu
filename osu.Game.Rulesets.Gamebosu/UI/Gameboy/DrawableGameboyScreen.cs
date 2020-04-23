// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using SixLabors.ImageSharp.PixelFormats;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    /// <summary>
    /// The drawable screen of a gameboy emulator.
    /// </summary>
    //TODO: investigate whether it would be benefical to make this use a custom draw node instead of uploading textures every 1/60 of a second.
    public class DrawableGameboyScreen : Sprite, IVideoOutput
    {
        private byte[] screenData;

        public DrawableGameboyScreen()
        {
            Texture = new Texture(160, 144);
            screenData = new byte[160 * 144 * sizeof(int)]; //since the 4 components of a color (r, g, b a) are each a byte (4 bytes in total), the same as an int.
        }

        public void Clear()
        {
            for (int i = 0; i < screenData.Length; i++)
                screenData[i] = byte.MaxValue;

            uploadTex();
        }

        public void RenderFrame(byte[] pixelData)
        {
            for (int i = 0, j = 0; j < pixelData.Length; i += 4, j += 3)
            {
                screenData[i] = pixelData[j]; //r component
                screenData[i + 1] = pixelData[j + 1]; //g component
                screenData[i + 2] = pixelData[j + 2]; //b component
                screenData[i + 3] = byte.MaxValue; // gameboy doesn't handle opacity, so let's force it to max value.
            }

            uploadTex();
        }

        private void uploadTex()
        {
            var image = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(screenData, 160, 144);
            Texture.SetData(new TextureUpload(image));
        }
    }
}