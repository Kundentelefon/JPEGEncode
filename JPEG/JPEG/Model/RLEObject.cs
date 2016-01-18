using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG.Model
{
    class RLEObject
    {
       public List<Bitstream> bitstreams;
       public SortedList<byte, List<bool>> huffmantablesAC;
       public SortedList<byte, List<bool>> huffmantablesDC;


    }
}
