using AoCUtils;
using Day17;

Console.WriteLine("Day17: Clumsy Crucible");

List<List<int>> input = FileUtil.ReadFileToIntGrid("inputSample.txt");
List<(int r, int c)> deltas = [(0, 1), (1, 0), (0, -1), (-1, 0)];

HashSet<Block> seen = [];
PriorityQueue<Block, int> pq = new();
pq.Enqueue(new Block(0, 0, 0, 0, 0, 0), 0);

Block x = new(4, 3, 6, -1, 0, 2);
Block y = new(4, 3, 6, -1, 0, 1);

Console.WriteLine($"{x} == {y} ? {x.Equals(y)}");

while (pq.Count > 0)
{
    Block b = pq.Dequeue();

    if (b.R == input.Count - 1 && b.C == input[0].Count - 1)
    {
        Console.WriteLine($"reached end: {b}");
        break;
    }

    if (seen.Contains(b))
    {
        Console.WriteLine($"already in seen: {b}");
        continue;
    }
    Console.WriteLine($"add to seen: {b}");
    seen.Add(b);

    if (b.N < 3 && (b.DR, b.DC) != (0, 0))
    {
        int nr = b.R + b.DR;
        int nc = b.C + b.DC;

        if (IsInBounds(input, nr, nc))
        {
            int weight = input[nr][nc];
            Block nb = new(b.H + weight, nr, nc, b.DR, b.DC, b.N + 1);
            if (b.N + 1 < 3)
            {
                Console.WriteLine($"add to queue: {nb}: ({pq.Count})");
                pq.Enqueue(nb, b.H + weight);
            }
        }
    }

    foreach ((int ndr, int ndc) in deltas)
    {
        //if ((ndr, ndc) != (b.DR, b.DC) && (ndr, ndc) != (-b.DR, -b.DC))
        //{
            Console.Write($"- new delta[{ndr},{ndc}]: ");
            int nr = b.R + ndr;
            int nc = b.C + ndc;

            if (IsInBounds(input, nr, nc))
            {
                int weight = input[nr][nc];
                Block nb = new(b.H + weight, nr, nc, ndr, ndc, b.N + 1);
                if (b.N + 1 < 3)
                {
                    Console.WriteLine($"add to queue: {nb}: ({pq.Count})");
                    pq.Enqueue(nb, b.H + weight);
                }
            }
            else
                Console.WriteLine();
        //}
    }
}

Console.WriteLine($"Part1: {0}");   // 102, 847

Console.WriteLine($"Part2: {0}");   // 94, 997

//============================================================================

bool IsInBounds(List<List<int>> grid, int row, int col)
{
    if (row >= 0 && col >= 0 && row < grid.Count && col < grid[0].Count)
        return true;
    return false;
}
