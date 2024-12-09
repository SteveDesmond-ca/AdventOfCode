internal sealed class Dec02 : Puzzle
{
    private enum Direction
    {
        Up,
        Down
    }

    public override long Part1()
        => Data.Split("\n").Select(GetLevels).Count(IsSafe);

    private static bool IsSafe(byte[] levels)
    {
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

    public override long Part2()
        => Data.Split("\n").Select(GetLevels).Count(IsSafeWhenDampened);

    private static bool IsSafeWhenDampened(byte[] levels)
    {
        if (IsSafe(levels))
            return true;
        
        for (var x = 0; x < levels.Length; x++)
        {
            var dampened = levels.ToList();
            dampened.RemoveAt(x);
            if (IsSafe(dampened.ToArray()))
                return true;
        }

        return false;
    }

    private static byte[] GetLevels(string report)
    {
        return report.Split(" ").Select(byte.Parse).ToArray();
    }
}