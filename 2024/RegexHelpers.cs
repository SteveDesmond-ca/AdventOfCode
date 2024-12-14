using System.Text.RegularExpressions;

internal static partial class RegexHelpers
{
    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    public static partial Regex Dec03();

    [GeneratedRegex(@"Button A: X\+(\d\d), Y\+(\d\d)\nButton B: X\+(\d\d), Y\+(\d\d)\nPrize: X=(\d{3,5}), Y=(\d{3,5})")]
    public static partial Regex Dec13();

    [GeneratedRegex(@"p=(-?\d{1,3}),(-?\d{1,3}) v=(-?\d\d?),(-?\d\d?)")]
    public static partial Regex Dec14();
}