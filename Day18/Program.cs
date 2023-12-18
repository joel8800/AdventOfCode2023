using AoCUtils;

Console.WriteLine("Day18: Lavaduct Lagoon");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<(string d, int n, string color)> instructions = [];

Dictionary<string, (int r, int c)> dirs = new()
{{"R", (0, 1)}, {"D", (1, 0)}, {"L", (0, -1)}, {"U", (-1, 0) }};

foreach (string line in input)
{
    string[] parts = line.Split(' ');
    parts[2] = parts[2].Replace("(#", "").Replace(")", "");
    instructions.Add((parts[0], int.Parse(parts[1]), parts[2]));
}

List<(int r, int c)> pointsPt1 = [(0, 0)];

int borderPt1 = 0;
foreach (var (d, n, color) in instructions)
{
    borderPt1 += n;
    (int dr, int dc) = dirs[d];
    (int rr, int cc) = pointsPt1[^1];
    pointsPt1.Add((rr + dr * n, cc + dc * n));
}

// using shoelace formula and pick's theorem to get area
//
// Shoelace formula calculates and area (A) that can be fed to Pick's theorem
// formula to get the interior area of a polygon with integer coordinates
//
// i = A + b/2 + 1
// 
// total area is the interior area plus the border squares
//
// need to do first and last ones separately because they wrap around the ends of the list 
int sumPt1 = pointsPt1[0].r * (pointsPt1[^1].c - pointsPt1[1].c);
for (int i = 1; i < pointsPt1.Count - 1; i++)
{
    sumPt1 += pointsPt1[i].r * (pointsPt1[i - 1].c - pointsPt1[i + 1].c);
}
sumPt1 += pointsPt1[^1].r * (pointsPt1[^2].c - pointsPt1[0].c);

int interiorPt1 = (Math.Abs(sumPt1) / 2) - (borderPt1 / 2) + 1;

Console.WriteLine($"Part1: {interiorPt1 + borderPt1}");

//----------------------------------------------------------------------------
// need to use long for part 2

string dirMap = "RDLU";

long borderPt2 = 0;
List<(long r, long c)> pointsPt2 = [(0, 0)];

foreach (var (d, n, color) in instructions)
{
    string first5 = color.Substring(0, 5);
    long distance = Convert.ToInt64(first5, 16);
    int direction = Convert.ToInt32(color[^1]) - '0';
    
    borderPt2 += distance;
    
    (long dr, long dc) = dirs[dirMap[direction].ToString()];
    (long rr, long cc) = pointsPt2.Last();
    pointsPt2.Add((rr + dr * distance, cc + dc * distance));
}

long sumPt2 = pointsPt2[0].r * (pointsPt2[^1].c - pointsPt2[1].c);
for (int i = 1; i < pointsPt2.Count - 1; i++)
{
    sumPt2 += pointsPt2[i].r * (pointsPt2[i - 1].c - pointsPt2[i + 1].c);
}
sumPt2 += pointsPt2[^1].r * (pointsPt2[^2].c - pointsPt2[0].c);

long interiorPt2 = (Math.Abs(sumPt2) / 2) - (borderPt2 / 2) + 1;

Console.WriteLine($"Part2: {interiorPt2 + borderPt2}");

//============================================================================
