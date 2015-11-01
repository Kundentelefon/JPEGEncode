using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class Bitstream
    {

        private const int ArrayTypeSize = 8;

        private long bitIndex;
        private byte[] bArray;

        public Bitstream(int bufferSize)
        {
            bArray = new byte[bufferSize];
        }

        //public static IEnumerable<bool> ReadBits(Stream input)
        //{
        //    if (input == null) throw new ArgumentNullException("input");
        //    if (!input.CanRead) throw new ArgumentException("Cannot read from input", "input");
        //    return ReadBitsCore(input);}

        //private static IEnumerable<bool> ReadBitsCore(Stream input)
        //{
        //    int readByte;
        //    while ((readByte = input.ReadByte()) >= 0)
        //    {
        //        for (int i = 7; i >= 0; i--)
        //            yield return ((readByte >> i) & 1) == 1;
        //    }
        //}

        //Writes one bit
        public void AddBits(bool value, int length, int offset = 0)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = offset; i < length + offset; i++)
            {
                AddBit(value);
            }

            sw.Stop();
            Console.WriteLine("BitWrite Elapsed={0} ", sw.Elapsed);
        }

        public void AddBit(bool value)
        {
            if (bitIndex >= bArray.Length * ArrayTypeSize)
                throw new IndexOutOfRangeException("Bitstream is full");

            if (value)
            {
                bArray[bitIndex / ArrayTypeSize] |= (byte) (1 << (int) ((ArrayTypeSize - 1) - (bitIndex % ArrayTypeSize)));
            }
            else
            {
                bArray[bitIndex / ArrayTypeSize] &= (byte)(~(1 << (int) ((ArrayTypeSize - 1) - (bitIndex % ArrayTypeSize))));
            }

            bitIndex++;
        }

        public void AddByte(byte value)
        {
            for (int i = 0x80; i > 0; i /= 2)
            {
                AddBit(0 != (value & i));
            }
        }

        public void AddShort(ushort value)
        {
            for (int i = 0x8000; i > 0; i /= 2)
            {
                AddBit(0 != (value & i));
            }
        }

        public bool GetBit(long index)
        {
            return 0 != (bArray[index / ArrayTypeSize] & (uint)(1 << (int)(index % ArrayTypeSize)));
        }

        public void WriteToFile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(bArray, 0, (int)(bitIndex + ArrayTypeSize - 1) / ArrayTypeSize);
            }
        }
    }
}
