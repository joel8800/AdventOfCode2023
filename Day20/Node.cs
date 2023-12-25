using System.ComponentModel.Design;

namespace Day20
{
    internal class Node
    {
        public string Name { get; set; }
        public int State { get; set; }
        public string Type { get; set; }
        public Dictionary<string, int> Inputs { get; set; }
        public List<string> Outputs { get; set; }

        public Node(string name, string outputs)
        {
            Type = name[0].ToString();
            Name = name[1..^0];
            State = 0;
            Inputs = [];
            string[] nodes = outputs.Split(", ");
            Outputs = [.. nodes];
        }

        public void Process(Dictionary<string, Node> graph)
        {
            if (Type == "%")
            {   // flip flop
                if (Inputs.Any(i => i.Value == 0))
                    State = State == 0 ? 1 : 0;
            }
            else
            {   // conjunction
                foreach (string key in Inputs.Keys)
                {
                    if (graph[key].State == 1)
                        Inputs[key] = 1;
                }

                if (Inputs.All(i => i.Value == 1))
                {
                    State = 0;
                }
                else
                {
                    State = 1;
                }
            }
        }
    }
}
