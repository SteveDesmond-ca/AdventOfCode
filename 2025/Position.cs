global using Position = (long y, long x);

public static class PositionExtensions
{
    public static Position ToPosition(this string line)
    {
        var coords = line.Split(',');
        return new Position(int.Parse(coords[0]), int.Parse(coords[1]));
    }
}