using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        //Variables & Constructor
        public byte symbol;
        public int frequency;

        public HuffmanNode parent;
        public HuffmanNode left;
        public HuffmanNode right;
        public bool isLeaf;
        public bool isDepthNode;

        public HuffmanNode(byte value) //Create a HuffmanNode from scratch
        {
            symbol = value;
            frequency = 1;

            parent = left = right = null;
            
            isLeaf = true;
            isDepthNode = false;
        }

        public HuffmanNode(int frequency, HuffmanNode left, HuffmanNode right)
        {
            this.left = left;
            this.right = right;
            this.frequency = frequency;
            isDepthNode = true;
        }

        public HuffmanNode(HuffmanNode nodeLower, HuffmanNode nodeHigher) //Combine two HuffmanNodes to form a tree
        {
            parent = null;
            isLeaf = false;

            right = nodeHigher;
            left = nodeLower;
            right.parent = left.parent = this;
            frequency = nodeHigher.frequency + nodeLower.frequency;
        }

        //Functions
        public int CompareTo(HuffmanNode other)
        {
            return other.frequency - frequency;
        }

        public void FrequencyIncrease()
        {
            frequency++;
        }
    }
}
