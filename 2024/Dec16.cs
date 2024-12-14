internal sealed class Dec16 : Puzzle
{
    public override long Part1()
    {
        var map = Data.Split("\n").Select(m => m.ToArray()).ToArray();
        var start = Find('S', map);

        var score = Move(map, new Dictionary<Position, long>(), start, Direction.East, 0);
        return score;
    }

    private static readonly Direction[] Turns = Enum.GetValues<Direction>();

    private static long Move(char[][] map, IDictionary<Position, long> paths, Position position, Direction direction, long score)
    {
        if (map[position.y][position.x] == 'E')
            return score;

        if (map[position.y][position.x] == '#' || (paths.TryGetValue(position, out var best) && best < score))
            return long.MaxValue;

        paths[position] = score;

        var min = long.MaxValue;
        foreach (var turn in Turns)
        {
            var next = position.GetNextPosition(turn);
            if (map[next.y][next.x] == '#')
                continue;

            var result = Move(map, paths, next, turn, score + (direction == turn ? 1 : 1001));
            if (result < min)
                min = result;
        }

        return min;
    }

    private static Position Find(char c, char[][] map)
    {
        for (var y = 1; y < map.Length - 1; y++)
        {
            for (var x = 1; x < map[0].Length - 1; x++)
            {
                if (map[y][x] == c)
                    return (y, x);
            }
        }

        throw new ArgumentException("No robot found!");
    }

    public override long Part2()
    {
        return 0;
    }
}