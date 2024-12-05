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

    public override int Part1()
        => _pages.Where(pageSet => _rules.Where(r => pageSet.Contains(r[0]) && pageSet.Contains(r[1]))
                .All(rule => PageSetFollowsRule(pageSet, rule)))
            .Aggregate(0, (current, pageSet) => current + pageSet[pageSet.Length / 2]);

    private static bool PageSetFollowsRule(byte[] pageSet, byte[] rule)
        => Array.FindIndex(pageSet, p => p == rule[0]) < Array.FindIndex(pageSet, p => p == rule[1]);

    public override int Part2()
    {
        var sum = 0;
        foreach (var pageSet in _pages)
        {
            var matchingRules = _rules.Where(r => pageSet.Contains(r[0]) && pageSet.Contains(r[1]))
                .OrderBy(r => r[0]).ThenBy(r => r[1]).ToArray();

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

    private static void SwapPages(byte[] pageSet, byte[] rule)
    {
        var left = Array.FindIndex(pageSet, p => p == rule[0]);
        var right = Array.FindIndex(pageSet, p => p == rule[1]);
        pageSet[left] = rule[1];
        pageSet[right] = rule[0];
    }
}