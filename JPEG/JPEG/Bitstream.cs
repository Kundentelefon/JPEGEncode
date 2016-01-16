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
        // 8 for byte length
        private const int ArrayTypeSize = 8;

        private long bitIndex;
        private byte bArray;
        private List<byte> bArrayList; 

        // long gives a broader index option and allows int32 * 8 
        public Bitstream()
        {
            // rounds up 
            bArray = new byte();
            bArrayList = new List<byte>();
        }

        // Add as many bits count have at the offset
        public void AddBits(bool value, int count, int offset = 0)
        {
            for (int i = offset; i < count + offset; i++)
            {
                AddBit(value);
            }
        }

        // Writes one bit at the end 
        public void AddBit(bool value)
        {
            if (value)
            {
                // OR operation allocation at the index position with bitshift to left
                bArray |= (byte)(1 << (int)((ArrayTypeSize - 1) - (bitIndex % ArrayTypeSize)));
            }
            else
            {
                // AND operation allocation with bitwise inverse at the index position and bitshift to left
                bArray &= (byte)(~(1 << (int)((ArrayTypeSize - 1) - (bitIndex % ArrayTypeSize))));
            }
            bitIndex++;

            //Adds a new Byte each time a byte is fully written
            if (bitIndex % 8 == 0)
            {
                bArrayList.Add(bArray);
                bArray = new byte();
            }
        }

        public void AddByteSpecial(byte value)
        {
            //handle special case
            if (value == 0xFF)
            {
                AddByte(0xFF);
                AddByte(0x00);
            }
            else
            {
                // shifts the bit through the added byte value with /2 eg. 128->64->32 etc and compares with &
                for (int i = 0x80; i > 0; i /= 2)
                {
                    AddBit(0 != (value & i));
                }
            }
        }

        public void AddByte(byte value)
        {
            // shifts the bit through the added byte value with /2 eg. 128->64->32 etc and compares with &
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

        public void WriteByteArray(Bitstream bs, byte[] bArray, int startPos)
        {
            int length = bArray.Length;
            for(int i = startPos; i < length; i++)
            {
                bs.AddByte(bArray[i]);
            }
        }

        public bool GetBit(long index)
        {
            // Compares at the given index the bit with a bitshifted 1 if its true or not
            return 0 != (bArray & (uint)(1 << (int)(index % ArrayTypeSize)));
        }

        public long GetLength()
        {
            return bArrayList.Count;
        }

        public void WriteToFile(string filePath)
        {
            // uses everything from interface IDisposal eg. Filestream -> automatically throw catches every exception and open closes with finally
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(bArrayList.ToArray(), 0, (int)(bitIndex + ArrayTypeSize - 1) / ArrayTypeSize);
            }
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
    }
}
