namespace Day08
{
    internal class Node
    {
        public string Name { get; set; }
        public string L {  get; set; }
        public string R { get; set; }
    
        public Node(string name, string left, string right)
        {
            Name = name;
            L = left;
            R = right;
        }

        public override string ToString()
        {
            return $"{Name}:({L},{R})";
        }
    }
}
