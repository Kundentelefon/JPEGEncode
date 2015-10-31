using System;
using System.Collections;
using System.Collections.Generic;
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
        private int[] bArray;
        private int _version;
        public static IEnumerable<bool> ReadBits(Stream input)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (!input.CanRead) throw new ArgumentException("Cannot read from input", "input");
            return ReadBitsCore(input);}

        private static IEnumerable<bool> ReadBitsCore(Stream input)
        {
            int readByte;
            while ((readByte = input.ReadByte()) >= 0)
            {
                for (int i = 7; i >= 0; i--)
                    yield return ((readByte >> i) & 1) == 1;
            }
        }

        public void BitstreamByte(Stream bits)
        {
            if (bits == null)
                throw new ArgumentNullException("bits are null: " + nameof(bits));

            //array of bits as bytes
            byte [] byteBits = new byte[bits.Length];

            long currentPos = bits.Position;

            bits.Position = 0;

            //write byte with bits
            bits.Read(byteBits, 0, (int) bits.Length);
            bits.Position = currentPos;
            BitWriteByte(byteBits, 0, (int) bits.Length);
        }

        public void BitWriteByte(byte[] bits, int offset, int count)
        {
            if(bits == null)
                throw new ArgumentNullException("bits are null: " + nameof(bits));
            if(offset < 0 )
                throw new ArgumentOutOfRangeException("offset out of range");
            if(count > (bits.Length - offset))
                throw new ArgumentException("Invalid count offset");

            byte [] byteBits = new byte[count];
            Buffer.BlockCopy(bits, offset, byteBits, 0, count);
        }

        //Writes one bit
        public void WriteBits(int bit, int length)
        {
            this.bArray = new int[length];
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < length; i++)
            {
                SetBits(i, (bit == 1));
                Console.WriteLine(i);
                Console.WriteLine("bool " + this.bArray[i]);
            }
            sw.Stop();
            Console.WriteLine("BitWrite Elapsed={0} ", sw.Elapsed);
            Console.ReadKey();
        }

        public void SetBits(int index, bool value)
        {
            if (index < 0 || index >= this.bArray.Length)
                new ArgumentOutOfRangeException("index: " + index + " is out of range");
            if (value) { 
                this.bArray[index / 32] |= 1 << index % 32;
            Console.WriteLine(this.bArray[index / 32]);
                Console.WriteLine(this.bArray[index / 32] |= 1 << index % 32);
            }
            else
                this.bArray[index / 32] &= ~(1 << index % 32);
        }

        public void WriteByte(byte[] buffer, byte b)
        {
            
        }
        public void WriteByte(byte[] buffer, int offset, int count)
        {
            byte value = 0;
            this.WriteByte(new byte[1]
{
        value
}, 0, 1);

            buffer = new byte[1];
            offset = 0;
            count = 1;
        }

        public void WriteShort(byte[] b, short hex)
        {
            
        }

        public void WriteBitArray(BitArray bA)
        {
            BitArray bitBuffer = new BitArray(new byte[255]);
            int bitCount = 0;
            for (int i = 0; i < bA.Length; i++)
            {
                bitBuffer.Set(bitCount + i, bA[i]);
                bitCount++;
            }

            if (bitCount%8 == 0)
            {
                BitArray bitBufferWithLength = new BitArray(new byte[bitCount / 8]);
                byte[] res = new byte[bitBuffer.Count / 8];
                for (int i = 0; i < bitCount; i++)
                {
                    bitBufferWithLength.Set(i, (bitBuffer[i]));
                }
                bitBuffer.CopyTo(res, 0);
                bitCount = 0;
               // base.Write(res, res.Length);
            }
        }

    }
}
