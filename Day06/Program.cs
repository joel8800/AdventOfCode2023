using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day06: Wait For It");

string[] input = FileUtil.ReadFileByLine("input.txt");

string timeLine = Regex.Replace(input[0], @"\s+", " ");
string distLine = Regex.Replace(input[1], @"\s+", " ");

List<int> timePt1 = timeLine.Split(':', StringSplitOptions.TrimEntries)[1].Split().Select(x => int.Parse(x)).ToList();
List<int> distPt1 = distLine.Split(':', StringSplitOptions.TrimEntries)[1].Split().Select(x => int.Parse(x)).ToList();

List<int> wins = [];
for (int i = 0; i < timePt1.Count; i++)
{
    int raceWins = 0;
    for (int t = 0; t < timePt1[i]; t++)
    {
        int d = t * (timePt1[i] - t);
        if (d > distPt1[i])
            raceWins++;
    }
    wins.Add(raceWins);
}

int raceWinsPt1 = wins.Aggregate((w, nextW) => w * nextW);

Console.WriteLine($"Part1: {raceWinsPt1}");

//----------------------------------------------------------------------------

timeLine = Regex.Replace(input[0], @"\s+", "");
distLine = Regex.Replace(input[1], @"\s+", "");

long timePt2 = long.Parse(timeLine.Split(':')[1]);
long distPt2 = long.Parse(distLine.Split(':')[1]);

long raceWinsPt2 = 0;
for (long t = 0; t < timePt2; t++)
{
    long d = t * (timePt2 - t);
    if (d > distPt2)
        raceWinsPt2++;
}

Console.WriteLine($"Part2: {raceWinsPt2}");

//============================================================================