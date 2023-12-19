using AoCUtils;

Console.WriteLine("Day10: Pipe Maze");

List<List<char>> grid = FileUtil.ReadFileToCharGrid("input.txt");

List<(string curr, string other, int r, int c)> directions =
[
    ("S|JL", "|7F", -1, 0),   // north
    ("S|F7", "|JL", +1, 0),   // south
    ("S-FL", "-7J", 0, +1),   // east
    ("S-7J", "-FL", 0, -1)    // west
];

(int r, int c) start = FindStart(grid);

List<(int r, int c)> loop = [start];
Queue<(int r, int c)> q = [];
q.Enqueue(start);

while (q.Count > 0)
{
    (int r, int c) = q.Dequeue();

    foreach (var d in directions)
    {
        int newRow = r + d.r;
        int newCol = c + d.c;

        if (IsInBounds(grid, newRow, newCol))
        {
            if (d.curr.Contains(grid[r][c]) && d.other.Contains(grid[newRow][newCol]))
            {
                if (loop.Contains((newRow, newCol)) == false)
                {
                    q.Enqueue((newRow, newCol));
                    loop.Add((newRow, newCol));
                }
            }
        }
    }
}

Console.WriteLine($"Part1: {loop.Count / 2}");

//----------------------------------------------------------------------------

// replace S with its actual char
SetCharOfStart(grid, start.r, start.c);

// replace chars not part of loop with '.'
for (int r = 0; r < grid.Count; r++)
{
    for (int c = 0; c < grid[0].Count; c++)
    {
        if (IsInLoop(loop, r, c) == false)
            grid[r][c] = '.';
    }
}

//PrintGrid(grid);

List<(int r, int c)> inside = [];

for (int r = 0; r < grid.Count; r++)
{
    int crossings = 0;

    for (int c = 0; c < grid[0].Count; c++)
    {
        char ch = grid[r][c];

        if (loop.Contains((r, c))) //&& "|F7".Contains(ch))
        {
            if ("|F7".Contains(ch))
                crossings++;
            continue;
        }

        if (crossings % 2 == 1)
            inside.Add((r, c));
    }
}

//PrintGrid(grid);
Console.WriteLine($"Part2: {inside.Count}");

//============================================================================

void PrintGrid(List<List<char>> grid)
{
    for (int r = 0; r < grid.Count; r++)
    {
        string row = new(grid[r].ToArray());
        Console.WriteLine(row);
    }
    Console.WriteLine();
}

bool IsInBounds(List<List<char>> grid, int row, int col)
{
    if (row >= 0 && col >= 0 && row < grid.Count && col < grid[0].Count)
        return true;
    return false;
}

bool IsInLoop(List<(int r, int c)> list, int row, int col)
{
    if (list.Contains((row, col)))
        return true;
    return false;
}

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

void SetCharOfStart(List<List<char>> grid, int row, int col)
{
    string possibles = "|-JL7F";

    if (IsInBounds(grid, row - 1, col)) 
        if ("|7F".Contains(grid[row - 1][col]))
            possibles = possibles.Replace("-", "").Replace("7", "").Replace("F", "");

    if (IsInBounds(grid, row + 1, col))
        if ("|JL".Contains(grid[row + 1][col]))
        possibles = possibles.Replace("-", "").Replace("J", "").Replace("L", "");

    if (IsInBounds(grid, row, col + 1))
        if ("-7J".Contains(grid[row][col + 1]))
        possibles = possibles.Replace("|", "").Replace("7", "").Replace("J", "");

    if (IsInBounds(grid, row, col - 1))
        if ("-FL".Contains(grid[row][col - 1]))
        possibles = possibles.Replace("|", "").Replace("L", "").Replace("F", "");

    grid[row][col] = possibles[0];
}