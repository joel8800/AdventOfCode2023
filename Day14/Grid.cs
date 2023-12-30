namespace Day14
{
    internal class Grid
    {
        public List<List<char>> _grid;

        public Grid(List<List<char>> grid)
        {
            _grid = grid;
        }

        public List<char> GetRow(int row)
        {
            return _grid[row];
        }


        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            Grid? other = obj as Grid;

            for (int r = 0; r < _grid.Count; r++)
            {
                string myRow = new(_grid[r].ToArray());
                string theirRow = new(other.GetRow(r).ToArray());

                if (myRow != theirRow)
                    return false;
            }
            return true;
        }
    }
}
