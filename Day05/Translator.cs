using System;
using System.Reflection.Metadata.Ecma335;

namespace Day05
{
    public class NumRange
    {
        public long st { get; set; }
        public long sp { get; set; }
        public long offset { get; set; }

        public NumRange()
        { }

        public NumRange(string rangeText)
        {
            string[] strs = rangeText.Split();
            List<long> mapNums = strs.Select(x => long.Parse(x)).ToList();
            
            st = mapNums[1];
            sp = mapNums[1] + mapNums[2] - 1;
            offset = mapNums[0] - mapNums[1];
        }

        public NumRange(long start, long stop, long offset)
        {
            st = start;
            sp = stop;
            this.offset = offset;
        }

        public override string ToString()
        {
            return $"[{st,11}..{sp,11}: {offset,11}]";
        }
    }


    internal class Translator
    {
        public List<NumRange> MapRanges;
        
        public Translator()
        {
            MapRanges = [];
        }

        public void SortRanges()
        {
            MapRanges.Sort((x, y) => x.st.CompareTo(y.st));
        }

        // part1
        public long Translate(long input)
        {
            foreach (NumRange nr in MapRanges)
                if (input >= nr.st && input <= nr.sp)
                    return input + nr.offset;

            return input;
        }

        // part2
        public List<NumRange> Translate(List<NumRange> seedRanges)
        {
            List<NumRange> newSeeds = [];

            while (seedRanges.Count > 0)
            {
                bool foundIntersect = false;

                NumRange sr = seedRanges[0];
                seedRanges.RemoveAt(0);

                foreach (NumRange mapRange in MapRanges)
                {
                    long overlapSt = Math.Max(sr.st, mapRange.st);
                    long overlapSp = Math.Min(sr.sp, mapRange.sp);

                    if (overlapSt < overlapSp)  // intersecting range
                    {
                        foundIntersect = true;
                        newSeeds.Add(new(overlapSt + mapRange.offset, overlapSp + mapRange.offset, 0));
                        
                        if (overlapSt > sr.st)  // left side leftover
                            seedRanges.Add(new(sr.st, overlapSt, 0));
                        
                        if (sr.sp > overlapSp)  // right side leftover
                            seedRanges.Add(new(overlapSp + 1, sr.sp, 0));
                        
                        break;
                    }
                }
                if (foundIntersect == false)
                    newSeeds.Add(sr);
            }

            return newSeeds;
        }
    }
}
