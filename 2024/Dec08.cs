using VTSV.AdventOfCode.Core;

internal sealed class Dec08 : Puzzle
{
    public override long Part1()
    {
        var grid = Data.Split("\n").Select(r => r.ToArray()).ToArray();
        return CountAntiNodes(grid, 1);
    }

    public override long Part2()
    {
        var grid = Data.Split("\n").Select(r => r.ToArray()).ToArray();
        var antiNodeHops = Math.Max(grid.Length, grid[0].Length);
        return CountAntiNodes(grid, antiNodeHops);
    }

    private int CountAntiNodes(char[][] grid, int antiNodeHops)
    {
        var frequencies = Data.Replace("\n", "").Replace(".", "").Distinct().ToArray();
        var nodes = frequencies.ToDictionary(f => f, f => new List<Position>());
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                var frequency = grid[y][x];
                if (frequency != '.')
                {
                    nodes[frequency].Add((y,x));
                }
            }
        }

        var antiNodes = new List<Position>();
        foreach (var frequency in nodes)
        {
            foreach (var thisNode in frequency.Value)
            {
                if (frequency.Value.Count > 2)
                    antiNodes.Add(thisNode);
                
                foreach (var otherNode in frequency.Value.Except([thisNode]))
                {
                    Position antiDistance = (otherNode.y - thisNode.y, otherNode.x - thisNode.x);
                    for (var x = 1; x <= antiNodeHops; x++)
                    {
                        Position antiNode = (otherNode.y + x * antiDistance.y, otherNode.x + x * antiDistance.x);
                        if (antiNode.x >= 0 && antiNode.y >= 0 && antiNode.x < grid[0].Length && antiNode.y < grid.Length)
                            antiNodes.Add(antiNode);
                    }
                }
            }
        }
        
        return antiNodes.Distinct().Count();
    }
}