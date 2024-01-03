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
            Outputs = [];
            if (outputs != "")
            {
                string[] nodes = outputs.Split(", ");
                Outputs = [.. nodes];
            }
        }

        public void InvertState()
        {
            State = State == 0 ? 1 : 0;
        }

        public override string ToString()
        {
            string s = $"{Name,-3}:{Type}:state={State}: in[ ";

            foreach (string key in Inputs.Keys)
                s += $"{key,3}:{Inputs[key]} ";

            s += "]: out[ ";
            
            if (Outputs != null)
                foreach (string o in Outputs)
                    s += $"{o} ";
            
            s += "]";
            
            return s;
        }
    }
}
