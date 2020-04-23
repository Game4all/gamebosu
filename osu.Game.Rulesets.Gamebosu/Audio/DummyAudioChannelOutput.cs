// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Audio;

namespace osu.Game.Rulesets.Gamebosu.Audio
{
    public class DummyAudioChannelOutput : IAudioChannelOutput
    {
        public int SampleRate => 0;

        public void BufferSoundSamples(float[] sampleData, int offset, int length)
        {
        }
    }
}