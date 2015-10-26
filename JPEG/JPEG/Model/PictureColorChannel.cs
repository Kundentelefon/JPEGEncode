using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    // Prototype for ColorChannel
    //class PictureColorChannel
    //{
    //    public int Width { get; private set; }
    //    public int Height { get; private set; }

    //    public byte[,] RChannel { get; private set; }
    //    public byte[,] GChannel { get; private set; }
    //    public byte[,] BChannel { get; private set; }

    //    public PictureColorChannel(Color[,] pictureData)
    //    {
    //        int rows = pictureData.GetLength(0);
    //        int cols = pictureData.GetLength(1);

    //        Width = cols;
    //        Height = rows;

    //        RChannel = new byte[rows,cols];
    //        GChannel = new byte[rows,cols];
    //        BChannel = new byte[rows,cols];
    //        for (int rowIdx = 0; rowIdx < rows; rowIdx++)
    //        {
    //            for (int colIdx = 0; colIdx < cols; colIdx++)
    //            {
    //                Color curPixel = pictureData[rowIdx, colIdx];
    //                RChannel[rowIdx, colIdx] = curPixel.a;
    //                GChannel[rowIdx, colIdx] = curPixel.b;
    //                BChannel[rowIdx, colIdx] = curPixel.c;
    //            }
    //        }
    //    }
    //}
}
