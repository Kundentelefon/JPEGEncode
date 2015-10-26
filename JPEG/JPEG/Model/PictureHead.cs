using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class PictureHead
    {
        byte Farbschema;
        public int pixelMaxX;
        public int pixelMaxY;
        //fals leer wird  4:4:4 genommen
        byte aAbtastschema;
        byte bAbtastschema;
        byte cAbtastschema;
    }
}
