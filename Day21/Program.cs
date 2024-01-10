using AoCUtils;

Console.WriteLine("Day21: Step Counter");

List<List<char>> grid = FileUtil.ReadFileToCharGrid("input.txt");

(int r, int c) start = FindStart(grid);

int maxSteps = 64;
int answerPt1 = WalkGrid(grid, start.r, start.c, maxSteps);

Console.WriteLine($"Part1: {answerPt1}"); // 42, 3666

//----------------------------------------------------------------------------

// part2 solution from Hyperneutrino.
// Calculates all the complete grids (odd and even)
// Calculates all the corner points
// Calculates the incomplete edges, large and small on all for sides

maxSteps = 26501365;
int size = grid.Count;

// max number of grids in any direction
int maxGrids = maxSteps / size - 1;

// two types of complete grids, odd and even
long oddGrids = 1L * ((maxGrids / 2 * 2) + 1) * ((maxGrids / 2 * 2) + 1);
long evenGrids = 1L * ((maxGrids + 1) / 2 * 2) * ((maxGrids + 1) / 2 * 2);

int oddPtsPerGrid = WalkGrid(grid, start.r, start.c, size * 2 + 1);
int evenPtsPerGrid = WalkGrid(grid, start.r, start.c, size * 2);

// fills out to a diamond shape, these are the corners
int cornerN = WalkGrid(grid, size - 1, start.c, size - 1);
int cornerE = WalkGrid(grid, start.r, 0, size - 1);
int cornerS = WalkGrid(grid, 0, start.c, size - 1);
int cornerW = WalkGrid(grid, start.r, size - 1, size - 1);

// each side of the diamond has two grid types,
// one with smaller fill and one with larger fill
int smallNE = WalkGrid(grid, size - 1, 0, size / 2 - 1);
int smallNW = WalkGrid(grid, size - 1, size - 1, size / 2 - 1);
int smallSE = WalkGrid(grid, 0, 0, size / 2 - 1);
int smallSW = WalkGrid(grid, 0, size - 1, size / 2 - 1);

int largeNE = WalkGrid(grid, size - 1, 0, size * 3 / 2 - 1);
int largeNW = WalkGrid(grid, size - 1, size - 1, size * 3 / 2 - 1);
int largeSE = WalkGrid(grid, 0, 0, size * 3 / 2 - 1);
int largeSW = WalkGrid(grid, 0, size - 1, size * 3 / 2 - 1);

// sum all together
long allOdd = 1L * oddGrids * oddPtsPerGrid;
long allEven = 1L * evenGrids * evenPtsPerGrid;
long allSmall = 1L * (maxGrids + 1) * (smallNE + smallNW + smallSE + smallSW);
long allLarge = 1L * maxGrids * (largeNE + largeNW + largeSE + largeSW);
long corners = cornerN + cornerE + cornerS + cornerW;
long answerPt2 = allOdd + allEven + allSmall + allLarge + corners;

Console.WriteLine($"Part2: {answerPt2}");   // 609298746763952

//============================================================================

int WalkGrid(List<List<char>> grid, int startRow, int startCol, int maxSteps)
{
    List<(int r, int c)> dirs = [(-1, 0), (0, +1), (+1, 0), (0, -1)];
    (int r, int c) start = (startRow, startCol);

    HashSet<(int r, int c)> plots = [];
    HashSet<(int r, int c)> visited = [];
    Queue<(int r, int c, int s)> q = [];

    q.Enqueue((start.r, start.c, maxSteps));

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

    //foreach ((int r, int c) in plots)
    //    grid[r][c] = 'O';
    //PrintGrid(grid);

    return plots.Count;
}

void PrintGrid(List<List<char>> grid)
{
    foreach (List<char> row in grid)
    {
        string r = new(row.ToArray());
        Console.WriteLine(r);
    }
    Console.WriteLine();
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

bool IsInBounds(List<List<char>> grid, int row, int col)
{
    if (row >= 0 && col >= 0 && row < grid.Count && col < grid[0].Count)
        return true;
    return false;
}
