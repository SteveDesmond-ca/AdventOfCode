using VTSV.AdventOfCode.Core;

internal sealed class Dec03 : Puzzle
{
    public override long Part1()
        => Data.Split('\n')
            .Select(GenerateMap)
            .Select(map => GetHighestJoltage(map, 2))
            .Sum();

    private static Dictionary<byte, List<byte>> GenerateMap(string line)
    {
        var map = Enumerable.Range(1, 9).ToDictionary<int, byte, List<byte>>(n => (byte)n, _ => []);
        var nums = line.Select(chr => byte.Parse(chr.ToString())).ToArray();

        for (byte pos = 0; pos < nums.Length; pos++)
            map[nums[pos]].Add(pos);

        return map;
    }

    private static long GetHighestJoltage(Dictionary<byte, List<byte>> map, byte batteries)
    {
        var maxLength = map.SelectMany(n => n.Value).Max();
        var digits = new Dictionary<byte, long>();
        byte start = 0;
        for (byte battery = 0; battery < batteries; battery++)
        {
            var digit = GetBestDigit(map, 9, start, (byte)(maxLength - batteries + battery + 1));
            digits.Add(battery, digit.Digit);
            start = (byte)(digit.Index + 1);
        }

        return digits.Sum(digit => digit.Value * (long)Math.Pow(10, batteries - digit.Key - 1));
    }

    private static (long Digit, byte Index) GetBestDigit(Dictionary<byte, List<byte>> map, byte num, byte start, byte end)
    {
        var hasMatch = map[num].Any(p => p >= start && p <= end);
        if (!hasMatch)
            return GetBestDigit(map, (byte)(num - 1), start, end);

        var index = map[num].First(p => p >= start && p <= end);
        return (num, index);
    }

    public override long Part2()
        => Data.Split('\n')
            .Select(GenerateMap)
            .Select(map => GetHighestJoltage(map, 12))
            .Sum();
}