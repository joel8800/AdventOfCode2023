using AoCUtils;

Console.WriteLine("Day22: Sand Slabs");

string[] input = FileUtil.ReadFileByLine("input.txt");
List<List<int>> bricks = [];

foreach (string line in input)
{
    List<int> brick = line.Replace("~", ",").Split(',').Select(x => int.Parse(x)).ToList();
    bricks.Add(brick);
}

// sort by z height and drop
bricks = [.. bricks.OrderBy(x => x[2])];
(bricks, _) = DropBricks(bricks);

int answerPt1 = SafeToDisintegrate(bricks);
Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

int answerPt2 = Disintegrate(bricks);// 0;
Console.WriteLine($"Part2: {answerPt2}");

//============================================================================

// checks each brick and drops it if possible
(List<List<int>>, int) DropBricks (List<List<int>> bricks)
{
    int dropped = 0;
    List<List<int>> tower = [];
    List<List<int>> towerXY = [];       // height of each grid location

    for (int y = 0; y < 10; y++)
    {
        List<int> x = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        towerXY.Add(x);
    }

    foreach (List<int> b in bricks)
    {
        int saveLevel = b[2];
        int brickHeight = b[5] - b[2];
        int towerHeight = FindHighestPoint(towerXY, b);

        b[2] = towerHeight + 1;
        b[5] = b[2] + brickHeight;

        for (int x = b[0]; x <= b[3]; x++)
            for (int y = b[1]; y <= b[4]; y++)
                towerXY[x][y] = b[5];

        if (b[2] != saveLevel)
            dropped++;

        tower.Add(b);
    }

    return (tower, dropped);
}

int FindHighestPoint(List<List<int>> towerXY, List<int> brick)
{
    int maxHeight = 0;

    for (int x = brick[0]; x <= brick[3]; x++)
        for (int y = brick[1]; y <= brick[4]; y++)
            maxHeight = Math.Max(maxHeight, towerXY[x][y]);

    return maxHeight;
}

bool IsTheSameBrick(List<int> a, List<int> b)
{
    if (a[0] == b[0] && a[1] == b[1] && a[2] == b[2] && a[3] == b[3] && a[4] == b[4])
        return true;
    else
        return false;
}

int SafeToDisintegrate(List<List<int>> bricks)
{
    int count = 0;

    foreach (List<int> brick in bricks)
    {
        List<List<int>> tmpTower = bricks.ConvertAll(b => new List<int>(b));

        for (int i = 0; i < tmpTower.Count; i++)
        {
            if (IsTheSameBrick(brick, tmpTower[i]))
                tmpTower.RemoveAt(i);
        }

    (_, int dropped) = DropBricks(tmpTower);

    if (dropped == 0)
        count++;
    }

    return count;
}

int Disintegrate(List<List<int>> bricks)
{
    int total = 0;

    foreach (List<int> brick in bricks)
    {
        List<List<int>> tmpTower = bricks.ConvertAll(b => new List<int>(b));

        for (int i = 0; i < tmpTower.Count; i++)
        {
            if (IsTheSameBrick(brick, tmpTower[i]))
                tmpTower.RemoveAt(i);
        }

        (_, int dropped) = DropBricks(tmpTower);

        total += dropped;
    }
    return total;
}
