using AoCUtils;
using Day20;

Console.WriteLine("Day20: Pulse Propagation");

string[] input = FileUtil.ReadFileByLine("input.txt");

Dictionary <string, Node> graph = [];

foreach (string line in input)
{
    string name = line.Split(" -> ")[0];
    string outputs = line.Split(" -> ")[1];

    if (name == "broadcaster")
        name = " " + name;

    Node newNode = new(name, outputs);
    graph[newNode.Name] = newNode;
}
ConnectInputs(graph);

int answerPt1 = GetPresses(graph, false);

Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

// I used Graphviz to visualize my input graph.  See graph.gv.
// My graph is composed of four 12-bit binary counters with different feedback
// bits.  The four counters cycle at different counts and feed into a conjunctor
// that feeds rx. The rx node pulses when all four counters pulse at the same
// time.  The answer for Part 2 is the least common multiple of the four counts.

int counter0 = GetPresses(graph, true, "dj");         // dj -> dc -> ns -> rx
int counter1 = GetPresses(graph, true, "rr");         // rr -> rv -> ns -> rx
int counter2 = GetPresses(graph, true, "pb");         // pb -> vp -> ns -> rx
int counter3 = GetPresses(graph, true, "nl");         // nl -> cq -> ns -> rx

List<long> nums = [counter0, counter1, counter2, counter3];

long answerPt2 = MathUtil.LCM(nums);

Console.WriteLine($"Part2: {answerPt2}");

//============================================================================

void ConnectInputs(Dictionary<string, Node> graph)
{
    List<string> newKeys = [];
    foreach (string key in graph.Keys)
        foreach (string s in graph[key].Outputs)
            if (graph.ContainsKey(s) == false)
                newKeys.Add(s);

    foreach (string key in newKeys)
        graph[key] = new(" " + key, "");

    foreach (string key in graph.Keys)  
        foreach (string s in graph[key].Outputs)
            graph[s].Inputs[key] = 0;
}

int GetPresses(Dictionary<string, Node> graph, bool isPart2, string nodeName = "rx")
{
    Queue<(string name, string output, int pulse)> q = [];
    
    // for part1
    int lowPulses = 0;
    int highPulses = 0;
    
    // for part2
    int lowPulse1 = 0;
    int lowPulse2 = 0;
    int localState = 1;
    
    int presses = isPart2 ? 8192 : 1000;

    for (int i = 0; i < presses; i++)
    {
        q.Clear();

        lowPulses++;
        foreach (string target in graph["broadcaster"].Outputs)
            q.Enqueue(("broadcaster", target, 0));

        while (q.Count > 0)
        {
            (string current, string target, int pulse) = q.Dequeue();

            if (pulse == 0)
                lowPulses++;
            else
                highPulses++;

            Node destNode = graph[target];

            if (destNode.Type == "%")
            {
                if (pulse == 0)
                {
                    destNode.InvertState();
                    foreach (string key in destNode.Outputs)
                        q.Enqueue((target, key, destNode.State));
                }
            }
            else
            {
                destNode.Inputs[current] = pulse;
                if (destNode.Inputs[current] == 1 && destNode.Inputs.Values.Distinct().Count() == 1)
                    destNode.State = 0;
                else
                    destNode.State = 1;

                foreach (string key in destNode.Outputs)
                    q.Enqueue((target, key, destNode.State));
            }

            if (isPart2 && destNode.Name == nodeName && destNode.State != localState)
            {
                // save first time node pulses low
                if (localState == 0 && lowPulse1 == 0)
                    lowPulse1 = i;

                // save second time node pulses low
                if (localState == 0 && lowPulse2 == 0 && i > lowPulse1)
                    lowPulse2 = i;

                localState = destNode.State;
            }
        }
    }

    if (isPart2)
        return lowPulse2 - lowPulse1;
    else
        return lowPulses * highPulses;
}
