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
            
            int height = pfr.GetHeight();
            int width = pfr.GetWidth();

            byte[] colors = new byte[] { };
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    col3[y, x] = pfr.ReadPicture().Data.PictureYX[y, x];
                    colors[0] = col3[y, x].a;
                    colors[1] = col3[y, x].b;
                    colors[2] = col3[y, x].c;
                     ml.RGBToYUV(colors);
                }
            }

            //create JPG head
            PictureHead.CreateJPGHead(bs, (ushort)height, (ushort)width);

            //TODO: DCT
            for (int y = 0; y < height; y += 8)
            {
                for (int x = 0; x < width; x += 8)
                {
                    //Y: DCT Quantization->ZigZag->Coefficiency->Bitstream
                    //Cr: DCT Quantization->ZigZag->Coefficiency->Bitstream
                    //Cb: DCT Quantization->ZigZag->Coefficiency->Bitstream
                }
            }

            bs.AddShort(0xffd9); //End of Picture Marker
            bs.WriteToFile(imagedest);
            
        }
    }
}