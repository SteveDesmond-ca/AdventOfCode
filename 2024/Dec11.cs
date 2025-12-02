using VTSV.AdventOfCode.Core;

internal sealed class Dec11 : Puzzle
{
    public override long Part1()
        => CountStones(25);

    public override long Part2()
        => CountStones(75);

    private long CountStones(byte blinks)
    {
        var cache = new Dictionary<(long, byte), long>();
        return Data.Split(" ").Select(long.Parse)
            .Sum(s => CountStoneRecursively(s, blinks, cache));
    }

    private static long CountStoneRecursively(long stone, byte blinksLeft, Dictionary<(long, byte), long> cache)
    {
        while (blinksLeft > 0)
        {
            blinksLeft--;
            if (stone == 0)
            {
                if (blinksLeft > 0)
                {
                    stone = 2024;
                    blinksLeft--;
                }
                else
                {
                    stone = 1;
                }

                continue;
            }

            var digits = 1 + (int)Math.Log10(stone);
            if (digits % 2 == 1)
            {
                stone *= 2024;
                continue;
            }

            if (cache.TryGetValue((stone, blinksLeft), out var cachedValue))
                return cachedValue;

            var splitter = (long)Math.Pow(10, digits / 2);
            var sum = CountStoneRecursively(stone / splitter, blinksLeft, cache)
                + CountStoneRecursively(stone % splitter, blinksLeft, cache);

            cache.Add((stone, blinksLeft), sum);
            return sum;
        }

        return 1;
    }
}