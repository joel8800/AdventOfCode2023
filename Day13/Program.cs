using AoCUtils;
using System.Text;

Console.WriteLine("Day13: Point of Incidence");

string[] blocks = FileUtil.ReadFileByBlock("input.txt");

int sumPt1 = 0;
List<(int h, int v)> results = [];

foreach (string block in blocks)
{
    List<string> grid = [.. block.Split(Environment.NewLine)];

    (int h, int v) = FindReflectionLine(grid, (-1, -1));

    results.Add((h, v));    // save results for part2

    if (h != -1)
        sumPt1 += h * 100;

    if (v != -1)
        sumPt1 += v;
}

Console.WriteLine($"Part1: {sumPt1}");

//----------------------------------------------------------------------------

int sumPt2 = 0;
int blockNum = 0;
foreach (string block in blocks)
{
    int h = -1;
    int v = -1;

    List<string> grid = [.. block.Split(Environment.NewLine)];

    for (int cr = 0; cr < grid.Count; cr++)
    {
        for (int cc = 0; cc < grid[0].Length; cc++)
        {
            // flip location
            grid[cr] = FlipLocation(grid[cr], cc);

            (h, v) = FindReflectionLine(grid, results[blockNum]);

            // flip back
            grid[cr] = FlipLocation(grid[cr], cc);

            if ((h, v) != (-1, -1) && (h, v) != results[blockNum])
            {
                cr = 1000;  // break out of outer loop as well
                break;
            }
        }
    }

    if (h != -1)
        sumPt2 += h * 100;
    if (v != -1)
        sumPt2 += v;

    blockNum++;
}

Console.WriteLine($"Part2: {sumPt2}");

//============================================================================

// check horizontal then transpose grid and repeat to check vertical
(int h, int v) FindReflectionLine(List<string> grid, (int h, int v) pt1Results)
{
    int h = CheckHorizontal(grid, pt1Results.h);
    
    List<string> transposed = Transpose(grid);
    int v = CheckHorizontal(transposed, pt1Results.v);

    return (h, v);
}

// returns fold location or -1 if it doesn't find one
int CheckHorizontal(List<string> grid, int pt1Result)
{
    int fold = 0;
    string above = "";
    string below = "";

    for (int i = 0; i < grid.Count - 1; i++)
    {
        int offset = 0;

        above = "";
        below = "";

        while (offset <= i && offset + i < grid.Count - 1)
        {
            above += grid[i - offset];
            below += grid[i + offset + 1];
            offset++;
        }

        fold++;
        if (above == below)
        {
            if (fold != pt1Result)
                return fold;
        }
    }

    return -1;
}

List<string> Transpose(List<string> grid)
{
    List<string> newGrid = [];

    StringBuilder sb = new();

    for (int c = 0; c < grid[0].Count(); c++)
    {
        sb.Clear();
        for (int r = 0; r < grid.Count; r++)
        {
            sb.Append(grid[r][c]);
        }
        newGrid.Add(sb.ToString());
    }

    return newGrid;
}

string FlipLocation(string row, int index)
{
    char[] chars = row.ToCharArray();
    chars[index] = chars[index] == '#' ? '.' : '#';
    return new(chars);
}
