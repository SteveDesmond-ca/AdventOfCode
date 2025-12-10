using VTSV.AdventOfCode.Core;

internal sealed class Dec09 : Puzzle
{
    public override long Part1()
    {
        var lines = Data.Split('\n');
        var positions = lines.Select(l => l.ToPosition()).OrderBy(p => p.y).ThenBy(p => p.x).ToArray();
        var rects = new Dictionary<(Position, Position), long>();
        for (var x = 0; x < positions.Length - 1; x++)
        {
            for (var y = x + 1; y < positions.Length; y++)
            {
                var first = positions[x];
                var second = positions[y];
                var area = (1 + Math.Abs(first.y - second.y)) * (1 + Math.Abs(first.x - second.x));
                rects.Add((first, second), area);
            }
        }

        return rects.Max(d => d.Value);
    }

    public override long Part2()
    {
        throw new NotImplementedException();
    }
}