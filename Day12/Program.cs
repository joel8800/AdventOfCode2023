using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day12: Hot Springs");

string[] input = FileUtil.ReadFileByLine("inputSample.txt");
int combos = 0;
foreach (string line in input)
{
    string[] cr = line.Split(' ');
    string grpStr = Regex.Replace(cr[0], @"\.+", ".");
    List<string> groups = grpStr.Split('.', StringSplitOptions.RemoveEmptyEntries).ToList();
    List<int> counts = cr[1].Split(',').Select(x => int.Parse(x)).ToList();

    if (groups.Count == counts.Count)
    {
        for (int i = 0; i < groups.Count; i++)
            combos += CombosInGroup(groups[i], counts[i]);
    }
    else
    {
        Console.WriteLine("not implemented");
    }

}


Console.WriteLine($"Part1: {0}");
Console.WriteLine($"Part2: {0}");

//============================================================================

int CombosInGroup(string group, int count)
{
    int combos = 0;

    return combos;
}