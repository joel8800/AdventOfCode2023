using AoCUtils;

Console.WriteLine("Day01: Trebuchet?!");

string[] input = FileUtil.ReadFileByLine("input.txt");

int part1 = 0;
int part2 = 0;

// get all the digits in the line, combine the first and last digits,
// and keep a running total
foreach (string line in input)
{
    List<char> digits = line.Where(c => char.IsDigit(c)).ToList();
    
    part1 += int.Parse(digits.First().ToString() + digits.Last().ToString());
}

Console.WriteLine($"Part1: {part1}");

//----------------------------------------------------------------------------

foreach (string line in input)
{
    int first = 0;
    int last = 0;

    // search from start of line
    for (int i = 0; i < line.Length; i++)
    {
        first = GetRealDigit(line.Substring(i));
        if (first != -1)
            break;
    }

    // search from end of line
    for (int i = line.Length - 1; i >= 0 ; i--)
    {
        last = GetRealDigit(line.Substring(i));
        if (last != -1)
            break;
    }

    part2 += (first * 10) + last;
}

Console.WriteLine($"Part2: {part2}");

//============================================================================

static int GetRealDigit(string line)
{
    // return if first char is a digit
    if (char.IsDigit(line[0]))
        return int.Parse(line[0].ToString());

    // check to see if digit is spelled out
    if (line.StartsWith("one"))
        return 1;
    if (line.StartsWith("two"))
        return 2;
    if (line.StartsWith("three"))
        return 3;
    if (line.StartsWith("four"))
        return 4;
    if (line.StartsWith("five"))
        return 5;
    if (line.StartsWith("six"))
        return 6;
    if (line.StartsWith("seven"))
        return 7;
    if (line.StartsWith("eight"))
        return 8;
    if (line.StartsWith("nine"))
        return 9;
    if (line.StartsWith("zero"))
        return 0;

    return -1;
}