Console.WriteLine("Day15: Lens Library");

string input = File.ReadAllText("input.txt");
List<string> inputs = [.. input.Split(',')];

int result = 0;
foreach (string s in inputs)
    result += Hash(s);

Console.WriteLine($"Part1: {result}");

//----------------------------------------------------------------------------

List<List<(string label, int fc)>> boxes = [];

for (int i = 0; i < 256; i++)
    boxes.Add([]);

foreach (string s in inputs)
{
    if (s.Contains('-'))
    {
        string label = s.Replace("-", "");
        int hash = Hash(label);
        if (boxes[hash].Where(b => b.label == label).Any())
        {
            int idx = boxes[hash].FindIndex(b => b.label == label);
            boxes[hash].RemoveAt(idx);
        }
    }

    if (s.Contains('='))
    {
        string[] inst = s.Split('=');
        string label = inst[0];
        int fl = int.Parse(inst[1]);
        int hash = Hash(label);

        if (boxes[hash].Where(b => b.label == label).Any())
        {
            int idx = boxes[hash].FindIndex(b => b.label == label);
            boxes[hash].RemoveAt(idx);
            boxes[hash].Insert(idx, (label, fl));
        }
        else
        {
            boxes[hash].Add((label, fl));
        }
    }
    //PrintBoxes(boxes);
}

int focusPower = 0;
for (int i = 0; i < 256; i++)
    if (boxes[i].Count > 0)
        for (int j = 0; j < boxes[i].Count; j++)
            focusPower += (i + 1) * (j + 1) * boxes[i][j].fc;

Console.WriteLine($"Part2: {focusPower}");

//============================================================================

static int Hash(string label)
{
    int hash = 0;

    foreach (char c in label)
        hash = (hash + Convert.ToInt32(c)) * 17 % 256;

    return hash;
}