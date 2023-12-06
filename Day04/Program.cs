using AoCUtils;
using System.Text.RegularExpressions;

Console.WriteLine("Day04: Scratchcards ");

string[] input = FileUtil.ReadFileByLine("input.txt");

int totalPart1 = 0;
int totalPart2 = 0;

Queue<int> cards = [];
Dictionary<int, int> scores = [];

foreach (string line in input)
{
    string card = Regex.Replace(line, @"\s+", " ");
    string[] parts = card.Split([':', '|']);
    string[] winners = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string[] myNumbers = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    int ID = int.Parse(parts[0].Split()[1]);
    
    int matches = 0;
    foreach (string num in winners)
    {
        if (myNumbers.Contains(num))
            matches++;
    }

    totalPart1 += (int)Convert.ToInt32(Math.Pow(2, matches - 1));

    // keep track of card matches per card for part 2
    scores.Add(ID, matches);
    cards.Enqueue(ID);
}

//----------------------------------------------------------------------------

// check each winning card, add new ones to queue
while (cards.Count > 0)
{
    totalPart2++;

    int cardID = cards.Dequeue();
    int cardsToAdd = scores[cardID];

    for (int i = 1; i <= cardsToAdd; i++)
    {
        if (cardID + i <= scores.Count)
            cards.Enqueue(cardID + i);
    }
}

Console.WriteLine($"Part1: {totalPart1}");
Console.WriteLine($"Part2: {totalPart2}");

//============================================================================