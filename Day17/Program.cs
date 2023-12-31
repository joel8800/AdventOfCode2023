using AoCUtils;
using Day17;

Console.WriteLine("Day17: Clumsy Crucible");

List<List<int>> grid = FileUtil.ReadFileToIntGrid("input.txt");

int answerPt1 = Dijkstra(grid, false);
Console.WriteLine($"Part1: {answerPt1}");

//----------------------------------------------------------------------------

int answerPt2 = Dijkstra(grid, true);
Console.WriteLine($"Part2: {answerPt2}");

//============================================================================

bool IsInBounds(List<List<int>> grid, int row, int col)
{
    if (row >= 0 && col >= 0 && row < grid.Count && col < grid[0].Count)
        return true;
    return false;
}

// Converted Hyperneutrino's python solution
int Dijkstra(List<List<int>> grid, bool isPart2)
{
    List<(int r, int c)> dirs = [(-1, 0), (0, 1), (1, 0), (0, -1)]; // N E S W

    PriorityQueue<Block, int> pq = new();
    HashSet<Block> seen = [];

    pq.Enqueue(new Block(0, 0, 0, 0, 0, 0), 0);

    while (pq.Count > 0)
    {
        Block curr = pq.Dequeue();

        // reached target?
        if (curr.R == grid.Count - 1 && curr.C == grid[0].Count - 1)
        {
            if (isPart2 && curr.Steps < 4)
                continue;

            return curr.Loss;
        }

        // skip if been there
        if (seen.Contains(curr))
            continue;
        seen.Add(curr);

        int minSteps = isPart2 ? 4 : 0;
        int maxSteps = isPart2 ? 10 : 3;

        // continue in same direction
        if (curr.Steps < maxSteps && (curr.DR, curr.DC) != (0, 0))
        {
            int newR = curr.R + curr.DR;
            int newC = curr.C + curr.DC;
            if (IsInBounds(grid, newR, newC))
            {
                Block nb = new(curr.Loss + grid[newR][newC], newR, newC, curr.DR, curr.DC, curr.Steps + 1);
                pq.Enqueue(nb, nb.Loss);
            }
        }

        // try other directions
        if (curr.Steps >= minSteps || (curr.DR, curr.DC) == (0, 0))
        {
            foreach ((int nextDR, int nextDC) in dirs)
            {   // exclude forward and backward, add only left and right turns
                if ((nextDR, nextDC) != (curr.DR, curr.DC) && (nextDR, nextDC) != (-curr.DR, -curr.DC))
                {
                    int newR = curr.R + nextDR;
                    int newC = curr.C + nextDC;
                    if (IsInBounds(grid, newR, newC))
                    {
                        Block nb = new(curr.Loss + grid[newR][newC], newR, newC, nextDR, nextDC, 1);
                        pq.Enqueue(nb, nb.Loss);
                    }
                }
            }
        }
    }

    return -1;
}
