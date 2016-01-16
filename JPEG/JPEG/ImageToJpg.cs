using System.Runtime.CompilerServices;

namespace JPEG
{
    public class ImageToJpg
    {
        public void PPMtoJpg(string imageSrc, string imagedest)
        {
            Bitstream bs = new Bitstream();
            Color3 col = new Color3();
            PlainFormatReader pfr = new PlainFormatReader(imageSrc);
            
            int height = pfr.GetHeight();
            int width = pfr.GetWidth();

            //returns PictureData
            pfr.ReadPicture();
            //TODO:convert PicutreData RGB to YCrCb

            byte Y = col.a;
            byte Cr = col.b;
            byte Cb = col.c;
            
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