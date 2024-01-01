using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day19: Aplenty");

string[] blocks = FileUtil.ReadFileByBlock("input.txt");

string[] workFLowLines = blocks[0].Split(Environment.NewLine);
string[] partLines = blocks[1].Split(Environment.NewLine);

Dictionary<string, List<List<string>>> workflows = [];

// parse workflow rules
Regex reWFRule = new(@"(\w)(\W)(\d+):(\w+)");
foreach (string line in workFLowLines)
{
    string name = line.Split('{')[0];
    string[] rest = line.Split('{')[1].Replace("}", "").Split(',');

    workflows[name] = [];
    foreach (string ruleLine in rest)
    {
        List<string> rule = [];
        if (ruleLine.Contains(':'))
        {
            MatchCollection mc = reWFRule.Matches(ruleLine);
            rule.Add(mc[0].Groups[1].Value);
            rule.Add(mc[0].Groups[2].Value);
            rule.Add(mc[0].Groups[3].Value);
            rule.Add(mc[0].Groups[4].Value);
        }
        else
        {
            rule.Add(ruleLine);
        }
        workflows[name].Add(rule);
    }
}

// parse parts
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

foreach (var part in parts)
{
    string workflow = "in";
    while (workflow != "A" && workflow != "R")
    {
        workflow = FollowWorkflow(workflows[workflow], part);
    }
    if (workflow == "A")
        accepted += part.x + part.m + part.a + part.s;
}

Console.WriteLine($"Part1: {accepted}");

//----------------------------------------------------------------------------

Dictionary<string, (int lo, int hi)> ranges = [];
ranges["x"] = (1, 4000);
ranges["m"] = (1, 4000);
ranges["a"] = (1, 4000);
ranges["s"] = (1, 4000);

long combos = FindCombinations(ranges, "in");

Console.WriteLine($"Part2: {combos}");

//============================================================================

string FollowWorkflow(List<List<string>> rules, (int x, int m, int a, int s) part)
{
    foreach (List<string> rule in rules)
    {
        if (rule[0] == "A") return "A";
        if (rule[0] == "R") return "R";

        if (rule.Count == 1) return rule[0];    // catch-all

        int leftSide = 0;
        int rightSide = int.Parse(rule[2]);
        switch (rule[0])
        {
            case "x": leftSide = part.x; break;
            case "m": leftSide = part.m; break;
            case "a": leftSide = part.a; break;
            case "s": leftSide = part.s; break;
        }

        if (rule[1] == "<")
        {
            if (leftSide < rightSide)
                return rule[3];
        }
        else
        {
            if (leftSide > rightSide)
                return rule[3];
        }
    }

    Console.WriteLine("Should not reach here");
    return "";
}

// Part 2 converted from Hyperneutrino's python solution
long FindCombinations(Dictionary<string, (int lo, int hi)> ranges, string name)
{
    if (name == "R")
        return 0;

    if (name == "A")
    {
        long product = 1;
        foreach ((int lo, int hi) in ranges.Values)
            product *= hi - lo + 1;
        return product;
    }

    List<List<string>> rules = workflows[name];
    string catchAll = rules.Last()[0];

    long total = 0;

    foreach (var r in rules)
    {
        if (r.Count <= 1)
            continue;

        string key = r[0];
        string cmp = r[1];
        string target = r[3];
        int n = int.Parse(r[2]);
        
        (int lo, int hi) trueRange;
        (int lo, int hi) falseRange;

        (int lo, int hi) = ranges[key];
        
        if (cmp == "<")
        {
            trueRange = (lo, Math.Min(n - 1, hi));
            falseRange = (Math.Max(n, lo), hi);
        }
        else
        {
            trueRange = (Math.Max(n + 1, lo), hi);
            falseRange = (lo, Math.Min(n, hi));
        }

        Dictionary<string, (int lo, int hi)> copy = new(ranges){ [key] = trueRange };
        total += FindCombinations(copy, target);

        ranges[key] = falseRange;
    }
    
    total += FindCombinations(ranges, catchAll);

    return total;
}