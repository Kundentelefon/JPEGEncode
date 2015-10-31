using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class JPGHeader
    {
        public void APP0Head(byte[] ba)
        {
            short length = 16; // length of segment >=16
            short j = 0x4a; // string J
            short f = 0x46; // string f
            short i = 0x49; // string I
            short zero = 0x00; // string #0
            byte majorRevisionNumber = 1;
            byte minorRevisionNumber = 1; // together 1.1
            byte xyunits = 0; // no unit = normal aspect ratio
            short xPixelDensity = 0x0048; // x density
            short yPixelDensity = 0x0048; // y density
            byte thumbnailWidth = 0;
            byte thumbnailHeight = 0; // no thumbnail

            Bitstream bs = new Bitstream();
            bs.WriteShort(ba, (short)0xFFD8); //initialises JPG
            bs.WriteShort(ba, (short)0xFFE0); //marker
            bs.WriteShort(ba, length); //length
            bs.WriteShort(ba, j);
            bs.WriteShort(ba, f);
            bs.WriteShort(ba, i);
            bs.WriteShort(ba, f);
            bs.WriteShort(ba, zero);
            bs.WriteByte(ba, majorRevisionNumber);
            bs.WriteByte(ba, minorRevisionNumber);
            bs.WriteByte(ba, xyunits);
            bs.WriteShort(ba, xPixelDensity);
            bs.WriteShort(ba, yPixelDensity);
            bs.WriteByte(ba, thumbnailWidth);
            bs.WriteByte(ba, thumbnailHeight);
        }

        public void SOF0Head(byte[] bw, short pHeight, short pWidth)
        {
            short length = 17; // 8 + (Y,Cb,Cr) * 3
            short precision = 8; // precision of data in bits/sample
            short pictureHeight = pHeight;
            short pictureWidth = pWidth;
            byte components = 3; // 3 for Y,Cb,Cr
            byte IDY = 1; // ID component 1 = Y
            byte SFY = 0x11; // sampling factor for Y (bit 0-3 vertical, bit 4-7 horicontal)
            byte QTY = 0; // quantization table of Y = 0
            byte IDCb = 2; // ID component 2 = Cb
            byte SFCb = 0x11;
            byte QTCb = 1; // quantization table of Cb = 1
            byte IDCr = 3; // ID component 3 = Cr
            byte SFCr = 0x11;
            byte QTCr = 1;

            Bitstream bs = new Bitstream();
            bs.WriteShort(bw, (short)0xFFC0); // marker
            bs.WriteShort(bw, length);
            bs.WriteShort(bw, pictureHeight);
            bs.WriteShort(bw, pictureWidth);
            bs.WriteByte(bw, components);
            bs.WriteByte(bw, IDY); // 3 Bytes for each component
            bs.WriteByte(bw, SFY);
            bs.WriteByte(bw, QTY);
            bs.WriteByte(bw, IDCb);
            bs.WriteByte(bw, SFCb);
            bs.WriteByte(bw, QTCb);
            bs.WriteByte(bw, IDCr);
            bs.WriteByte(bw, SFCr);
            bs.WriteByte(bw, QTCr);
        }

        public void CreateJPGHead(byte[] bh, short pHeight, short pWidth)
        {
            APP0Head(bh);
            SOF0Head(bh, pHeight, pWidth);
        }
    }
}
