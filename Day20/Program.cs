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
//PrintGraph(graph);

Queue<(string name, string output, int pulse)> q = [];
int lowPulses = 0;
int highPulses = 0;

for (int i = 0; i < 1000; i++)
{
    q.Clear();
    
    lowPulses++;
    foreach (string target in graph["broadcaster"].Outputs)
        q.Enqueue(("broadcaster", target, 0));

    while (q.Count > 0)
    {
        (string current, string target, int pulse)= q.Dequeue();

        if (pulse == 0)
            lowPulses++;
        else
            highPulses++;

        if (graph.ContainsKey(target) == false)
            continue;

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
    }
}

int answerPt1 = lowPulses * highPulses;
Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

long answerPt2 = 0;

Console.WriteLine($"Part2: {0}");

//============================================================================

void PrintGraph(Dictionary<string, Node> graph)
{
    foreach (string key in graph.Keys)
        Console.WriteLine(graph[key]);
    Console.WriteLine();
}

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
        if (graph[key].Outputs != null)
            foreach (string s in graph[key].Outputs)
                graph[s].Inputs[key] = 0;
}