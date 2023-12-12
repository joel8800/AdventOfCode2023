using AoCUtils;

Console.WriteLine("Day11: Cosmic Expansion");

List<List<char>> input = FileUtil.ReadFileToCharGrid("input.txt");

List<(int r, int c)> galaxies = [];
List<(int r, int c)> galsPt1 = [];
List<(int r, int c)> galsPt2 = [];
List<int> emptyRows = [];
List<int> emptyCols = [];
List<int> distancesPt1 = [];
List<int> distancesPt2 = [];

// find empty rows
for (int i = 0; i < input.Count; i++)
    if (input[i].Contains('#') == false)
        emptyRows.Add(i);

// find empty columns
for (int col = 0; col < input[0].Count; col++)
{
    List<char> colData = [];
    for (int row = 0; row < input.Count; row++)
        colData.Add(input[row][col]);
    if (colData.Contains('#') == false)
        emptyCols.Add(col);
}

// find galaxies
for (int row = 0; row < input.Count; row++)
    for (int col = 0; col < input[0].Count; col++)
        if (input[row][col] == '#')
            galaxies.Add((row, col));

int expFactor = 1; // 2 - 1

// adjust galaxy locations for expansion
for (int i = 0; i < galaxies.Count; i++)
    galsPt1.Add(CalcNewPosition(galaxies[i], emptyRows, emptyCols, expFactor));

// find distances
for (int i = 0; i < galsPt1.Count - 1; i++)
    for (int j = i + 1; j < galsPt1.Count; j++)
        distancesPt1.Add(Manhattan(galsPt1[i], galsPt1[j]));

Console.WriteLine($"Part1: {distancesPt1.Sum()}");

//----------------------------------------------------------------------------

expFactor = 1000000 - 1;

// adjust galaxy locations for expansion
for (int i = 0; i < galaxies.Count; i++)
    galsPt2.Add(CalcNewPosition(galaxies[i], emptyRows, emptyCols, expFactor));

// find distances
for (int i = 0; i < galsPt2.Count - 1; i++)
    for (int j = i + 1; j < galsPt2.Count; j++)
        distancesPt2.Add(Manhattan(galsPt2[i], galsPt2[j]));

Console.WriteLine($"Part2: {distancesPt2.Select(d => Convert.ToInt64(d)).Sum()}");

//============================================================================

int Manhattan((int r, int c) point0, (int r, int c) point1)
{
    return Math.Abs(point0.r - point1.r) + Math.Abs(point0.c - point1.c);
}

int CalcPositionInRowOrCol(List<int> list, int val, int expFactor)
{
    for (int i = 0; i < list.Count; i++)
    {
        if (val < list[i])
            return (i * expFactor) + val;
    }

    return list.Count * expFactor + val;
}

(int r, int c) CalcNewPosition((int r, int c) gal, List<int> emptyRows, List<int> emptyCols, int expFactor)
{
    int newRow = CalcPositionInRowOrCol(emptyRows, gal.r, expFactor);
    int newCol = CalcPositionInRowOrCol(emptyCols, gal.c, expFactor);

    return (newRow, newCol);
}