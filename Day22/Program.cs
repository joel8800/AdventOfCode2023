using AoCUtils;

Console.WriteLine("Day22: Sand Slabs");

string[] input = FileUtil.ReadFileByLine("inputSample.txt");

foreach (string line in input)
{
    string[] ends = line.Split('~');
    int[] p1 = ends[0].Split(',').Select(n => int.Parse(n)).ToArray();
    int[] p2 = ends[1].Split(',').Select(n => int.Parse(n)).ToArray();
}

Console.WriteLine($"Part1: {0}");
Console.WriteLine($"Part2: {0}");

//============================================================================
