using System.Collections.Concurrent;

internal sealed class Dec05 : Puzzle
{
    private readonly byte[][] _rules;
    private readonly byte[][] _pages;

    public Dec05()
    {
        var split = Data.Split("\n\n");
        _rules = split[0].Split("\n").Select(r => r.Split("|").Select(byte.Parse).ToArray()).ToArray();
        _pages = split[1].Split("\n").Select(p => p.Split(",").Select(byte.Parse).ToArray()).ToArray();
    }

    public override long Part1()
        => _pages.Where(pageSet => _rules.All(rule => PageSetFollowsRule(pageSet, rule)))
            .Sum(pageSet => pageSet[pageSet.Length / 2]);

    private static bool PageSetFollowsRule(Span<byte> pageSet, byte[] rule)
    {
        var left = pageSet.IndexOf(rule[0]);
        var right = pageSet.IndexOf(rule[1]);
        return left == -1 || right == -1 || left < right;
    }

    public override long Part2()
    {
        var sum = 0;
        foreach (var pageSet in _pages)
        {
            var matchingRules = _rules.Where(r => pageSet.Contains(r[0]) && pageSet.Contains(r[1])).ToArray();
            if (matchingRules.All(rule => PageSetFollowsRule(pageSet, rule)))
                continue;

            var fixedPageSet = FixPageSet(pageSet, matchingRules);
            sum += fixedPageSet[fixedPageSet.Length / 2];
        }

        return sum;
    }

    private static byte[] FixPageSet(byte[] pageSet, byte[][] rules)
    {
        var corrected = new byte[pageSet.Length];
        Array.Copy(pageSet, corrected, pageSet.Length);

        for (var x = 0; x < pageSet.Length; x++)
        {
            foreach (var rule in rules)
            {
                if (!PageSetFollowsRule(corrected, rule))
                    SwapPages(corrected, rule);
            }
        }

        return corrected;
    }

    private static void SwapPages(Span<byte> pageSet, byte[] rule)
    {
        var left = pageSet.IndexOf(rule[0]);
        var right = pageSet.IndexOf(rule[1]);
        pageSet[left] = rule[1];
        pageSet[right] = rule[0];
    }
}