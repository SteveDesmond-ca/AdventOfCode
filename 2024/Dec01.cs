internal sealed class Dec01 : Puzzle
{
    public Dec01() : base()
    {
    }

    public override int Part1()
    {
        var lines = Data.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        var left = lines.Select(l => int.Parse(l[..5])).OrderBy(l => l).ToArray();
        var right = lines.Select(l => int.Parse(l[5..])).OrderBy(l => l).ToArray();

        var sum = 0;
        for (var x = 0; x < lines.Length; x++)
        {
            sum += Math.Abs(right[x] - left[x]);
        }

        return sum;
    }

    public override int Part2()
    {
        var lines = Data.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        var left = lines.Select(l => int.Parse(l[..5])).ToArray();
        var right = lines.Select(l => int.Parse(l[5..])).ToArray();
        return left.Sum(l => l * right.Count(r => r == l));
    }
}