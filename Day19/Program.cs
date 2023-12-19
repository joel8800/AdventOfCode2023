using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day19: Aplenty");

string[] blocks = FileUtil.ReadFileByBlock("input.txt");
string[] workFLowLines = blocks[0].Split(Environment.NewLine);
string[] partLines = blocks[1].Split(Environment.NewLine);

Dictionary<string, List<string>> workflows = [];
foreach (string line in workFLowLines)
{
    string[] pieces = line.Split('{');
    pieces[1] = pieces[1].Replace("}", "");
    List<string> rules = [.. pieces[1].Split(',')];
    workflows.Add(pieces[0], rules);
}

Regex rePart = new(@"\d+");
List<(int x, int m, int a, int s)> parts = [];
foreach (string line in partLines)
{
    MatchCollection mc = rePart.Matches(line);
    int x = int.Parse(mc[0].Value);
    int m = int.Parse(mc[1].Value);
    int a = int.Parse(mc[2].Value);
    int s = int.Parse(mc[3].Value);

    parts.Add((x, m, a, s));
}

int accepted = 0;
int rejected = 0;

foreach (var part in parts)
{
    string rule = "in";
    while (rule != "A" && rule != "R")
    {
        rule = NextCommand(workflows[rule], part);
    }
    if (rule == "A")
        accepted += part.x + part.m + part.a + part.s;
}

Console.WriteLine($"Part1: {accepted}");

//----------------------------------------------------------------------------

long maxCombos = 0;

foreach (var part in parts)
    maxCombos += FindMaxCombinations(workflows, part);

Console.WriteLine($"Part2: {maxCombos}");   // sample 167409079868000

//============================================================================

string NextCommand(List<string> rules, (int x, int m, int a, int s) part)
{
    foreach (string rule in rules)
    {
        if (rule == "A") return "A";
        if (rule == "R") return "R";

        if (rule.Contains(':') == false) return rule;

        char p = rule[0];
        char op = rule[1];
        string[] s = rule[2..^0].Split(':');
        //Console.WriteLine($"{p} {op} {s[0]} {s[1]}");

        int left = 0;
        int right = int.Parse(s[0]);
        switch (p)
        {
            case 'x': left = part.x; break; 
            case 'm': left = part.m; break;
            case 'a': left = part.a; break;
            case 's': left = part.s; break;
        }

        if (op == '<')
        {
            if (left < right)
                return s[1];
        }
        else
        {
            if (left > right)
                return s[1];
        }
    }

    Console.WriteLine("Should not reach here");
    return "";
}

long FindMaxCombinations(Dictionary<string, List<string>> workflows, (int x, int m, int a, int s) part)
{
    long maxCombos = 0;

    string rule = "in";
    while (rule != "A" && rule != "R")
    {
        rule = NextCommand(workflows[rule], part);
    }
    if (rule == "A")
        accepted += part.x + part.m + part.a + part.s;





    return maxCombos;
}