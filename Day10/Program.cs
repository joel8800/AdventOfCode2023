using AoCUtils;
using Day10;

Console.WriteLine("Day10: Pipe Maze");

List<List<char>> input = FileUtil.ReadFileToCharGrid("input.txt");

List<(string me, string other,int r, int c)> dirs = [];
dirs.Add(("S|JL", "|7F", -1, 0));   // north
dirs.Add(("S|F7", "|JL", +1, 0));   // south
dirs.Add(("S-FL", "-7J", 0, +1));   // east
dirs.Add(("S-7J", "-7J", 0, -1));   // west

Node start = FindStart(input);
List<Node> visited = [start];
Queue<Node> q = [];
q.Enqueue(start);

while (q.Count > 0)
{
    Node n = q.Dequeue();

    //for (int i = 0; i < 3; i++)
    //{
    //    if (dirs[i].me.Contains(n.Ch) && dirs[i].other.Contains(input[n.Row + dirs[i].r][n.Col + dirs[i].c]))
    //    {
    //        Node newNode = new(input[n.Row + dirs[i].r][n.Col + dirs[i].c], n.Row + dirs[i].r, n.Col + dirs[i].c);
    //        if (IsInList(visited, newNode) == false)
    //        {
    //            q.Enqueue(newNode);
    //            visited.Add(newNode);
    //        }
    //    }
    //}

    // check north
    if ("S|JL".Contains(n.Ch) && "|7F".Contains(input[n.Row - 1][n.Col]))
    {
        Node newNode = new(input[n.Row - 1][n.Col], n.Row - 1, n.Col);
        if (IsInList(visited, newNode) == false)
        {
            q.Enqueue(newNode);
            visited.Add(newNode);
        }
    }

    // check south
    if ("S|F7".Contains(n.Ch) && "|JL".Contains(input[n.Row + 1][n.Col]))
    {
        Node newNode = new(input[n.Row + 1][n.Col], n.Row + 1, n.Col);
        if (IsInList(visited, newNode) == false)
        {
            q.Enqueue(newNode);
            visited.Add(newNode);
        }
    }

    // check east
    if ("S-FL".Contains(n.Ch) && "-7J".Contains(input[n.Row][n.Col + 1]))
    {
        Node newNode = new(input[n.Row][n.Col + 1], n.Row, n.Col + 1);
        if (IsInList(visited, newNode) == false)
        {
            q.Enqueue(newNode);
            visited.Add(newNode);
        }
    }

    // check west
    if ("S-7J".Contains(n.Ch) && "-FL".Contains(input[n.Row][n.Col - 1]))
    {
        Node newNode = new(input[n.Row][n.Col - 1], n.Row, n.Col - 1);
        if (IsInList(visited, newNode) == false)
        {
            q.Enqueue(newNode);
            visited.Add(newNode);
        }
    }
}

Console.WriteLine($"Part1: {visited.Count / 2}"); // 6815

//----------------------------------------------------------------------------

// replace S with its actual char
char st = GetCharOfStart(input, start);
input[start.Row][start.Col] = st;

List <(int r, int c)> outside = [];

for (int r = 0; r < input.Count; r++)
{
    for (int c = 0; c < input[0].Count; c++)
    {
        char ch = input[r][c];

        
    }
}







Console.WriteLine($"grid has {input.Count * input[0].Count} locations");
Console.WriteLine($"loop has {visited.Count} locations");
Console.WriteLine($"outsides {outside.Count} locations");
Console.WriteLine($"Part2: {(input.Count * input[0].Count - visited.Count - outside.Count)}");   // 269

//============================================================================

bool InInside(List<List<char>> grid, List<Node> visited, int row, int col)
{
    List<Node> inLoop = visited.Where(n => n.Row == row).ToList();


    if (row >= 0 && row < grid.Count && col >= 0 && col < grid[row].Count)
        return true;
    else
        return false;
}

bool IsInList(List<Node> list, Node n)
{
    foreach (Node node in list)
    {
        if (node.Row == n.Row && node.Col == n.Col)
            return true;
    }
    return false;
}

Node FindStart(List<List<char>> grid)
{
    int r = 0;
    int c = 0;

    for (int row = 0; row < grid.Count; row++)
    {
        if (grid[row].Contains('S'))
        {
            int col = grid[row].FindIndex(c => c == 'S');
            r = row; c = col;
            break;
        }
    }

    return new Node(grid[r][c], r, c);
}

char GetCharOfStart(List<List<char>> grid, Node st)
{
    string possibles = "|-JL7F";
    
    if ("|7F".Contains(grid[st.Row - 1][st.Col]))
        possibles = possibles.Replace("-", "").Replace("7", "").Replace("F", "");

    if ("|JL".Contains(grid[st.Row + 1][st.Col]))
        possibles = possibles.Replace("-", "").Replace("J", "").Replace("L", "");

    if ("-7J".Contains(grid[st.Row][st.Col + 1]))
        possibles = possibles.Replace("|", "").Replace("7", "").Replace("J", "");

    if ("-FL".Contains(grid[st.Row][st.Col - 1]))
        possibles = possibles.Replace("|", "").Replace("L", "").Replace("F", "");

    Console.WriteLine($"S:{possibles}");
    return possibles[0];
}