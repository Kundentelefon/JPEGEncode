using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace JPEG
{
    public class ImageToJpg
    {


        public void PPMtoJpg(string imageSrc, string imagedest)
        {
            Bitstream bs = new Bitstream();
            PlainFormatReader pfr = new PlainFormatReader(imageSrc);
            Mathloc ml = new Mathloc();
            Picture pic = pfr.ReadPicture();

            int height = pic.Head.pixelMaxY;
            int width = pic.Head.pixelMaxX;

            Color3[,] col3 = new Color3[height,width];
            byte[] colors = new byte[3];

            //convert RGB to YUV for each pixel
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    col3[y, x] = pic.Data.PictureYX[y, x];
                    colors[0] = col3[y, x].a; // R
                    colors[1] = col3[y, x].b; // G
                    colors[2] = col3[y, x].c; // B
                    colors = ml.RGBToYUV(colors);
                    col3[y, x].a = colors[0]; // Y
                    col3[y, x].b = colors[1]; // U
                    col3[y, x].c = colors[2]; // V
                }
            }

            //convert float array to byte array
            float[,] yArray = new float[width, height]; // width and height vertauscht
            float[,] uArray = new float[width, height];
            float[,] vArray = new float[width, height];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    yArray[i, j] = (float)col3[j, i].a;
                    uArray[i, j] = (float)col3[j, i].b;
                    vArray[i, j] = (float)col3[j, i].c;
                }
            }

            byte[,] yArrayEnde = new byte[width, height];
            byte[,] uArrayEnde = new byte[width, height];
            byte[,] vArrayEnde = new byte[width, height];

            //get float [number of 8x8][8x8]
            var yarry = aufteilen(yArray);
            var uarry = aufteilen(uArray);
            var varry = aufteilen(vArray);


            //geht beim 40x40 bild 25
            for (int y = 0; y < height/8; y ++)
            {
                for (int x = 0; x < width/8; x ++)
                {
                    

            //DCTDirect
            for (int i = 0; i < ((height * width) / 64); i++)
            {
                yArray = DCT.DCTdirect(yarry[i]);
                uArray = DCT.DCTdirect(uarry[i]);
                vArray = DCT.DCTdirect(varry[i]);
            }

            //convert float array to byte array
            byte[,] yArrayB = new byte[width, height];
            byte[,] uArrayB = new byte[width, height];
            byte[,] vArrayB = new byte[width, height];

            for (int i = 0; i < width/8; i++)
            {
                for (int j = 0; j < height/8; j++)
                {
                    yArrayB[i, j] = (byte)yArray[i, j];
                    uArrayB[i, j] = (byte)uArray[i, j];
                    vArrayB[i, j] = (byte)vArray[i, j];
                }
            }

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            yArrayEnde[i * x, j * y] = yArrayB[i, j];
                            uArrayEnde[i * x, j * y] = uArrayB[i, j];
                            vArrayEnde[i * x, j  *y] = vArrayB[i, j];           
                        }
                    }


                    byte[] yArrayB2 = new byte[64];
            byte[] uArrayB2 = new byte[64];
            byte[] vArrayB2 = new byte[64];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                yArrayB2[ZigZagTable2[i*j]] = yArrayB[i, j];
                uArrayB2[ZigZagTable2[i*j]] = uArrayB[i, j];
                vArrayB2[ZigZagTable2[i*j]] = vArrayB[i, j];
                }
            }





                    // Create Huffmantables
                    //yArrayEnde = HuffmanCalc(YDCNodes, YDCValues, yArrayB2);
                    //yArrayEnde = HuffmanCalc(YDCNodes, YACValues, yArrayB2);

                    //uArrayEnde = HuffmanCalc(CbDCNodes, CbDCValues, uArrayB2);
                    //uArrayEnde = HuffmanCalc(CbACNodes, CbACValues, uArrayB2);

                    //vArrayEnde = HuffmanCalc(CbDCNodes, CbDCValues, vArrayB2);
                    //vArrayEnde = HuffmanCalc(CbACNodes, CbACValues, vArrayB2);

                }
            }


            //create JPG head
            PictureHead.CreateJPGHead(bs, (ushort)height, (ushort)width);

            
            //TODO: DCT
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bs.AddByte((byte)yArrayEnde[x, y]);
                    bs.AddByte((byte)uArrayEnde[x, y]);
                    bs.AddByte((byte)vArrayEnde[x, y]);
                }
            }

            bs.AddShort(0xffd9); //End of Picture Marker
            bs.WriteToFile(imagedest);
            

        }
        public MemoryStream DCzeug(List<byte[]> Input)
        {
            MemoryStream stream = new MemoryStream();
            foreach (var item in Input)
            {
                for (int i = 1; i < item.Length; i++)
                {
                    stream.WriteByte(item[i]);
                }
            }
            return stream;
        } 
        public float[][,] aufteilen(float[,]input)
        {
            float[][,] returnarry= new float[(input.GetLength(1) * input.GetLength(0))/64][,];
            for (int i = 0; i < input.GetLength(1); i=i+8)
            {
                for (int ia = 0; ia < input.GetLength(0); )
                {
                    float[,] temparry = new float[8,8];
                    for (int ib  = 0; ib < 8; ib++)
                    {
                        for (int ic = 0; ic < 8; ic++)
                        {
                            temparry[ib, ic] = input[i+ib,ia+ic];
                        }
                    }
                    ia = ia + 8;
                    returnarry[(i*input.GetLength(0) + ia *8)/64 -1]= temparry;
                }
            }
            return returnarry;
        }

        public byte[] HuffmanCalc(byte[] ACDCNrcodes, byte[] ACDCValues, byte[] YUVACDCHufftable )
        {
            byte i, j;
            byte tablePosition;
            short codeValue;

            byte[] tempTable = YUVACDCHufftable;

            codeValue = 0;
            tablePosition = 0;
            for ( i = 1; i <= 16; i++)
            {
                for (j = 1; j <= 16; j++)
                {
                    tempTable[ACDCValues[tablePosition]] = (byte) codeValue;
                    tempTable[ACDCValues[tablePosition]] = j;
                    tablePosition++;
                    codeValue++;
                }
                codeValue <<= 1;
            }
            return tempTable;
        }

        static byte[] YDCNodes = { 0, 0, 1, 5, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 };
        static byte[] YDCValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        static byte[] YACNodes = { 0, 0, 2, 1, 3, 3, 2, 4, 3, 5, 5, 4, 4, 0, 0, 1, 0x7d };
        static byte[] YACValues = {0x01, 0x02, 0x03, 0x00, 0x04, 0x11, 0x05, 0x12,
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

        static byte[] CbDCNodes = { 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        static byte[] CbDCValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        static byte[] CbACNodes = { 0, 0, 2, 1, 2, 4, 4, 3, 4, 7, 5, 4, 4, 0, 1, 2, 0x77 };
        static byte[] CbACValues = {0x00, 0x01, 0x02, 0x03, 0x11, 0x04, 0x05, 0x21,
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

        public static Byte[] ZigZagTable2 =
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

    }



}