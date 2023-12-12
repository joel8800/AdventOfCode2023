using AoCUtils;
using Day05;

Console.WriteLine("Day05: If You Give A Seed A Fertilizer");

string[] input = FileUtil.ReadFileByBlock("input.txt");

List<long> seedNums = [.. input[0].Split(": ")[1].Split().Select(x => long.Parse(x))];

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
        t.MapRanges.Add(ix);
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

List<NumRange> seedRanges = [];
for (int i = 0; i < seedNums.Count - 1; i += 2)
{
    NumRange nr = new()
    {
        st = seedNums[i],
        sp = seedNums[i] + seedNums[i + 1] - 1,
        offset = 0
    };

    seedRanges.Add(nr);
}

foreach (Translator map in maps)
    seedRanges = map.Translate(seedRanges);

Console.WriteLine($"Part2: {seedRanges.Select(x => x.st).Min()}"); //50855035

//============================================================================
