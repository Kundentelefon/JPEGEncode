using System.Runtime.CompilerServices;

namespace JPEG
{
    public class ImageToJpg
    {
        public void PPMtoJpg(string imageSrc, string imagedest)
        {
            Bitstream bs = new Bitstream();
            Color3[,] col3 = new Color3[,] {};
            PlainFormatReader pfr = new PlainFormatReader(imageSrc);
            Mathloc ml = new Mathloc();

            Picture pic = pfr.ReadPicture();

            int height = pic.Head.pixelMaxY;
            int width = pic.Head.pixelMaxX;

            //convert RGB to YUV for each pixel
            byte[] colors = {};
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
            for (int y = 0; y < height; y += 8)
            {
                for (int x = 0; x < width; x += 8)
                {
                    bs.AddByte(col3[y, x].a);
                    bs.AddByte(col3[y, x].b);
                    bs.AddByte(col3[y, x].c);
                }
            }

            bs.AddShort(0xffd9); //End of Picture Marker
            bs.WriteToFile(imagedest);
            
        }
    }
}