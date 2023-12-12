using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    internal class Node
    {
        public Node? Prev { get; set; }
        public Node? Next { get; set; }

        public char Ch { get; set; }
        public string Data { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Node(char ch, int row, int col)
        {
            Ch = ch;
            Row = row;
            Col = col;
            Data = MakeNodeString(row, col);

            Prev = null;
            Next = null;
        }

        static public string MakeNodeString(int row, int col)
        {
            return $"{row:000}{col:000}";
        }

        public bool MatchLocation(int row, int col)
        {
            if (row == Row && col == Col)
                return true ;
            return false;
        }

        public override string ToString()
        {
            return $"[{Row:000},{Col:000}]";
        }
    }
}
