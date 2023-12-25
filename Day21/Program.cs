using AoCUtils;

Console.WriteLine("Day21: Step Counter");

List<List<char>> grid = FileUtil.ReadFileToCharGrid("input.txt");

(int r, int c) start = FindStart(grid);

List<(int r, int c)> dirs = [(-1, 0), (0, +1), (+1, 0), (0, -1)];
HashSet<(int r, int c)> plots = [];
HashSet<(int r, int c)> visited = [];
Queue<(int r, int c, int s)> q = [];
q.Enqueue((start.r, start.c, 64));

while (q.Count > 0)
{
    (int r, int c, int s) = q.Dequeue();

    if (s % 2 == 0)
        plots.Add((r, c));

    if (s == 0)
        continue;

    foreach ((int dr, int dc) in dirs)
    {
        if (IsInBounds(grid, r + dr, c + dc))
        {
            if (grid[r + dr][c + dc] != '#' && visited.Contains((r + dr, c + dc)) == false)
            {
                q.Enqueue((r + dr, c + dc, s - 1));
                visited.Add((r + dr, c + dc));
            }
        }
    }
}

//foreach (var x in visited)
//    Console.WriteLine(x);
//Console.WriteLine("----");
//foreach (var x in plots)
//    Console.WriteLine(x);

Console.WriteLine($"Part1: {plots.Count}");
Console.WriteLine($"Part2: {0}");

//============================================================================

(int r, int c) FindStart(List<List<char>> grid)
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

    return (r, c);
}

bool IsInBounds(List<List<char>> grid, int row, int col)
{
    if (row >= 0 && col >= 0 && row < grid.Count && col < grid[0].Count)
        return true;
    return false;
}
