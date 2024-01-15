using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day24: Never Tell Me The Odds");

bool test = true;

string fileName = "input.txt";
long min = 200000000000000;
long max = 400000000000000;
if (test)
{
    fileName = "inputSample.txt";
    min = 7;
    max = 27;
}
string[] input = FileUtil.ReadFileByLine(fileName);

List<List<long>> hail = [];
Regex re = new(@"-?\d+");
foreach (string line in input)
{
    MatchCollection mc = re.Matches(line);

    List<long> hs = mc.Select(m => Convert.ToInt64(m.Value)).ToList();
    hail.Add(hs);
    double angle = Math.Tan(((double)hs[4] / (double)hs[3]));
    //Console.WriteLine($"hs1:({hs[0]},{hs[1]},{hs[2]}) - ({hs[3],3},{hs[4],3},{hs[5],3}): angle:{angle}");

}

int answerPt1 = 0;
for (int i = 0; i < hail.Count - 1; i++)
    for (int j = i + 1; j < hail.Count; j++)
        if (WillIntersect(hail[i], hail[j], min, max))
            answerPt1++;

Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

Console.WriteLine($"Part2: {0}");

//============================================================================

// converted from StackOverflow post Determining if two rays intersect 
// dx = bs.x - as.x
// dy = bs.y - as.y
// det = bd.x * ad.y - bd.y * ad.x      // if det == 0, lines are parallel
// u = (dy * bd.x - dx * bd.y) / det    // if u or v are negative, rays don't intersect in future
// v = (dy * ad.x - dx * ad.y) / det
bool WillIntersect(List<long> hs1, List<long> hs2, long min, long max)
{
    long dx = hs2[0] - hs1[0];
    long dy = hs2[1] - hs1[1];
    long det = hs2[3] * hs1[4] - hs2[4] * hs1[3];

    if (det != 0)
    {
        long u = (dy * hs2[3] - dx * hs2[4]) / det;
        long v = (dy * hs1[3] - dx * hs1[4]) / det;
        if (u < 0 || v < 0)
            return false;
    
        long xIntercept = hs2[0] + hs2[3] * v;
        long yIntercept = hs2[1] + hs2[4] * v;

        // check if intercepts are in range
        if (xIntercept > min && xIntercept < max && yIntercept > min && yIntercept < max)
            return true;
    }

    return false;
}



