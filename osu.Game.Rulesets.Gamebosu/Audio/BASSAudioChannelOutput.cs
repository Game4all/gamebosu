// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.Bass;
using Emux.GameBoy.Audio;
using ManagedBass;
using System;
using System.Runtime.InteropServices;

namespace osu.Game.Rulesets.Gamebosu.Audio
{
    //TODO: Move BASS stream to a class derived from AudioComponent
    //TODO: Fix audio
    public class BASSAudioChannelOutput : IAudioChannelOutput, IDisposable
    {
        private int bassChannel;

        public int SampleRate => 44100;

        private CircularBuffer<float> buff;

        public BASSAudioChannelOutput()
        {
            bassChannel = Bass.CreateStream(SampleRate, 4, BassFlags.Default, fetchBassData);
            buff = new CircularBuffer<float>(64768);
            Bass.ChannelSetAttribute(bassChannel, ChannelAttribute.Volume, 0.5f); //lock volume to 50%
            Bass.ChannelPlay(bassChannel);
        }

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

        public void Dispose()
        {
            Bass.ChannelStop(bassChannel);
            Bass.StreamFree(bassChannel);
        }
    }
}
