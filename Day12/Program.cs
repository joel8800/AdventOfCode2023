using AoCUtils;
using System.Text;

Console.WriteLine("Day12: Hot Springs");

string[] input = FileUtil.ReadFileByLine("input.txt");
int combos = 0;

foreach (string line in input)
    combos += CombosInRow(line);

Console.WriteLine($"Part1: {combos}");

foreach (string line in input)
{

}


Console.WriteLine($"Part2: {0}");

//============================================================================

int CombosInRow(string entry)
{
    int combos = 0;

    string row = entry.Split(' ')[0];
    string counts = entry.Split(' ')[1];
    int unknown = entry.Where(c => c == '?').Count();

    Console.WriteLine($"unknowns: {unknown}");
    //Console.WriteLine($"{row} -- {counts}");

    for (int i = 0; i < Math.Pow(2, unknown); i++)
    {
        List<char> binary = [.. Convert.ToString(i, 2).PadLeft(unknown, '0')];
        StringBuilder sb = new();

        //Console.Write($"-- binary:{Convert.ToString(i, 2).PadLeft(unknown, '0')} ");

        foreach (char c in row)
        {
            if (c != '?')
            {
                sb.Append(c == '.' ? '.' : '#');
            }
            else
            {
                sb.Append(binary[0] == '0' ? '.' : '#');
                binary.RemoveAt(0);
            }
        }

        string result = CountRuns(sb.ToString());
        //Console.WriteLine($"- {sb} -- {result}");

        if (result == counts)
            combos++;
    }

    //Console.WriteLine($"combos found: {combos}");
    return combos;
}

string CountRuns(string row)
{
    List<int> runs = [];

    bool isInRun = false;
    int count = 0;
    foreach (char c in row)
    {
        if (isInRun)
        {
            if (c == '.')
            {
                isInRun = false;
                runs.Add(count);
                count = 0;
            }
            else
                count++;
        }
        else
        {
            if (c == '#')
            {
                isInRun = true;
                count = 1;
            }
        }
    }

    if (isInRun)
        runs.Add(count);

    StringBuilder sb = new();
    for (int i = 0; i < runs.Count; i++)
    {
        sb.Append(runs[i]);
        if (i < runs.Count - 1)
            sb.Append(',');
    }

    return sb.ToString();
}