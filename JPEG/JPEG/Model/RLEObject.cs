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
       public List<SortedList<byte, List<bool>>> huffmantablesAC= new List<SortedList<byte, List<bool>>>();
       public List<SortedList<byte, List<bool>>> huffmantablesDC= new List<SortedList<byte, List<bool>>>();


    }
}
