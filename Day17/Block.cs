namespace Day17
{
    // record works but don't include Loss in seen HashSet
    // class works if equatable methods are overridden
    //public record BlockRec
    //{
    //    public BlockRec(int hl, int r, int c, int dr, int dc, int n)
    //    {
    //        Loss = hl;
    //        R = r;
    //        C = c;
    //        DR = dr;
    //        DC = dc;
    //        Steps = n;
    //    }

    //    public int Loss { get; set; }
    //    public int R { get; set; }
    //    public int C { get; set; }
    //    public int DR { get; set; }
    //    public int DC { get; set; }
    //    public int Steps { get; set; }
    //}

    // class requires overriding Equals and GetHashCode methods to work in collections
    // otherwise two Blocks will always be not equal even if all fields are equal
    public class Block //: IEquatable<Block>
    {
        public int Loss { get; set; }
        public int R { get; set; }
        public int C { get; set; }
        public int DR { get; set; }
        public int DC { get; set; }
        public int Steps { get; set; }
        public int Hash { get; set; }

        public Block(int cost, int row, int col, int dRow, int dCol, int steps)
        {
            Loss = cost;
            R = row;
            C = col;
            DR = dRow;
            DC = dCol;
            Steps = steps;
            Hash = CalcHashCode();
        }

        // use row-col coords, direction, and step count for hash
        private int CalcHashCode()
        {
            int hash = 17;
            hash = hash * 11 + R;
            hash = hash * 11 + C;
            hash = hash * 13 + DR;
            hash = hash * 13 + DC;
            hash = hash * 19 + Steps;
            return hash;
        }

        // optional, easier to debug when you can print
        public override string ToString()
        {
            return $"{Loss}:[{R},{C}]:[{DR},{DC}]:{Steps}";
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override bool Equals(object? obj)
        {
            //return Hash == ((Block)obj).Hash;
            return Equals(obj as Block);
        }

        public bool Equals(Block? other)
        {
            //return Hash == other.Hash;
            return other != null &&
                R == other.R &&
                C == other.C &&
                DR == other.DR &&
                DC == other.DC &&
                Steps == other.Steps;
        }
    }
}
