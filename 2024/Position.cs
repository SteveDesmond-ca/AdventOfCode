global using Position = (int y, int x);

internal static class PositionExtensions
{
    public static Position GetNextPosition(this Position position, Direction direction)
        => direction switch
        {
            Direction.North => (position.y - 1, position.x),
            Direction.East => (position.y, position.x + 1),
            Direction.South => (position.y + 1, position.x),
            Direction.West => (position.y, position.x - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
}