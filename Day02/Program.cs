using AoCUtils;

Console.WriteLine("Day02: Cube Conundrum");

string[] input = FileUtil.ReadFileByLine("input.txt");

const int R = 12;
const int G = 13;
const int B = 14;

int r, g, b, total = 0, power = 0;

foreach (string line in input)
{
    string[] parts = line.Split(':');
    string[] gameID = parts[0].Split();
    string[] rounds = parts[1].Split(';');

    int maxR = 0, maxG = 0, maxB = 0;
    bool possible = true;

    foreach (string round in rounds)
    {
        (r, g, b) = CheckRound(round);

        if (r > R || g > G || b > B)
            possible = false;

        maxR = Math.Max(maxR, r);
        maxG = Math.Max(maxG, g);
        maxB = Math.Max(maxB, b);
    }

    if (possible)
        total += int.Parse(gameID[1]);

    power += maxR * maxG * maxB;
}

Console.WriteLine($"Part1: {total}");
Console.WriteLine($"Part2: {power}");

//============================================================================

static (int r, int g, int b) CheckRound(string round)
{
    int r = 0, g = 0, b = 0;

    string raw = round.Replace(",", "").Trim();
    string[] parts = raw.Split();

    int i = 1;
    while (i < parts.Length)
    {
        if (parts[i].StartsWith('r'))
            r = int.Parse(parts[i - 1]);
        if (parts[i].StartsWith('g'))
            g = int.Parse(parts[i - 1]);
        if (parts[i].StartsWith('b'))
            b = int.Parse(parts[i - 1]);
        i += 2;
    }

    return (r, g, b);
}

