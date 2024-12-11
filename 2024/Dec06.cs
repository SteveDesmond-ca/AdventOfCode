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
        var direction = Direction.Up;
        var position = GetStartPosition(map);
        var next = position;

        do
        {
            if (WithinBounds(next, map[0].Length, map.Length) && map[next.y][next.x] == '#')
            {
                direction = TurnRight(direction);
            }
            else
            {
                map[position.y][position.x] = 'X';
                position = next;
            }

            next = GetNextPosition(position, direction);
        } while (WithinBounds(position, map[0].Length, map.Length));

        return map.Sum(r => r.Count(c => c == 'X'));
    }

    private static bool WithinBounds(Position position, int mapWidth, int mapLength)
        => position is { x: >= 0, y: >= 0 } && position.x < mapWidth && position.y < mapLength;

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
        var map = Data.Split("\n").Select(r => r.ToArray()).ToArray();
        var mapWidth = map[0].Length;
        var mapLength = map.Length;
        var start = GetStartPosition(map);
        var visited = new byte[map.Length, map[0].Length];
        var potentials = WalkMap(map);
        var blockers = new List<Position>();
        Position previousBlocker = (0, 0);


        //TODO make this not O(n³)
        foreach (var potential in potentials)
        {
            map[previousBlocker.y][previousBlocker.x] = '.';
            previousBlocker = potential;
            map[potential.y][potential.x] = '#';

            Array.Clear(visited);
            var direction = Direction.Up;
            var position = start;
            var next = position;
            var loopFound = false;

            while (WithinBounds(next, mapWidth, mapLength) && !loopFound)
            {
                if (map[next.y][next.x] == '#')
                {
                    direction = TurnRight(direction);
                }
                else
                {
                    if (++visited[position.y, position.x] > 4)
                    {
                        blockers.Add(potential);
                        loopFound = true;
                    }

                    position = next;
                }

                next = GetNextPosition(position, direction);
            }
        }

        return blockers.Count;
    }

    private static Position[] WalkMap(char[][] map)
    {
        var nexts = new List<Position>();

        var direction = Direction.Up;
        var position = GetStartPosition(map);
        var next = position;

        do
        {
            var withinBounds = WithinBounds(next, map[0].Length, map.Length);
            if (withinBounds && map[next.y][next.x] == '.')
            {
                nexts.Add(next);
            }
            if (withinBounds && map[next.y][next.x] == '#')
            {
                direction = TurnRight(direction);
            }
            else
            {
                position = next;
            }

            next = GetNextPosition(position, direction);
        } while (WithinBounds(position, map[0].Length, map.Length));

        return nexts.Distinct().ToArray();
    }
}