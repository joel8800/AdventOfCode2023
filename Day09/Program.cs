using AoCUtils;

Console.WriteLine("Day09: Mirage Maintenance");

string[] inData = FileUtil.ReadFileByLine("input.txt");

List<List<int>> input = [];
foreach (string line in inData)
{
    string[] nums = line.Split();
    List<int> list = nums.Select(x => int.Parse(x)).ToList();
    input.Add(list);
}

int answerPt1 = 0;
foreach (List<int> list in input)
    answerPt1 += GetNextNumber(list, false);

Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

int answerPt2 = 0;
foreach (List<int> list in input)
    answerPt2 += GetNextNumber(list, true);

Console.WriteLine($"Part2: {answerPt2}");

//============================================================================

int GetNextNumber(List<int> list, bool isPart2)
{
    List<List<int>> lists = [list];

    while (true)
    {
        List<int> nextList = GetNextList(lists.Last());
        lists.Add(nextList);

        if (nextList.All(x => x == 0))
            break;
    }

    lists.Last().Add(0);

    for (int i = lists.Count - 2; i >= 0; i--)
    {
        if (isPart2)
        {
            int a = lists[i].First() - lists[i + 1].First();
            lists[i].Insert(0, a);
        }
        else
        {
            int a = lists[i + 1].Last() + lists[i].Last();
            lists[i].Add(a);
        }
    }

    if (isPart2)
        return lists[0].First();
    else
        return lists[0].Last();
}

List<int> GetNextList(List<int> list)
{
    List<int> nextList = [];
    
    for (int i = 0; i < list.Count - 1; i++)
        nextList.Add(list[i + 1] - list[i]);

    return nextList;
}