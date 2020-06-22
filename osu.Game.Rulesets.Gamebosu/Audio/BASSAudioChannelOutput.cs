// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.Bass;
using Emux.GameBoy.Audio;
using ManagedBass;
using osu.Framework.Audio;
using System;
using System.Runtime.InteropServices;

namespace osu.Game.Rulesets.Gamebosu.Audio
{
    //TODO: Fix audio weird noises.
    public class BASSAudioChannelOutput : AdjustableAudioComponent, IAudioChannelOutput, IDisposable
    {
        private int bassChannel;

        public int SampleRate => 44100;

        private CircularBuffer<float> buff;

        public BASSAudioChannelOutput()
            : base()
        {
            bassChannel = Bass.CreateStream(SampleRate, 2, BassFlags.Default | BassFlags.Float, fetchBassData);
            buff = new CircularBuffer<float>(64768);

            AggregateVolume.BindValueChanged(t => Bass.ChannelSetAttribute(bassChannel, ChannelAttribute.Volume, t.NewValue * 0.10), true);
        }

        public bool Play() => Bass.ChannelPlay(bassChannel);

        public void BufferSoundSamples(Span<float> sampleData, int offset, int length)
        {
            buff.Enqueue(sampleData);
        }

        private int fetchBassData(int _Handle, IntPtr Buffer, int Length, IntPtr User)
        {
            var length = Length / sizeof(float);
            var sData = new float[length];

            buff.Dequeue(sData.AsSpan());
            Marshal.Copy(sData, 0, Buffer, length);

            return Length;
        }

        public override void Dispose()
        {
            Bass.ChannelStop(bassChannel);
            Bass.StreamFree(bassChannel);

            base.Dispose();
        }
    }
}