using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class PictureHead
    {

        public int pixelMaxX;
        public int pixelMaxY;

        public int fillX;
        public int fillY;

        public static Byte[] QTYStandard =
        {
            16, 11, 10, 16, 24, 40, 51, 61,
            12, 12, 14, 19, 26, 58, 60, 55,
            14, 13, 16, 24, 40, 57, 69, 56,
            14, 17, 22, 29, 51, 87, 80, 62,
            18, 22, 37, 56, 68, 109, 103, 77,
            24, 35, 55, 64, 81, 104, 113, 92,
            49, 64, 78, 87, 103, 121, 120, 101,
            72, 92, 95, 98, 112, 100, 103, 99
        };

        public static Byte[] QTCrCbStandard =
        {
            17, 18, 24, 47, 99, 99, 99, 99,
            18, 21, 26, 66, 99, 99, 99, 99,
            24, 26, 56, 99, 99, 99, 99, 99,
            47, 66, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99,
            99, 99, 99, 99, 99, 99, 99, 99
        };

        public static Byte[] ZigZagTable =
        {
            0, 1, 5, 6, 14, 15, 27, 28,
            2, 4, 7, 13, 16, 26, 29, 42,
            3, 8, 12, 17, 25, 30, 41, 43,
            9, 11, 18, 24, 31, 40, 44, 53,
            10, 19, 23, 32, 39, 45, 52, 54,
            20, 22, 33, 38, 46, 51, 55, 60,
            21, 34, 37, 47, 50, 56, 59, 61,
            35, 36, 48, 49, 57, 58, 62, 63
        };

        private static void PictureStart(Bitstream bs)
        {
            //initialises JPG
            bs.AddShort(0xFFD8);
        }

        private static void APP0Head(Bitstream bs)
        {
            ushort length = 16; // length of segment >=16
            byte j = 0x4a; // string J
            byte f = 0x46; // string f
            byte i = 0x49; // string I
            byte zero = 0x00; // string #0
            byte majorRevisionNumber = 1;
            byte minorRevisionNumber = 1; // together 1.1
            byte xyunits = 0; // no unit = normal aspect ratio
            ushort xPixelDensity = 0x0048; // x density
            ushort yPixelDensity = 0x0048; // y density
            byte thumbnailWidth = 0;
            byte thumbnailHeight = 0; // no thumbnail

            bs.AddShort(0xFFE0); //marker
            bs.AddShort(length); //length
            bs.AddByteSpecial(j);
            bs.AddByteSpecial(f);
            bs.AddByteSpecial(i);
            bs.AddByteSpecial(f);
            bs.AddByteSpecial(zero);
            bs.AddByteSpecial(majorRevisionNumber);
            bs.AddByteSpecial(minorRevisionNumber);
            bs.AddByteSpecial(xyunits);
            bs.AddShort(xPixelDensity);
            bs.AddShort(yPixelDensity);
            bs.AddByteSpecial(thumbnailWidth);
            bs.AddByteSpecial(thumbnailHeight);
        }

        //Define Quantization Table
        private static void DQT(Bitstream bs)
        {
            ushort marker = 0xFFDB;
            ushort length = 2+2+64;
            byte QTY = 0; // Bit 0-3: Number of QTable. Bit 4-7 precission. -> Table for Y with precision of 8 bit
            byte QTCbCr = 1; // 1 (quantization table for CB,CR)

            byte[] outputYTable = new byte[64]; // 64*(precision+1)
            byte[] outputCrCbTable = new byte[64];

            for (int i = 0; i < 64; i++)
            {
                //position at position i from zigzag = temp
                outputYTable[ZigZagTable[i]] = QTYStandard[i];
            }

            for (int i = 0; i < 64; i++)
            {
                outputCrCbTable[ZigZagTable[i]] = QTCrCbStandard[i];
            }

            bs.AddShort(marker);
            bs.AddShort(length);
            bs.AddByteSpecial(QTY);
            bs.WriteByteArray(bs, outputYTable, 0);
            bs.AddByteSpecial(QTCbCr);
            bs.WriteByteArray(bs, outputCrCbTable, 0);
        }

        private static void SOF0Head(Bitstream bs, ushort pHeight, ushort pWidth)
        {
            ushort length = 17; // 8 + (Y,Cb,Cr) * 3
            byte precision = 8; // precision of data in bits/sample
            ushort pictureHeight = pHeight;
            ushort pictureWidth = pWidth;
            byte components = 3; // 3 for Y,Cb,Cr
            byte IDY = 1; // ID component 1 = Y
            byte SFY = 0x22; // sampling factor for Y (bit 0-3 vertical, bit 4-7 horicontal)
            byte QTY = 0; // quantization table of Y = 0
            byte IDCb = 2; // ID component 2 = Cb
            byte SFCb = 0x11;
            byte QTCb = 0; // quantization table of Cb = 1
            byte IDCr = 3; // ID component 3 = Cr
            byte SFCr = 0x11;
            byte QTCr = 0;

            bs.AddShort(0xFFC0); // marker
            bs.AddShort(length);
            bs.AddByteSpecial(precision);
            bs.AddShort(pictureHeight);
            bs.AddShort(pictureWidth);
            bs.AddByteSpecial(components);
            bs.AddByteSpecial(IDY); // 3 Bytes for each component
            bs.AddByteSpecial(SFY);
            bs.AddByteSpecial(QTY);
            bs.AddByteSpecial(IDCb);
            bs.AddByteSpecial(SFCb);
            bs.AddByteSpecial(QTCb);
            bs.AddByteSpecial(IDCr);
            bs.AddByteSpecial(SFCr);
            bs.AddByteSpecial(QTCr);
        }

        private static void DHTHead(Bitstream bs)
        {
            //Huffman-Code with only 1 Bits is not allowed
            //Maximum Huffman depth is 16 
            ushort length;
            byte hTInformation = 0x00; // bit 0..3 = number of HT
            //bit 4 = type of HT, 0 = DC, 1 = AC
            //bit 5..7 = must be 0

            // How many codes with symbol 1
            byte[] symbolLength = new byte[16]; // quantity of symbols with codeLength between 1..16 (Sum of symbols must be <= 256)
            byte[] table; // n bytes, n = total number of symbols

            //HuffmanEncoder he = new HuffmanEncoder();

            length = (ushort)(2 + 1 + 16 + HuffmanEncoder.huffmanTable.Count); // length = addition of bytes of each segment
            table = new byte[HuffmanEncoder.huffmanTable.Count];

            //counts for each Values (List<bool>) the number of objects in it and saves it to symbolLength

            for (int k = 0; k < HuffmanEncoder.huffmanTable.Count; k++)
            {
                int temp = 0;
                foreach (List<bool> sl in HuffmanEncoder.huffmanTable.Values)
                {
                    if (k == sl.Count)
                        temp++;
                }
                symbolLength[k] = (byte)temp;
            }

            int j = 0;
            foreach (var symbolCount in HuffmanEncoder.huffmanTable.Keys)
            {
                table[j++] = symbolCount;
            }

            bs.AddShort(0xFFc4); //marker
            bs.AddShort(length);
            bs.AddByteSpecial(hTInformation);
            bs.WriteByteArray(bs, symbolLength, 0);
            bs.WriteByteArray(bs, table, 0);
        }

        private static void DHTHeadStandard(Bitstream bs)
        {
            // standards for 2:1 horicontal subsampling
            ushort length = 0x01A2;//DHTheadlänge
            byte AC = 0x10;//Durchschnittsfrequenz
            byte DC = 0x00;//min->max Frequenz

            byte[] YDCNodes = { 0, 0, 1, 5, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 };
            byte[] YDCValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            byte[] YACNodes = { 0, 0, 2, 1, 3, 3, 2, 4, 3, 5, 5, 4, 4, 0, 0, 1, 0x7d };
            byte[] YACValues = {0x01, 0x02, 0x03, 0x00, 0x04, 0x11, 0x05, 0x12,
            0x21, 0x31, 0x41, 0x06, 0x13, 0x51, 0x61, 0x07,
            0x22, 0x71, 0x14, 0x32, 0x81, 0x91, 0xa1, 0x08,
            0x23, 0x42, 0xb1, 0xc1, 0x15, 0x52, 0xd1, 0xf0,
            0x24, 0x33, 0x62, 0x72, 0x82, 0x09, 0x0a, 0x16,
            0x17, 0x18, 0x19, 0x1a, 0x25, 0x26, 0x27, 0x28,
            0x29, 0x2a, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39,
            0x3a, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49,
            0x4a, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59,
            0x5a, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69,
            0x6a, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79,
            0x7a, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89,
            0x8a, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98,
            0x99, 0x9a, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7,
            0xa8, 0xa9, 0xaa, 0xb2, 0xb3, 0xb4, 0xb5, 0xb6,
            0xb7, 0xb8, 0xb9, 0xba, 0xc2, 0xc3, 0xc4, 0xc5,
            0xc6, 0xc7, 0xc8, 0xc9, 0xca, 0xd2, 0xd3, 0xd4,
            0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xe1, 0xe2,
            0xe3, 0xe4, 0xe5, 0xe6, 0xe7, 0xe8, 0xe9, 0xea,
            0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8,
            0xf9, 0xfa };

            byte[] CbDCNodes = { 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            byte[] CbDCValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            byte[] CbACNodes = { 0, 0, 2, 1, 2, 4, 4, 3, 4, 7, 5, 4, 4, 0, 1, 2, 0x77 };
            byte[] CbACValues = {0x00, 0x01, 0x02, 0x03, 0x11, 0x04, 0x05, 0x21,
            0x31, 0x06, 0x12, 0x41, 0x51, 0x07, 0x61, 0x71,
            0x13, 0x22, 0x32, 0x81, 0x08, 0x14, 0x42, 0x91,
            0xa1, 0xb1, 0xc1, 0x09, 0x23, 0x33, 0x52, 0xf0,
            0x15, 0x62, 0x72, 0xd1, 0x0a, 0x16, 0x24, 0x34,
            0xe1, 0x25, 0xf1, 0x17, 0x18, 0x19, 0x1a, 0x26,
            0x27, 0x28, 0x29, 0x2a, 0x35, 0x36, 0x37, 0x38,
            0x39, 0x3a, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48,
            0x49, 0x4a, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58,
            0x59, 0x5a, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68,
            0x69, 0x6a, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78,
            0x79, 0x7a, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87,
            0x88, 0x89, 0x8a, 0x92, 0x93, 0x94, 0x95, 0x96,
            0x97, 0x98, 0x99, 0x9a, 0xa2, 0xa3, 0xa4, 0xa5,
            0xa6, 0xa7, 0xa8, 0xa9, 0xaa, 0xb2, 0xb3, 0xb4,
            0xb5, 0xb6, 0xb7, 0xb8, 0xb9, 0xba, 0xc2, 0xc3,
            0xc4, 0xc5, 0xc6, 0xc7, 0xc8, 0xc9, 0xca, 0xd2,
            0xd3, 0xd4, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda,
            0xe2, 0xe3, 0xe4, 0xe5, 0xe6, 0xe7, 0xe8, 0xe9,
            0xea, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8,
            0xf9, 0xfa };


            bs.AddShort(0xFFc4); //marker
            bs.AddShort(length);

            bs.AddByteSpecial(DC); //HTInformation for YDC
            bs.WriteByteArray(bs, YDCNodes, 1); // 1 is startposition after the byte of HTInformation for YDC
            bs.WriteByteArray(bs, YDCValues, 0);

            bs.AddByteSpecial(AC);
            bs.WriteByteArray(bs, YACNodes, 1);
            bs.WriteByteArray(bs, YACValues, 0);

            bs.AddByteSpecial(DC);
            bs.WriteByteArray(bs, CbDCNodes, 1);
            bs.WriteByteArray(bs, CbDCValues, 0);

            bs.AddByteSpecial(AC);
            bs.WriteByteArray(bs, CbACNodes, 1);
            bs.WriteByteArray(bs, CbACValues, 0);

        }

        private static void SOS(Bitstream bs)
        {
            ushort marker = 0xFFDA;
            ushort length = 12; // 6 + 2 * (number of components)
            byte NROFComponents = 3; // Number of Components in Picture 1 or 3 
            byte IdY = 1; // component ID
            byte HTY = 0x01; // Bits 0-3: AC Table
            byte IdCb = 2;
            //TODO: überprüfe ob richtig eingesetztes HT
            byte HTCb = 0x10; // Bits 4-7: DC Table
            byte IdCr = 3;
            byte HTCr = 0x10; // 0x11 would be AC and DC
            byte SS = 0; // start of spectral or prediction selection
            byte SE = 63; // end of spectral selection
            byte BF = 0; // successiv approcimation

            bs.AddShort(marker);
            bs.AddShort(length);
            bs.AddByteSpecial(NROFComponents);
            bs.AddByteSpecial(IdY);
            bs.AddByteSpecial(HTY);
            bs.AddByteSpecial(IdCb);
            bs.AddByteSpecial(HTCb);
            bs.AddByteSpecial(IdCr);
            bs.AddByteSpecial(HTCr);
            bs.AddByteSpecial(SS);
            bs.AddByteSpecial(SE);
            bs.AddByteSpecial(BF);
        }

        private static void PictureEnd(Bitstream bs)
        {
            //Ends JPG
            bs.AddShort(0xffd9);
        }

        public static void CreateJPGHead(Bitstream bs, ushort pHeight, ushort pWidth)
        {
            PictureStart(bs);
            APP0Head(bs);
            DQT(bs);
            SOF0Head(bs, pHeight, pWidth);
            DHTHeadStandard(bs);
            DHTHead(bs);
            SOS(bs);
            //PictureEnd(bs);
        }

    }
}
