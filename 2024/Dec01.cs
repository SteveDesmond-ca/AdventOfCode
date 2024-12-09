internal sealed class Dec01 : Puzzle
{
    public Dec01() : base()
    {
    }

    public override long Part1()
    {
        var lines = Data.Split("\n");
        var left = lines.Select(l => int.Parse(l[..5])).OrderBy(l => l).ToArray();
        var right = lines.Select(l => int.Parse(l[5..])).OrderBy(l => l).ToArray();

        var sum = 0;
        for (var x = 0; x < lines.Length; x++)
        {
            sum += Math.Abs(right[x] - left[x]);
        }

        return sum;
    }

    public override long Part2()
    {
        var lines = Data.Split("\n");
        var left = lines.Select(l => int.Parse(l[..5])).ToArray();
        var right = lines.Select(l => int.Parse(l[5..])).ToArray();
        var counts = right.GroupBy(r => r).ToDictionary(r => r.Key, r => r.Count());
        return left.Sum(l => l * counts.GetValueOrDefault(l, 0));
    }
}