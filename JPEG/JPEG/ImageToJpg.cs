using System;
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

            float[,] yArray = new float[width,height]; // width and height vertauscht
            float[,] uArray = new float[width,height];
            float[,] vArray = new float[width, height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    yArray[i, j] = (float)col3[j, i].a;
                    uArray[i, j] = (float)col3[j, i].b;
                    vArray[i, j] = (float)col3[j, i].c;
                }
            }

            DCT.DCTdirect(yArray);
            DCT.DCTdirect(uArray);
            DCT.DCTdirect(vArray);

            //create JPG head
            PictureHead.CreateJPGHead(bs, (ushort)height, (ushort)width);

            //TODO: DCT
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    bs.AddByte((byte)yArray[x, y]);
                    bs.AddByte((byte)uArray[x, y]);
                    bs.AddByte((byte)vArray[x, y]);
                }
            }

            bs.AddShort(0xffd9); //End of Picture Marker
            bs.WriteToFile(imagedest);
            
        }
    }
}