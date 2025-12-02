using VTSV.AdventOfCode.Core;

internal sealed class Dec10 : Puzzle
{
    public override long Part1()
    {
        var grid = Data.Split("\n").Select(r => r.Select(c => byte.Parse(c.ToString())).ToArray()).ToArray();

        var sum = 0;
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x] != 0)
                    continue;
                
                var endPoints = FindPaths(grid, (y, x), 0);
                sum += endPoints.Distinct().Count();
            }
        }

        return sum;
    }

    private static Position[] FindPaths(byte[][] grid, Position position, int elevation)
    {
        if (elevation == 9)
            return [position];

        var up = (position.y - 1, position.x);
        var right = (position.y, position.x + 1);
        var down = (position.y + 1, position.x);
        var left = (position.y, position.x - 1);
        var directions = new Position[] { up, right, down, left };

        var pathsUp = directions
            .Where(direction => direction.x >= 0 && direction.x < grid[0].Length
                 && direction.y >= 0 && direction.y < grid.Length
                 && grid[direction.y][direction.x] == elevation + 1)
            .ToArray();

        return pathsUp.SelectMany(p => FindPaths(grid, p, elevation + 1)).ToArray();
    }

    public override long Part2()
    {
        var grid = Data.Split("\n").Select(r => r.Select(c => byte.Parse(c.ToString())).ToArray()).ToArray();

        var sum = 0;
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x] != 0)
                    continue;
                
                var endPoints = FindPaths(grid, (y, x), 0);
                sum += endPoints.Length;
            }
        }

        return sum;
    }
}