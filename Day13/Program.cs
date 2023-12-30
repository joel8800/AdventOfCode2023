using AoCUtils;
using System.Text;

Console.WriteLine("Day13: Point of Incidence");

string[] blocks = FileUtil.ReadFileByBlock("input.txt");

int sumPt1 = 0;
List<(int h, int v)> results = [];

foreach (string block in blocks)
{
    List<string> grid = [.. block.Split(Environment.NewLine)];
    
    int h = CheckHorizontalReflection(grid, (-1, -1), false);
    int v = CheckVerticalReflection(grid, (-1, -1));

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

            h = CheckHorizontalReflection(grid, results[blockNum], false);
            v = CheckVerticalReflection(grid, results[blockNum]);

            // flip back
            grid[cr] = FlipLocation(grid[cr], cc);

           if ((h, v) != (-1, -1) && (h, v) != results[blockNum])
                break;
        }
        if ((h, v) != (-1, -1) && (h, v) != results[blockNum])
        {
            if (h == results[blockNum].h)
                h = -1;
            if (v == results[blockNum].v)
                v = -1;
            break;
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

void PrintGrid(List<string> grid)
{
    for (int r = 0; r < grid.Count; r++)
    {
        for (int c = 0; c < grid[0].Length; c++)
            Console.Write(grid[r][c]);
        Console.WriteLine();
    }
}

int CheckHorizontalReflection(List<string> grid, (int h, int v) pt1Score, bool isVert)
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
            if (isVert == false && fold == pt1Score.h)
                continue;
            if (isVert == true && fold == pt1Score.v)
                continue;

            return fold;
        }
    }

    return -1;
}

int CheckVerticalReflection(List<string> grid, (int h, int v) pt1Score)
{
    List<string> transposed = Transpose(grid);

    return CheckHorizontalReflection(transposed, pt1Score, true);
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

bool IsNewResult((int h, int v) pt1, (int h, int v) pt2)
{
    if (pt1.h != pt2.h)
    {
        pt2.v = -1;
        return true;
    }

    if (pt1.v != pt2.v)
    {
        pt2.h = -1;
        return true;
    }
    return false;
}