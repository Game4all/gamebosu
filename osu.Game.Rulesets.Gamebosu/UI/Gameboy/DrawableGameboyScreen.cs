// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using System;
using osu.Framework.Allocation;
using osu.Framework.Platform;
using SixLabors.ImageSharp.PixelFormats;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    /// <summary>
    /// The drawable screen of a gameboy emulator.
    /// </summary>
    public class DrawableGameboyScreen : Sprite, IVideoOutput
    {
        private readonly SpanTextureUpload upload;
        private Memory<byte> screenData;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            Texture = host.Renderer.CreateTexture(160, 144, false, TextureFilteringMode.Nearest, WrapMode.ClampToEdge, WrapMode.ClampToEdge, new Rgba32(0xFFFFFFFF));
        }

        public DrawableGameboyScreen()
        {
            screenData = new Memory<byte>(new byte[160 * 144 * sizeof(int)]); //since the 4 components of a color (r, g, b a) are each a byte (4 bytes in total), the same as an int.
            upload = new SpanTextureUpload(screenData);
        }

        public void Clear()
        {
            for (int i = 0; i < screenData.Length; i++)
                screenData.Span[i] = byte.MaxValue;

            Texture.SetData(upload);
        }

        public void RenderFrame(byte[] pixelData)
        {
            for (int i = 0, j = 0; j < pixelData.Length; i += 4, j += 3)
            {
                screenData.Span[i] = pixelData[j]; //r component
                screenData.Span[i + 1] = pixelData[j + 1]; //g component
                screenData.Span[i + 2] = pixelData[j + 2]; //b component
                screenData.Span[i + 3] = byte.MaxValue; // gameboy doesn't handle opacity, so let's force it to max value.
            }

            Texture.SetData(upload);
        }
    }
}