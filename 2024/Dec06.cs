internal sealed class Dec06 : Puzzle
{
    private enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public override long Part1()
    {
        var map = Data.Split("\n").Select(r => r.ToArray()).ToArray();
        var visited = new byte[map.Length, map[0].Length];
        var direction = Direction.Up;
        var position = GetStartPosition(map);
        Position next;

        do
        {
            next = GetNextPosition(position, direction);
            if (WithinBounds(map, next) && map[next.y][next.x] == '#')
            {
                direction = TurnRight(direction);
            }
            else
            {
                map[position.y][position.x] = 'X';
                visited[position.y, position.x]++;
                position = next;
            }
        } while (WithinBounds(map, next));

        return map.Sum(r => r.Count(c => c == 'X'));
    }

    private static bool WithinBounds(char[][] map, Position next)
    {
        return next.y >= 0 && next.y < map.Length && next.x >= 0 && next.x < map[0].Length;
    }

    private static Direction TurnRight(Direction direction)
        => (Direction)((int)(direction + 1) % 4);

    private static Position GetNextPosition(Position position, Direction direction)
        => direction switch
        {
            Direction.Up => (position.y - 1, position.x),
            Direction.Right => (position.y, position.x + 1),
            Direction.Down => (position.y + 1, position.x),
            Direction.Left => (position.y, position.x - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

    private static Position GetStartPosition(char[][] map)
    {
        var y = 0;
        while (!map[y].Contains('^'))
            y++;

        return (y, map[y].AsSpan().IndexOf('^'));
    }

    public override long Part2()
    {
        var originalMap = Data.Split("\n").Select(r => r.ToArray()).ToArray();
        var blockers = new List<Position>();

        for (var y = 0; y < originalMap.Length; y++)
        {
            for (var x = 0; x < originalMap[y].Length; x++)
            {
                var map = Data.Split("\n").Select(r => r.ToArray()).ToArray();
                if (map[y][x] == '.')
                    map[y][x] = '#';
                else
                    continue;

                var visited = new byte[map.Length, map[0].Length];
                var direction = Direction.Up;
                var position = GetStartPosition(map);
                Position next;
                var loopFound = false;

                do
                {
                    next = GetNextPosition(position, direction);
                    if (WithinBounds(map, next) && map[next.y][next.x] == '#')
                    {
                        direction = TurnRight(direction);
                    }
                    else
                    {
                        if (visited[position.y, position.x]++ > 3)
                        {
                            blockers.Add((y, x));
                            loopFound = true;
                        }

                        position = next;
                    }
                } while (WithinBounds(map, next) && !loopFound);
            }
        }

        return blockers.Count;
    }
}