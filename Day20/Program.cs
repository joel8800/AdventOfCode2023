using AoCUtils;
using Day20;

Console.WriteLine("Day20: Pulse Propagation");

string[] input = FileUtil.ReadFileByLine("inputSample.txt");

Node bCast;
Dictionary <string, Node> graph = [];

foreach (string line in input)
{
    string[] parts = line.Split(" -> ");

    if (parts[0] == "broadcaster")
    {
        bCast = new(" broadcaster", parts[1]);
        graph[bCast.Name] = bCast;
    }
    else
    {
        Node n = new(parts[0], parts[1]);
        graph[n.Name] = n;
    }
}
ConnectInputs(graph);

Queue<Node> q = [];
q.Enqueue(bCast);

while (q.Count > 0)
{
    Node n = q.Dequeue();

    n.Process();


}


Console.WriteLine($"Part1: {0}");
Console.WriteLine($"Part2: {0}");

//============================================================================

void ConnectInputs(Dictionary<string, Node> graph)
{
    foreach (string key in graph.Keys)  
    {
        if (key == "broadcaster")
            continue;

        foreach (string s in graph[key].Outputs)
        {
            graph[s].Inputs[key] = 0;
        }
    }
}