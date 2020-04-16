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
