internal sealed class Dec04 : Puzzle
{
    public override int Part1()
    {
        var found = 0;
        var grid = Data.Split("\n").Select(l => l.ToArray()).ToArray();
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                found += FindWords(grid, row, col);
            }
        }

        return found;
    }

    private static int FindWords(char[][] grid, int row, int col)
    {
        var found = 0;

        if (col < grid[row].Length - 3
                && grid[row][col] == 'X' && grid[row][col + 1] == 'M' && grid[row][col + 2] == 'A' && grid[row][col + 3] == 'S')
            found++;

        if (row < grid.Length - 3 && col < grid[row].Length - 3
                && grid[row][col] == 'X' && grid[row + 1][col + 1] == 'M' && grid[row + 2][col + 2] == 'A' && grid[row + 3][col + 3] == 'S')
            found++;

        if (row < grid.Length - 3
                && grid[row][col] == 'X' && grid[row + 1][col] == 'M' && grid[row + 2][col] == 'A' && grid[row + 3][col] == 'S')
            found++;

        if (row < grid.Length - 3 && col > 2
                && grid[row][col] == 'X' && grid[row + 1][col - 1] == 'M' && grid[row + 2][col - 2] == 'A' && grid[row + 3][col - 3] == 'S')
            found++;

        if (col > 2
                && grid[row][col] == 'X' && grid[row][col - 1] == 'M' && grid[row][col - 2] == 'A' && grid[row][col - 3] == 'S')
            found++;

        if (row > 2 && col > 2
                && grid[row][col] == 'X' && grid[row - 1][col - 1] == 'M' && grid[row - 2][col - 2] == 'A' && grid[row - 3][col - 3] == 'S')
            found++;

        if (row > 2
                && grid[row][col] == 'X' && grid[row - 1][col] == 'M' && grid[row - 2][col] == 'A' && grid[row - 3][col] == 'S')
            found++;

        if (row > 2 && col < grid[row].Length - 3
                && grid[row][col] == 'X' && grid[row - 1][col + 1] == 'M' && grid[row - 2][col + 2] == 'A' && grid[row - 3][col + 3] == 'S')
            found++;

        return found;
    }

    public override int Part2()
    {
        var found = 0;
        var grid = Data.Split("\n").Select(l => l.ToArray()).ToArray();
        for (var row = 1; row < grid.Length - 1; row++)
        {
            for (var col = 1; col < grid[row].Length - 1; col++)
            {
                found += FindCross(grid, row, col) ? 1 : 0;
            }
        }

        return found;
    }

    private static bool FindCross(char[][] grid, int row, int col)
    {
        return grid[row][col] == 'A'
            && ((grid[row - 1][col - 1] == 'M' && grid[row + 1][col + 1] == 'S')
                || (grid[row - 1][col - 1] == 'S' && grid[row + 1][col + 1] == 'M'))
            && ((grid[row + 1][col - 1] == 'M' && grid[row - 1][col + 1] == 'S')
                || (grid[row + 1][col - 1] == 'S' && grid[row - 1][col + 1] == 'M'));
    }
}