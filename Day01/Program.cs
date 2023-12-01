using AoCUtils;

Console.WriteLine("Day01: Trebuchet?!");

string[] input = FileUtil.ReadFileByLine("input.txt");

int part1 = 0;
int part2 = 0;

foreach (string line in input)
{
    int first = 0;
    int last = 0;

    for (int i = 0; i < line.Length; i++)
    {
        first = IsDigit(line.Substring(i));
        if (first != -1)
            break;
    }

    for (int i = line.Length - 1; i >= 0; i--)
    {
        last = IsDigit(line.Substring(i));
        if (last != -1)
            break;
    }

    part1 += (first * 10) + last;
}

Console.WriteLine(part1);


foreach (string line in input)
{
    int digits = 0;
    int first = 0;
    int last = 0;
    for (int i = 0; i < line.Length; i++)
    {
        first = IsReal(line.Substring(i));
        if (first != -1)
            break;
    }

    for (int i = line.Length - 1; i >= 0 ; i--)
    {
        last = IsReal(line.Substring(i));
        if (last != -1)
            break;
    }

    digits = (first * 10) + last;
    part2 += digits;
}

Console.WriteLine(part2);

//============================================================================

int IsDigit(string line)
{
    int digit = -1;

    if (char.IsDigit(line[0]))
        digit = int.Parse(line[0].ToString());

    return digit;
}

int IsReal(string line)
{
    if (char.IsDigit(line[0]))
        return int.Parse(line[0].ToString());

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