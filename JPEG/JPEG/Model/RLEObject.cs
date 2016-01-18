using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG.Model
{
    struct RLEObject
    {
       public List<Bitstream> bitstreams;
       public List<SortedList<byte, List<bool>>> huffmantablesAC;
       public List<SortedList<byte, List<bool>>> huffmantablesDC;
    }
}
