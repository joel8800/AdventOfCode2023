using AoCUtils;

Console.WriteLine("Day12: Hot Springs");

string[] input = FileUtil.ReadFileByLine("input.txt");

long combosPt1 = 0;
Dictionary<(int, int, int), long> cache = [];

foreach (string line in input)
{
    string[] parts = line.Split();
    
    List<int> counts = parts[1].Split(',').Select(c => int.Parse(c)).ToList();
    cache.Clear();

    combosPt1 += CombosPt2(parts[0], counts, 0, 0, 0);
    //combosPt1 += CombosInRow(parts[0], parts[1]);
}

Console.WriteLine($"Part1: {combosPt1}");

//----------------------------------------------------------------------------

long combosPt2 = 0;
foreach (string line in input)
{
    string[] parts = line.Split();
    string row5x = string.Join("?", Enumerable.Repeat(parts[0], 5));
    string counts5x = string.Join(",", Enumerable.Repeat(parts[1], 5));
    
    List<int> counts = counts5x.Split(',').Select(c => int.Parse(c)).ToList();
    cache.Clear();

    combosPt2 += CombosPt2(row5x, counts, 0, 0, 0);
}

Console.WriteLine($"Part2: {combosPt2}");

//============================================================================

// Couldn't figure out part2. My original solution to part1 was to make each ? a
// bit in an integer and try all combinations.  It was slow and did not scale to part2.
// This is Jonathan Paulson's Python solution converted to C#
long CombosPt2(string row, List<int> nums, int rowIdx, int numIdx, int current)
{
    long combos = 0;
    (int, int, int) key = (rowIdx, numIdx, current);

    if (cache.TryGetValue(key, out long value))
        return value;

    if (rowIdx == row.Length)
    {
        if (numIdx == nums.Count && current == 0)
            return 1;
        else if (numIdx == nums.Count - 1 && nums[numIdx] == current)
            return 1;
        else
            return 0;
    }

    foreach (char c in ".#")
    {
        if (row[rowIdx] == c || row[rowIdx] == '?')
        {
            if (c == '.' && current == 0)
                combos += CombosPt2(row, nums, rowIdx + 1, numIdx, 0);
            else if (c == '.' && current > 0 && numIdx < nums.Count && nums[numIdx] == current)
                combos += CombosPt2(row, nums, rowIdx + 1, numIdx + 1, 0);
            else if (c == '#')
                combos += CombosPt2(row, nums, rowIdx + 1, numIdx, current + 1);
        }
    }

    cache[key] = combos;

    return combos;
}
