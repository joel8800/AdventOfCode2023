using AoCUtils;
using Day07;

Console.WriteLine("Day07: Camel Cards");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<Hand> hands = [];
foreach (string line in input)
{
    int bid = int.Parse(line.Split()[1]);
    string cards = line.Split()[0];
    hands.Add(new(cards, bid));
}

List<Hand> rankedPt1 = [.. hands.OrderBy(h => h.CompStr).OrderBy(h => h.Type)];

int winningsPt1 = 0;
for (int i = 0; i < rankedPt1.Count; i++)
    winningsPt1 += rankedPt1[i].Bid * (i + 1);

Console.WriteLine($"Part1: {winningsPt1}");

//----------------------------------------------------------------------------

foreach (Hand hand in hands)
    hand.UpdateForPart2();

List<Hand> rankedPt2 = [.. hands.OrderBy(h => h.CompStr).OrderBy(h => h.Type)];

int winningsPt2 = 0;
for (int i = 0; i < rankedPt2.Count; i++)
    winningsPt2 += rankedPt2[i].Bid * (i + 1);

Console.WriteLine($"Part2: {winningsPt2}"); 

//============================================================================