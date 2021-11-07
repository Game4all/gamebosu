// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Audio;
using ManagedBass;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using System;
using System.Runtime.InteropServices;

namespace osu.Game.Rulesets.Gamebosu.Audio
{
    //TODO: Fix audio weird noises.
    public class BassAudioChannelOutput : AdjustableAudioComponent, IAudioChannelOutput, IDisposable
    {
        private int bassChannel;

        public int SampleRate => 44100;

        private CircularBuffer<float> buff;

        private readonly BindableDouble adjustmentBindable = new BindableDouble(0.08);

        public BassAudioChannelOutput()
        {
            bassChannel = Bass.CreateStream(SampleRate, 2, BassFlags.Default | BassFlags.Float, fetchBassData);
            buff = new CircularBuffer<float>(64768);

            AddAdjustment(AdjustableProperty.Volume, adjustmentBindable);
            AggregateVolume.BindValueChanged(t => Bass.ChannelSetAttribute(bassChannel, ChannelAttribute.Volume, t.NewValue), true);
        }

        public bool Play() => Bass.ChannelPlay(bassChannel);

        public bool Stop() => Bass.ChannelStop(bassChannel);

        public void BufferSoundSamples(Span<float> sampleData, int offset, int length) => buff.Enqueue(sampleData);

        private int fetchBassData(int handle, IntPtr buffer, int bufferLength, IntPtr user)
        {
            var length = bufferLength / sizeof(float);
            var sData = new float[length];

            buff.Dequeue(sData.AsSpan());
            Marshal.Copy(sData, 0, buffer, length);

            return bufferLength;
        }

        protected override void Dispose(bool disposing)
        {
            Stop();
            Bass.StreamFree(bassChannel);

            base.Dispose(disposing);
        }
    }
}