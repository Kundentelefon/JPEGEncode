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

            DCT.DCTdirect(yArray);
            DCT.DCTdirect(uArray);
            DCT.DCTdirect(vArray);

            //create JPG head
            PictureHead.CreateJPGHead(bs, (ushort)height, (ushort)width);

            
            //TODO: DCT
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bs.AddByte((byte)yArray[x, y]);
                    bs.AddByte((byte)uArray[x, y]);
                    bs.AddByte((byte)vArray[x, y]);
                }
            }

            bs.AddShort(0xffd9); //End of Picture Marker
            bs.WriteToFile(imagedest);
            

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
                    returnarry[(i*input.Length+ia*8)/64]= temparry;
                }
            }
            return returnarry;
        }
    }
}