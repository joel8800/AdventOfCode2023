using AoCUtils;

Console.WriteLine("Day23: A Long Walk");

List<List<char>> grid = FileUtil.ReadFileToCharGrid("input.txt");

(int r, int c) start = (0, grid[0].IndexOf('.'));
(int r, int c) finish = (grid.Count - 1, grid[^1].IndexOf('.'));

// find decision points
List<(int r, int c)> vertices = [start, finish];
vertices = GetVertices(grid, vertices);

// create weighted graph of only the decision points
Dictionary<(int r, int c), Dictionary<(int r, int c), int>> graph = [];
graph = CreateGraph(vertices, 1);

HashSet<(int r, int c)> seen = [];
int answerPt1 = DFS(start, seen);
Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

// ignore slopes in part 2
graph = CreateGraph(vertices, 2);

seen.Clear();
int answerPt2 = DFS(start, seen);
Console.WriteLine($"Part2: {answerPt2}");

//============================================================================

int DFS((int r, int c) curr, HashSet<(int r, int c)> seen)
{
    if (curr == finish)
        return 0;

    int max = 0;

    seen.Add(curr);
    foreach (var nx in graph[curr])
    {
        if (seen.Contains(nx.Key) == false)
            max = Math.Max(max, DFS(nx.Key, seen) + graph[curr][nx.Key]);
    }
    seen.Remove(curr);

    return max;
}

bool IsInbounds(List<List<char>> grid, int row, int col)
{
    if (row >= 0 && row < grid.Count && col >= 0 && col <grid[0].Count)
        return true;
    return false;
}

// find decision points, nodes with more than two neighbors
List<(int r, int c)> GetVertices(List<List<char>> grid, List<(int r, int c)> nodes)
{
    List<(int r, int c)> dirs = [(-1, 0), (0, 1), (1, 0), (0, -1)];

    for (int r = 0; r < grid.Count; r++)
    {
        for (int c = 0; c < grid[0].Count; c++)
        {
            if (grid[r][c] == '#')
                continue;

            int neighbors = 0;
            foreach (var d in dirs)
                if (IsInbounds(grid, r + d.r, c + d.c) && grid[r + d.r][c + d.c] != '#')
                    neighbors++;

            if (neighbors > 2)
                nodes.Add((r, c));
        }
    }

    return nodes;
}

Dictionary<(int r, int c), Dictionary<(int r, int c), int>> CreateGraph(List<(int r, int c)> vertices, int part)
{
    Dictionary<(int r, int c), Dictionary<(int r, int c), int>> graph = [];
    foreach (var v in vertices)
        graph[v] = [];

    Dictionary<char, List<(int r, int c)>> dirs = new()
    {
        ['.'] = [(-1, 0), (1, 0), (0, -1), (0, 1)],
        ['^'] = [(-1, 0)], ['>'] = [(0, 1)],
        ['v'] = [(1, 0)],  ['<'] = [(0, -1)],
    };
    
    // compress paths to weighed edges
    foreach (var v in vertices)
    {
        Stack<(int r, int c, int w)> st = [];
        st.Push((v.r, v.c, 0));

        HashSet<(int r, int c)> seen = [];
        seen.Add(v);

        while (st.Count > 0)
        {
            (int r, int c, int w) curr = st.Pop();

            if (curr.w != 0 && vertices.Contains((curr.r, curr.c)))
            {
                graph[(v.r, v.c)][(curr.r, curr.c)] = curr.w;
                continue;
            }

            // ignore slopes in part 2
            char ch = part == 2 ? '.' : grid[curr.r][curr.c];
            
            foreach ((int dr, int dc) in dirs[ch])
            {
                int newR = curr.r + dr;
                int newC = curr.c + dc;

                if (IsInbounds(grid, newR, newC) && grid[newR][newC] != '#' && seen.Contains((newR, newC)) == false)
                {
                    st.Push((newR, newC, curr.w + 1));
                    seen.Add((newR, newC));
                }
            }
        }
    }

    return graph;
}
