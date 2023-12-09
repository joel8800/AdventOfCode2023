using AoCUtils;
using Day08;
using System.Text.RegularExpressions;

Console.WriteLine("Day08: ");

string[] input = FileUtil.ReadFileByBlock("input.txt");
string[] nodeText = input[1].Split(Environment.NewLine);

Regex reNode = new(@"\w{3}");

List<Node> nodes = [];
foreach (string nodeInfo in nodeText)
{
    MatchCollection mc = reNode.Matches(nodeInfo);
    Node n = new(mc[0].Value, mc[1].Value, mc[2].Value);
    nodes.Add(n);
}

int stepsPt1 = 0;
string inst = input[0];
string current = "AAA";

// follow instructions until we reach ZZZ
while (current != "ZZZ")
{
    for (int i = 0; i < inst.Length; i++)
    {
        Node? n = nodes.Find(x => x.Name == current);

        stepsPt1++;

        current = inst[i] == 'L' ? n.L : n.R;
        if (current == "ZZZ")
            break;
    }
}

Console.WriteLine($"Part1: {stepsPt1}");

//----------------------------------------------------------------------------

List<long> cycles = [];
List<Node> startNodes = nodes.FindAll(x => x.Name.EndsWith('A'));;

int stepsPt2 = 0;

// takes too long to iterate until all nodes end in Z
// for every start node, it will fall into a loop of steps that repeat 
// find the number of steps in the cycle for each start node
foreach (Node curr in startNodes)
{
    stepsPt2 = 0;
    int firstZ = 0;
    current = curr.Name;
    while (current.EndsWith('Z') == false)
    {
        for (int i = 0; i < inst.Length; i++)
        {
            stepsPt2++;
            Node? n = nodes.Find(x => x.Name == current);

            current = inst[i] == 'L' ? n.L : n.R;
            if (current.EndsWith('Z'))
            {
                if (firstZ == 0)
                    firstZ = stepsPt2;
                else
                {
                    stepsPt2 -= firstZ;
                    break;
                }
            }
        }
    }
    cycles.Add(stepsPt2);
}

// the least common multiple is the step where all nodes end in Z
long lcm = cycles[0];
foreach (long n in cycles)
    lcm = MathUtil.LCM(lcm, n);

Console.WriteLine($"Part2: {lcm}");

//============================================================================