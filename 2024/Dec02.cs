internal sealed class Dec02 : Puzzle
{
    private enum Direction
    {
        Up,
        Down
    }

    public override int Part1()
    {
        var reports = Data.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        return reports.Count(IsSafe);
    }

    private static bool IsSafe(string report)
    {
        var levels = report.Split(" ").Select(byte.Parse).ToArray();
        var direction = levels[0] < levels[1] ? Direction.Up : Direction.Down;

        for (var x = 1; x < levels.Length; x++)
        {
            if (direction == Direction.Up && levels[x - 1] > levels[x]
                || direction == Direction.Down && levels[x - 1] < levels[x]
                || Math.Abs(levels[x-1] - levels[x]) > 3
                || Math.Abs(levels[x-1] - levels[x]) < 1)
                return false;
        }

        return true;
    }

    public override int Part2()
    {
        var reports = Data.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        return reports.Count(IsSafeWhenDampened);
    }

    private static bool IsSafeWhenDampened(string report)
    {
        if (IsSafe(report))
            return true;
        
        var levels = report.Split(" ").Select(byte.Parse).ToArray();
        for (var x = 0; x < levels.Length; x++)
        {
            var dampened = levels.ToList();
            dampened.RemoveAt(x);
            if (IsSafe(string.Join(" ", dampened)))
                return true;
        }

        return false;
    }
}