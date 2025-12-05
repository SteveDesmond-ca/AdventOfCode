using VTSV.AdventOfCode.Core;

internal sealed class Dec05 : Puzzle
{
    public override long Part1()
    {
        var sections = Data.Split("\n\n");
        var freshRanges = GetFreshRanges(sections[0].Split('\n'));
        var available = sections[1].Split('\n').Select(long.Parse).ToArray();
        return available.Count(i => freshRanges.Any(r => r.min <= i && r.max >= i));
    }

    private static (long min, long max)[] GetFreshRanges(string[] ranges)
    {
        var freshRanges = new List<(long min, long max)>();
        foreach (var range in ranges)
        {
            var split = range.Split('-');
            var min = long.Parse(split[0]);
            var max = long.Parse(split[1]);
            freshRanges.Add((min, max));
        }

        return freshRanges
            .OrderBy(r => r.min)
            .ThenBy(r => r.max)
            .ToArray();
    }

    public override long Part2()
    {
        var sections = Data.Split("\n\n");
        var freshRanges = GetFreshRanges(sections[0].Split('\n'));

        long total = 0;
        long currentMax = 0;
        foreach (var range in freshRanges)
        {
            if (currentMax >= range.max)
                continue;

            if (currentMax >= range.min)
                total += range.max - currentMax;
            else
                total += range.max - range.min + 1;

            currentMax = range.max;
        }

        return total;
    }
}