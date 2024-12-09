using System.Text.RegularExpressions;

internal sealed class Dec03 : Puzzle
{
    private static readonly Regex Matcher = RegexHelpers.Dec03();

    public override long Part1()
        => GetMultipliedSum(Data);

    private static int GetMultipliedSum(string input)
        => Matcher.Matches(input).Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

    public override long Part2()
        => Data.Split("do()").Select(s => s.Split("don't()")[0]).Sum(GetMultipliedSum);
}