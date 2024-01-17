using AoCUtils;

Console.WriteLine("Day25: Snowverload");

string[] input = FileUtil.ReadFileByLine("input.txt");

Dictionary<string, HashSet<string>> modules = [];
foreach (string line in input)
{
    string[] names = line.Replace(":", "").Split(' ');
    string name = names[0];
    List<string> connections = [.. names[1..]];

    modules[name] = [];
    foreach (string c in connections)
        modules[name].Add(c);
}

// add potential reverse connections
List<string> keys = [.. modules.Keys];
foreach(string key in keys)
{
    foreach (string c in modules[key])
    {
        if (modules.ContainsKey(c) == false)
            modules[c] = [];

        modules[c].Add(key);
    }
}
//PrintModules(modules);

List<int> counts = [];
List<string> wireGroup = [.. modules.Keys];
while (true)
{
    counts.Clear();
    HashSet<string> wireGroupSet = [.. wireGroup];

    foreach (string wire in wireGroup)
    {
        int count = modules[wire].Except(wireGroupSet).Count();
        counts.Add(count);
    }
    
    if (counts.Sum() == 3)
        break;

    string rmWire = wireGroup[counts.FindIndex(x => x == counts.Max())];
    wireGroup.Remove(rmWire);
}

int answerPt1 = wireGroup.Count * modules.Keys.Except(wireGroup).Count();

Console.WriteLine($"Part1: {answerPt1}");

//============================================================================

void PrintModules(Dictionary<string, HashSet<string>> modules)
{
    foreach (string key in modules.Keys)
        Console.WriteLine($"{key}: {modules[key].Count} => ({string.Join(',', modules[key])})");

}
