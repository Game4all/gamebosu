// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using System;

namespace Emux.Bass
{   
    /// <summary>
    /// A Span<T> version of a fixed-size buffer, that should eventually become actually "circular".
    /// </summary>
    public class CircularBuffer<T>
        where T : struct
    {
        private Memory<T> mem;

        public CircularBuffer(int workingSize)
        {
            mem = new Memory<T>(new T[workingSize]);
        }

        public void Enqueue(Span<T> data)
        {
            if (data.Length > mem.Span.Length)
                data.Slice(mem.Span.Length).CopyTo(mem.Span);
            else
                data.CopyTo(mem.Span);
        }

        public void Dequeue(Span<T> Outdata)
        {
            if (mem.Span.Length > Outdata.Length)
                mem.Span.Slice(0, Outdata.Length).CopyTo(Outdata);
            else
                mem.Span.CopyTo(Outdata);

            mem.Span.Fill(default(T)); //clears the buffer
        }
    }
}