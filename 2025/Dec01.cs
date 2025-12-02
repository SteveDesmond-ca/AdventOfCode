using VTSV.AdventOfCode.Core;

internal sealed class Dec01 : Puzzle
{
    public override long Part1()
    {
        var pos = 50;
        var zeros = 0;

        foreach (var turn in GetTurns())
        {
            pos = (100 + pos + turn) % 100;
            if (pos == 0)
                zeros++;
        }

        return zeros;
    }

    private int[] GetTurns()
        => Data.Split('\n')
            .Select(l => l[0] == 'R' ? short.Parse(l[1..]) : -short.Parse(l[1..]))
            .ToArray();

    public override long Part2()
    {
        var pos = 50;
        var zeros = 0;

        foreach (var turn in GetTurns())
        {
            zeros += Math.Abs(pos + turn - (turn < 0 && pos > 0 ? 100 : 0)) / 100;
            pos = (100 * zeros + pos + turn) % 100;
        }

        return zeros;
    }
}