using AoCUtils;

Console.WriteLine("Day14: Parabolic Reflector Dish");

List<List<char>> grid = FileUtil.ReadFileToCharGrid("input.txt");

TiltNorth(grid);
int answerPt1 = CalcTotalLoad(grid);

Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

List<int> hashcodes = [];
Dictionary<int, int> lookupLoads = [];

int index = 0;

while (hashcodes.Contains(HashCode(grid)) == false)
{        
    hashcodes.Add(HashCode(grid));

    for (int i = 0; i < 4; i++)
    {
        TiltNorth(grid);
        grid = Rotate(grid);
    }
    
    lookupLoads[HashCode(grid)] = CalcTotalLoad(grid);
    index++;
}

int offset = hashcodes.IndexOf(HashCode(grid));     // start of cycle
int cycleLength = index - offset;

//Console.WriteLine($"{index} - {offset} = {cycleLength}");
int answerPt2 = lookupLoads[hashcodes[(1000000000 - offset) % cycleLength + offset]];

Console.WriteLine($"Part2: {answerPt2}");

//=============================================================================

int HashCode(List<List<char>> grid)
{
    int hash = 0;
    foreach (List<char> row in grid)
    {
        foreach (char c in row)
        {
            if (c == '.')
                hash *= 3;
            else if (c == 'O')
                hash = hash * 3 + 1;
            else
                hash = hash * 3 + 2;
        }
    }
    return hash;
}

// rotate grid clockwise 90 degrees
List<List<char>> Rotate(List<List<char>> grid)
{
    List<List<char>> newGrid = [];

    for (int r = 0; r < grid.Count; r++)
    {
        List<char> newRow = [];

        for (int c = grid.Count - 1; c >= 0; c--)
            newRow.Add(grid[c][r] );

        newGrid.Add(newRow);
    }

    return newGrid;
}

void PrintGrid(List<List<char>> grid)
{
    foreach (List<char> row in grid)
    {
        char[] chars = [.. row];
        Console.WriteLine(new string(chars));
    }
    Console.WriteLine();
    Console.WriteLine();
}

void TiltNorth(List<List<char>> grid)
{
    bool rockMoved = true;

    while (rockMoved)
    {
        rockMoved = false;
        for (int r = 0; r < grid.Count - 1; r++)
        {
            for (int c = 0; c < grid[0].Count; c++)
            {
                if (grid[r][c] == '.' && grid[r + 1][c] == 'O')
                {
                    grid[r][c] = 'O';
                    grid[r + 1][c] = '.';
                    rockMoved = true;
                }
            }
        }
    }
}

int CalcTotalLoad(List<List<char>> grid)
{
    int totalLoad = 0;

    for (int r = 0; r < grid.Count; r++)
    {
        int count = grid[r].Where(c => c == 'O').Count();
        totalLoad += count * (grid.Count - r);
    }

    return totalLoad;
}