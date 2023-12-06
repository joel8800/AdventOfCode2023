namespace Day05
{
    public class NumRange
    {
        public long st { get; set; }
        public long sp { get; set; }
        public long offset { get; set; }

        public NumRange()
        {

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
        public List<NumRange> InputRanges;
        
        public Translator()
        {
            InputRanges = [];
        }

        public void SortRanges()
        {
            InputRanges.Sort((x, y) => x.st.CompareTo(y.st));
        }

        public void PrintRanges()
        {
            Console.WriteLine("--- in ---------");
            foreach (NumRange nr in InputRanges)
                Console.WriteLine(nr.ToString());
        }

        public long Translate(long input)
        {
            foreach (NumRange nr in InputRanges)
            {
                if (input >= nr.st && input <= nr.sp)
                    return input + nr.offset;
            }

            return input;
        }

        public List<NumRange> Translate(List<NumRange> ranges)
        {
            List<NumRange> output = [];

            for (int i = 0; i < ranges.Count; i++) 
            {
                bool outputAdded = false;
                Console.WriteLine($"search  {ranges[i]}");

                // find range where start point is
                for (int j = 0; j < InputRanges.Count; j++)
                {
                    // start point is in range
                    if (InRange(ranges[i].st, InputRanges[j]))
                    {
                        if (InRange(ranges[i].sp, InputRanges[j]))
                        {
                            // stop is in this range
                            NumRange translated = TranslateFullRange(ranges[i], j);
                            output.Add(translated);
                            outputAdded = true;
                            break;
                        }
                        else
                        {
                            // stop is outside of this range
                            List<NumRange> translated = TranslatePartialRanges(ranges[i], j);
                            foreach (NumRange nr in translated)
                                output.Add(nr);
                            outputAdded = true;
                            break;
                        }
                    }
                }

                if (outputAdded == false)
                {
                    output.Add(ranges[i]);
                    Console.WriteLine($"--  none[{ranges[i].st,11}..{ranges[i].sp,11}] added, no offset applied");
                }
            }

            return output;
        }

        private bool InRange(long num, NumRange range)
        {
            if (num >= range.st &&  num <= range.sp)
                return true;

            return false;
        }

        private NumRange TranslateFullRange(NumRange range, int inRangeIdx)
        {
            NumRange outRange = new()
            {
                st = range.st + InputRanges[inRangeIdx].offset,
                sp = range.sp + InputRanges[inRangeIdx].offset
            };
            Console.WriteLine($"--  full[{outRange.st,11}..{outRange.sp,11}] added, offset {InputRanges[inRangeIdx].offset,11} applied");
            return outRange;
        }

        private List<NumRange> TranslatePartialRanges(NumRange range, int inRangeIdx)
        {
            List<NumRange> outRanges = [];
            NumRange lowerRange = new()
            {
                // translate lower part that is in range
                st = range.st + InputRanges[inRangeIdx].offset,
                sp = InputRanges[inRangeIdx].sp + InputRanges[inRangeIdx].offset
            };
            Console.WriteLine($"-- lower[{lowerRange.st,11}..{lowerRange.sp,11}] added, offset {InputRanges[inRangeIdx].offset,11} applied");
            outRanges.Add(lowerRange);

            // create new range for the upper part
            NumRange newRange = new(InputRanges[inRangeIdx].sp + 1, range.sp, 0);

            if (inRangeIdx <= InputRanges.Count - 1)
            {
                // translate upper part with next range's offset
                newRange.st += InputRanges[inRangeIdx + 1].offset;
                newRange.sp += InputRanges[inRangeIdx + 1].offset;
                outRanges.Add(newRange);
                Console.WriteLine($"-- upper[{newRange.st,11}..{newRange.sp,11}] added, offset {InputRanges[inRangeIdx + 1].offset,11} applied");
            }
            else
            {
                // not in next range so return as is
                outRanges.Add(newRange);
                Console.WriteLine($"-- upper[{newRange.st,11}..{newRange.sp,11}] added, not in next range");
            }


            return outRanges;
        }
    }
}
