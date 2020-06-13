using Emux.GameBoy.Cartridge;
using System;
using System.IO;

namespace osu.Game.Rulesets.Gamebosu.IO
{
    /// <summary>
    /// RAM based external memory (similar to <see cref="BufferedExternalMemory"/>.
    /// Writes to disk only when disposed.
    /// </summary>
    public class RAMMemory : IExternalMemory
    {
        private byte[] externalMemory;
        private Stream saveStream;

        public RAMMemory(Stream saveStream)
        {
            externalMemory = new byte[1];

            this.saveStream = saveStream;
            SetBufferSize((int)saveStream.Length);
            saveStream.Read(externalMemory, 0, (int)saveStream.Length);
        }

        public bool IsActive { get; private set; }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public byte ReadByte(int address)
        {
            if (IsActive)
                return externalMemory[address];

            return 0;
        }

        public void ReadBytes(int address, byte[] buffer, int offset, int length)
        {
            if (IsActive)
                Array.Copy(externalMemory, address, buffer, 0, length);
        }

        public void SetBufferSize(int length) => Array.Resize<byte>(ref externalMemory, length);

        public void WriteByte(int address, byte value)
        {
            if (IsActive)
                externalMemory[address] = value;
        }

        public void Dispose()
        {
            saveStream.Position = 0;
            saveStream.Write(externalMemory.AsSpan());
            saveStream.Flush();
            saveStream.Dispose();
        }
    }
}