using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day03: Gear Ratios");

List<List<char>> input = FileUtil.ReadFileToCharGrid("input.txt");

int partNumbers = 0;
int gearRatios = 0;
List<(int r, int c)> gears = [];

Regex reNums = new(@"\d+");
Regex reGears = new(@"\*");

for (int row = 0; row < input.Count; row++)
{
    string line = new(input[row].ToArray());

    MatchCollection mcNums = reNums.Matches(line);
    foreach (Match m in mcNums.Cast<Match>())
    {
        int col = m.Index;
        if (IsPartNumber(input, row, col, m.Value.Length))
            partNumbers += int.Parse(m.Value);
    }

    // get locations of stars for part 2
    MatchCollection mcStars = reGears.Matches(line);
    foreach (Match m in mcStars.Cast<Match>())
        gears.Add((row, m.Index));
}

foreach ((int row, int col) in gears)
    gearRatios += GetGearRatio(input, row, col);

Console.WriteLine($"Part1: {partNumbers}");
Console.WriteLine($"Part2: {gearRatios}");
Console.WriteLine("==========================================================================");

//============================================================================

bool IsPartNumber(List<List<char>> grid, int row, int col, int numLen)
{
    List<(int, int)> bounds = [];

    string[] rows = new string[3];

    // get list of bounding box coordinates
    for (int r = row - 1; r <= row + 1; r++)
    {
        for (int c = col - 1; c <= col + numLen; c++)
        {
            if (r == row && c >= col && c < col + numLen)
                continue;

            if (IsInGrid(grid, r, c))
                bounds.Add((r, c));
        }
    }

    // check bounding box for char other than '.'
    foreach ((int r, int c) in bounds)
    {
        if (grid[r][c] != '.')
            return true;
    }

    return false;
}

bool IsInGrid(List<List<char>> grid, int row, int col)
{
    if (row >= 0 && row < grid.Count && col >= 0 && col < grid[0].Count)
        return true;
    else
        return false;
}

// checks that all locations are in the grid
char[] GetLocalRow(List<List<char>> grid, int row, int col)
{
    char[] local = new char[7];

    for (int i = 0, j = col - 3; i < 7; i++)
    {
        if (IsInGrid(grid, row, j))
            local[i] = grid[row][j];
        j++;
    }

    return local;
}

// check if chars left and right of center are digits
// if so, check if the next char outwards is also a digit
//   if not, change next outer char to '.'
void IsolateNum(char[] row)
{
    // left side
    if (char.IsDigit(row[2]))
    {
        if (char.IsDigit(row[1]) == false)
            row[0] = '.';
    }
    else
    {
        row[0] = '.';
        row[1] = '.';
    }

    // right side
    if (char.IsDigit(row[4]))
    {
        if (char.IsDigit(row[5]) == false)
            row[6] = '.';
    }
    else
    {
        row[5] = '.';
        row[6] = '.';
    }
}

int GetGearRatio(List<List<char>> grid, int row, int col)
{
    int gearRatio = 0;

    Regex reNums = new(@"\d+");

    char[] row0 = GetLocalRow(grid, row - 1, col);      // row above
    char[] row1 = GetLocalRow(grid, row, col);          // row with *
    char[] row2 = GetLocalRow(grid, row + 1, col);      // row below

    //Console.WriteLine(new string(row0));
    //Console.WriteLine(new string(row1));
    //Console.WriteLine(new string(row2));

    // check row above
    IsolateNum(row0);
    IsolateNum(row1);
    IsolateNum(row2);

    string block = new(row0);
    block += "|" + new string(row1);
    block += "|" + new string(row2);

    //Console.WriteLine($"[{row,3},{col,3}]: {block}");

    MatchCollection mc = reNums.Matches(block);
    if (mc.Count == 2)
        gearRatio = int.Parse(mc[0].Value) * int.Parse(mc[1].Value);

    return gearRatio;
}
