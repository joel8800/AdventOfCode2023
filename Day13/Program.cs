using AoCUtils;
using System.Text;

Console.WriteLine("Day13: Point of Incidence");

string[] blocks = FileUtil.ReadFileByBlock("input.txt");

int sum = 0;
List<int> blockRefs = [];
List<(int h, int v)> results = [];

foreach (string block in blocks)
{
    List<string> grid = [.. block.Split(Environment.NewLine)];

    for (int r = 0; r < grid.Count; r++)
    {
        for (int c = 0; c < grid[0].Length; c++)
            Console.Write(grid[r][c]);
        Console.WriteLine();
    }

    int h = CheckHorizontalReflection(grid, (-1, -1));
    int v = CheckVerticalReflection(grid, (-1, -1));

    results.Add((h, v));
    Console.WriteLine($"[{h,3},{v,3}]");
    Console.WriteLine();

    if (h != -1)
    {
        blockRefs.Add(h * 100);
        sum += h * 100;
    }
    if (v != -1)
    {
        blockRefs.Add(v);
        sum += v;
    }
}

int i = 0;
foreach ((int h, int v) in results)
    Console.WriteLine($"{i++}: h={h,3}, v={v,3}");

Console.WriteLine($"Part1: {sum}");

//----------------------------------------------------------------------------

sum = 0;
int blockNum = 0;
foreach (string block in blocks)
{
    int h = -1;
    int v = -1;
    bool newScore = false;

    List<string> grid = [.. block.Split(Environment.NewLine)];

    for (int cr = 0; cr < grid.Count; cr++)
    {
        for (int cc = 0; cc < grid[0].Length; cc++)
        {
            Console.Write($"[{blockNum,3}][{cr,2},{cc,2}]  ");
            // flip location
            grid[cr] = FlipLocation(grid[cr], cc);

            //for (int r = 0; r < grid.Count; r++)
            //{
            //    for (int c = 0; c < grid[0].Length; c++)
            //        Console.Write(grid[r][c]);
            //    Console.WriteLine();
            //}

            h = CheckHorizontalReflection(grid, results[blockNum]);
            v = CheckVerticalReflection(grid, results[blockNum]);

            // flip back
            grid[cr] = FlipLocation(grid[cr], cc);

            Console.WriteLine($"og:[{results[blockNum].h,3},{results[blockNum].v,3}]  new:[{h,3},{v,3}]");
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
        sum += h * 100;
    if (v != -1)
        sum += v;
    Console.WriteLine($"[{blockNum}] sum = {sum} (h={h,3} v={v,3})");
    Console.WriteLine();

    blockNum++;
}

Console.WriteLine($"Part2: {sum}");

//============================================================================

int CheckHorizontalReflection(List<string> grid, (int h, int v) pt1Score)
{
    int fold = 0;

    //if (grid[0] == grid[1])
    //{
    //    Console.WriteLine("rows 0 & 1 match");
    //    return 0;
    //}

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
            //Console.WriteLine($"a:{i-offset,3}  above:{above}");
            //Console.WriteLine($"b:{i+offset+1,3}  below:{below}");
            //Console.WriteLine("----");
            offset++;
        }

        fold++;
        if (above == below)
        {
            if (fold == pt1Score.h || fold == pt1Score.v)
                continue;
            else
                break;
        }
    }

    if (above == below)
        return fold;
    else
        return -1;
}

int CheckVerticalReflection(List<string> grid, (int h, int v) pt1Score)
{
    List<string> transposed = Transpose(grid);

    return CheckHorizontalReflection(transposed, pt1Score);
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