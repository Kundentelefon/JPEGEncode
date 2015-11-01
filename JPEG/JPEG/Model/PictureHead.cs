using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class PictureHead
    {

        public int pixelMaxX;
        public int pixelMaxY;

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
            bs.AddByte(j);
            bs.AddByte(f);
            bs.AddByte(i);
            bs.AddByte(f);
            bs.AddByte(zero);
            bs.AddByte(majorRevisionNumber);
            bs.AddByte(minorRevisionNumber);
            bs.AddByte(xyunits);
            bs.AddShort(xPixelDensity);
            bs.AddShort(yPixelDensity);
            bs.AddByte(thumbnailWidth);
            bs.AddByte(thumbnailHeight);
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
            bs.AddByte(precision);
            bs.AddShort(pictureHeight);
            bs.AddShort(pictureWidth);
            bs.AddByte(components);
            bs.AddByte(IDY); // 3 Bytes for each component
            bs.AddByte(SFY);
            bs.AddByte(QTY);
            bs.AddByte(IDCb);
            bs.AddByte(SFCb);
            bs.AddByte(QTCb);
            bs.AddByte(IDCr);
            bs.AddByte(SFCr);
            bs.AddByte(QTCr);
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
            SOF0Head(bs, pHeight, pWidth);
            PictureEnd(bs);
        }
        
    }
}
