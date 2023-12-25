using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Block
    {
        public int H { get; set; }
        public int R { get; set; }
        public int C { get; set; }
        public int DR { get; set; }
        public int DC { get; set; }
        public int N { get; set; }
        public int HC { get; set; }

        public Block(int h, int r, int c, int dR, int dC, int n)
        {
            H = h;
            R = r;
            C = c;
            DR = dR;
            DC = dC;
            N = n;
            HC = CalcHashCode();
            Console.WriteLine($"hash code: {HC}");
        }

        private int CalcHashCode()
        {
            int hash = 17;
            hash = hash * 31 + H;
            hash = hash * 13 + R;
            hash = hash * 13 + C;
            hash = hash * 11 + DR;
            hash = hash * 11 + DC;
            hash = hash * 23 + N;
            return hash;
        }

        public override string ToString()
        {
            return $"{H}:[{R},{C}]:[{DR},{DC}]:{N}";
        }

        public override int GetHashCode()
        {
            return HC;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            return HC == ((Block) obj).HC;
        }
    }
}
