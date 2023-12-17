using AoCUtils;

Console.WriteLine("Day16: The Floor Will Be Lava");

List<List<char>> input = FileUtil.ReadFileToCharGrid("input.txt");

//PrintGrid(input);
List<(int r, int c)> mirrors = [];
for (int r = 0; r < input.Count; r++)
    for (int c = 0; c < input[0].Count; c++)
        if (input[r][c] != '.')
            mirrors.Add((r, c));

int answerPt1 = CalcEnergizedTiles(input, 0, -1, 0, 1);

Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

List<int> results = [];

// top row going down
for (int c = 0; c < input[0].Count; c++)
    results.Add(CalcEnergizedTiles(input, -1, c, 1, 0));

// bottom row going up
for (int c = 0; c < input[0].Count; c++)
    results.Add(CalcEnergizedTiles(input, input.Count, c, -1, 0));

// left column going right
for (int r = 0; r < input.Count; r++)
    results.Add(CalcEnergizedTiles(input, r, -1, 0, 1));

// right column going left
for (int r = 0; r < input.Count; r++)
    results.Add(CalcEnergizedTiles(input, r, input[0].Count, 0, -1));

int answerPt2 = results.Max();

Console.WriteLine($"Part2: {answerPt2}");

//============================================================================

void AddToQueue(Queue<(int r, int c, int dr, int dc)> q, 
    HashSet<(int r, int c, int dr, int dc)> s, int r, int c, int dr, int dc)
{
    if (s.Contains((r, c, dr, dc)) == false)
    {
        s.Add((r, c, dr, dc));
        q.Enqueue((r, c, dr, dc));
    }
}

int CalcEnergizedTiles(List<List<char>> input, int row, int col, int deltaRow, int deltaCol)
{
    Queue<(int r, int c, int dr, int dc)> q = [];
    q.Enqueue((row, col, deltaRow, deltaCol));

    HashSet<(int r, int c, int dr, int dc)> seen = [];

    while (q.Count > 0)
    {
        (int r, int c, int dr, int dc) = q.Dequeue();

        r += dr;
        c += dc;

        if (r < 0 || r >= input.Count || c < 0 || c >= input[0].Count)
            continue;

        char ch = input[r][c];

        if (ch == '.' || (ch == '-' && dc != 0) || (ch == '|' && dr != 0))
        {
            AddToQueue(q, seen, r, c, dr, dc);
        }
        else if (ch == '/')
        {
            // invert and swap deltas = turn left
            (dr, dc) = (-dc, -dr);

            AddToQueue(q, seen, r, c, dr, dc);
        }
        else if (ch == '\\')
        {
            // swap deltas = turn right
            (dr, dc) = (dc, dr);

            AddToQueue(q, seen, r, c, dr, dc);
        }
        else
        {
            if (dc == 0 && ch == '-')    // dir is vertical and hitting split
            {
                AddToQueue(q, seen, r, c, 0, 1);
                AddToQueue(q, seen, r, c, 0, -1);
                continue;
            }
            if (dr == 0 && ch == '|')   // dir is horizontal and hitting split
            {
                AddToQueue(q, seen, r, c, 1, 0);
                AddToQueue(q, seen, r, c, -1, 0);
                continue;
            }
        }
    }

    HashSet<(int r, int c)> uniques = seen.Select(x => (x.r, x.c)).ToHashSet();

    return uniques.Count;
}

void PrintGrid(List<List<char>> grid)
{
    foreach (List<char> row in grid)
    {
        char[] chars = row.ToArray();
        Console.WriteLine(new string(chars));
    }
    Console.WriteLine();
    Console.WriteLine();
}
