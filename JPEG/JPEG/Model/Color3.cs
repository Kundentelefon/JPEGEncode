using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPEG.Model;

namespace JPEG
{
    class Color3 : Color
    {
        public byte a;
        public byte b;
        public byte c;


        public Color1 ConvertToColor1()
        {
            Color1 color = new Color1();
            color.a = a;

            return color;
        }
        public void Color3AdditionA(Color3 inputColor)
        {
            a += inputColor.a;         
        }

        public void Color3AdditionB(Color3 inputColor)
        {
            b += inputColor.b;
        }

        public void Color3AdditionC(Color3 inputColor)
        {
            c += inputColor.c;
        }

        public void Color3DivisionB(int divisor)
        {
            b = (byte) (b / divisor);
        }

        public void Color3DivisionC(int divisor)
        {
            c = (byte)(c / divisor);
        }
    }
}
