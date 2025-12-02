using VTSV.AdventOfCode.Core;

internal sealed class Dec02 : Puzzle
{
    public override long Part1() => Run(IsValid1);

    public override long Part2() => Run(IsValid2);

    private long Run(Func<long, bool> validityCheck)
    {
        var ranges = Data.Split(',');
        long total = 0;
        foreach (var range in ranges)
        {
            var split = range.Split('-');

            var min = long.Parse(split[0]);
            var max = long.Parse(split[1]);
            var current = min;
            while (current <= max)
            {
                if (validityCheck(current))
                    total += current;
                current++;
            }
        }

        return total;
    }

    private static bool IsValid1(long current)
    {
        var str = current.ToString().AsSpan();
        var first = str[..(str.Length / 2)];
        var last = str[(str.Length / 2)..];
        return first.SequenceEqual(last);
    }

    private static bool IsValid2(long current)
    {
        var str = current.ToString().AsSpan();
        for (var len = (byte)(str.Length / 2); len > 0; len--)
        {
            if (str.Length % len == 0 && AllMatch(str, len))
                return true;
        }

        return false;
    }

    private static bool AllMatch(ReadOnlySpan<char> str, byte len)
    {
        var first = str[..len];
        var current = len * 2;
        while (current <= str.Length)
        {
            var sub = str[(current - len)..current];
            if (!sub.SequenceEqual(first))
                return false;
            current += len;
        }

        return true;
    }
}