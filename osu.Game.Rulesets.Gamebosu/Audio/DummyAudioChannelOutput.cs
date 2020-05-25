// gamebosu! ruleset. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Audio;
using System;

namespace osu.Game.Rulesets.Gamebosu.Audio
{
    public class DummyAudioChannelOutput : IAudioChannelOutput
    {
        public int SampleRate => 0;

        public void BufferSoundSamples(Span<float> sampleData, int offset, int length)
        {
        }
    }
}