using AoCUtils;
using Day05;

Console.WriteLine("Day05: If You Give A Seed A Fertilizer");

string[] input = FileUtil.ReadFileByBlock("input.txt");

List<string> seedsText = [.. input[0].Split()];
seedsText.RemoveAt(0);
List<long> seedNums = seedsText.Select(x => long.Parse(x)).ToList();

List<List<string>> mapData = [];
for (int i = 1; i <= 7; i++)
{
    List<string> mapText = [.. input[i].Split(Environment.NewLine)];
    mapText.RemoveAt(0);
    mapData.Add(mapText);
}

List<Translator> maps = [];
foreach (List<string> mapText in mapData)
{
    Translator t = new();

    foreach (string range in mapText)
    {
        string[] strs = range.Split();
        List<long> mapNums = strs.Select(x => long.Parse(x)).ToList();
        NumRange ix = new()
        {
            st = mapNums[1],
            sp = mapNums[1] + mapNums[2] - 1,
            offset = mapNums[0] - mapNums[1]
        };
        t.InputRanges.Add(ix);
    }

    t.SortRanges();
    maps.Add(t);
}

List<long> locations = [];
foreach (long seed in seedNums)
{
    long x = seed;

    for (int i = 0; i < maps.Count; i++)
        x = maps[i].Translate(x);

    locations.Add(x);
}

Console.WriteLine($"Part1: {locations.Min()}");

//----------------------------------------------------------------------------

foreach (Translator t in maps)
{
    for (int i = 1; i < t.InputRanges.Count; i++)
    {
        if ((t.InputRanges[i].st - t.InputRanges[i - 1].sp) != 1)
            Console.WriteLine($"*** gap found between {i-1} and {i}");
    }

    t.PrintRanges();
    Console.WriteLine("-------------------------------------");
}

List<NumRange> seedRanges = [];
for (int i = 0; i < seedNums.Count - 1; i += 2)
{
    Console.WriteLine($"seed {i}");
    NumRange nr = new()
    {
        st = seedNums[i],
        sp = seedNums[i] + seedNums[i + 1] - 1,
        offset = 0
    };

    seedRanges.Add(nr);
}

foreach (NumRange nr in seedRanges)
{
    Console.WriteLine(nr.ToString());
}

Console.WriteLine("=====================================");
List<NumRange> sr1 = maps[0].Translate(seedRanges);
foreach (NumRange nr in sr1) { Console.WriteLine("sr1: " + nr.ToString()); }
Console.WriteLine("-------------------------------------");
List<NumRange> sr2 = maps[1].Translate(sr1);
foreach (NumRange nr in sr2) { Console.WriteLine("sr2: " + nr.ToString()); }
Console.WriteLine("-------------------------------------");
List<NumRange> sr3 = maps[2].Translate(sr2);
foreach (NumRange nr in sr3) { Console.WriteLine("sr3: " + nr.ToString()); }
Console.WriteLine("-------------------------------------");
List<NumRange> sr4 = maps[3].Translate(sr3);
foreach (NumRange nr in sr4) { Console.WriteLine("sr4: " + nr.ToString()); }
Console.WriteLine("-------------------------------------");
List<NumRange> sr5 = maps[4].Translate(sr4);
foreach (NumRange nr in sr5) { Console.WriteLine("sr5: " + nr.ToString()); }
Console.WriteLine("-------------------------------------");
List<NumRange> sr6 = maps[5].Translate(sr5);
foreach (NumRange nr in sr6) { Console.WriteLine("sr6: " + nr.ToString()); }
Console.WriteLine("-------------------------------------");
List<NumRange> sr7 = maps[6].Translate(sr6);
foreach (NumRange nr in sr7) { Console.WriteLine("sr7: " + nr.ToString()); }
Console.WriteLine("=====================================");

long minVal = sr7.Select(x => x.st).Min();
Console.WriteLine($"Part2: {minVal}"); //50855035

//============================================================================

int FindOverlapping(int start1, int end1, int start2, int end2)
{
    return Math.Max(0, Math.Min(end1, end2) - Math.Max(start1, start2) + 1);
}