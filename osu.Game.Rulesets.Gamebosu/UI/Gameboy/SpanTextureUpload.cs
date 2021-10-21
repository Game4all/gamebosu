// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Textures;
using osuTK.Graphics.ES30;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Runtime.InteropServices;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    public class SpanTextureUpload : ITextureUpload
    {
        private readonly Memory<byte> uploadData;

        public SpanTextureUpload(Memory<byte> uploadData)
        {
            this.uploadData = uploadData;
        }

        public ReadOnlySpan<Rgba32> Data => MemoryMarshal.Cast<byte, Rgba32>(uploadData.Span);

        public int Level => 0;

        private RectangleI bounds = new RectangleI(0, 0, 160, 144);

        public RectangleI Bounds
        {
            get => bounds;
            set => bounds = value;
        }

        public PixelFormat Format => PixelFormat.Rgba;

        public void Dispose()
        {
        }
    }
}