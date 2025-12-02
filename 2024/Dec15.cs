using VTSV.AdventOfCode.Core;

internal sealed class Dec15 : Puzzle
{
    public override long Part1()
    {
        var parts = Data.Split("\n\n");
        var map = parts[0].Split("\n").Select(m => m.ToArray()).ToArray();
        var directions = parts[1].Where(d => d != '\n').ToArray();
        var robot = FindRobot(map);

        foreach (var direction in directions)
        {
            Follow(direction, ref robot, map);
        }

        return GPSSum(map);
    }

    private long GPSSum(char[][] map)
    {
        var sum = 0;
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                if (map[y][x] == 'O')
                    sum += 100 * y + x;
            }
        }

        return sum;
    }

    private static void Debug(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                Console.Write(map[y][x]);
            }
            Console.WriteLine();
        }
    }

    private static Position FindRobot(char[][] map)
    {
        for (var y = 1; y < map.Length - 1; y++)
        {
            for (var x = 1; x < map[0].Length - 1; x++)
            {
                if (map[y][x] == '@')
                    return (y, x);
            }
        }

        throw new ArgumentException("No robot found!");
    }

    private static void Follow(char direction, ref Position robot, char[][] map)
        => Move(ref robot, GetStep(direction), map);

    private static Position GetStep(char direction)
        => direction switch
        {
            '^' => new Position(-1, 0),
            '>' => new Position(0, 1),
            'v' => new Position(1, 0),
            '<' => new Position(0, -1),
            _ => throw new ArgumentException("Invalid direction!")
        };

    private static void Move(ref Position robot, Position step, char[][] map)
    {
        var position = new Position(robot.y + step.y, robot.x + step.x);
        while (map[position.y][position.x] == 'O')
        {
            position.x += step.x;
            position.y += step.y;
        }

        if (map[position.y][position.x] == '#')
            return;

        var stepBack = new Position(-step.y, -step.x);
        while (position.x + stepBack.x != robot.x || position.y + stepBack.y != robot.y)
        {
            map[position.y][position.x] = 'O';
            map[position.y + stepBack.y][position.x + stepBack.x] = '.';
            position.x += stepBack.x;
            position.y += stepBack.y;
        }

        map[robot.y][robot.x] = '.';
        robot.x = position.x;
        robot.y = position.y;
        map[robot.y][robot.x] = '@';
    }

    public override long Part2()
    {
        return 0;
    }
}