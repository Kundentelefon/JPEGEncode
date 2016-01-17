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
                    colors[0] = col3[y, x].a;
                    colors[1] = col3[y, x].b;
                    colors[2] = col3[y, x].c;
                    colors = ml.RGBToYUV(colors);
                    col3[y, x].a = colors[0];
                    col3[y, x].b = colors[1];
                    col3[y, x].c = colors[2];
                }
            }

            //create JPG head
            PictureHead.CreateJPGHead(bs, (ushort)height, (ushort)width);

            
            //TODO: DCT
            for (int y = 0; y < height; y ++)
            {
                for (int x = 0; x < width; x ++)
                {
                    //bs.AddByte(col3[y, x].a);
                    //bs.AddByte(col3[y, x].b);
                    //bs.AddByte(col3[y, x].c);
                }
            }

            bs.AddShort(0xffd9); //End of Picture Marker
            bs.WriteToFile(imagedest);
            

        }

        public float[][,] aufteilen(float[,]input)
        {
            float[][,] returnarry= new float[(input.Length* input.GetLength(0))/64][,];
            for (int i = 0; i < input.Length; i=i+8)
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