using VTSV.AdventOfCode.Core;

internal sealed class Dec14 : Puzzle
{
    private static readonly Position Max = (103, 101);

    private class Robot
    {
        public Position Position;
        public readonly Position Velocity;

        public Robot(string spec)
        {
            var match = RegexHelpers.Dec14().Match(spec).Groups;
            Position = (int.Parse(match[2].Value), int.Parse(match[1].Value));
            Velocity = (int.Parse(match[4].Value), int.Parse(match[3].Value));
        }
    }

    public override long Part1()
    {
        var robots = Data.Split("\n").Select(r => new Robot(r)).ToArray();
        for (var x = 0; x < 100; x++)
        {
            RunSimulation(robots);
        }

        var xSplit = Max.x / 2;
        var ySplit = Max.y / 2;
        var quadrants = new[] { new List<Robot>(), new List<Robot>(), new List<Robot>(), new List<Robot>() };
        foreach (var robot in robots)
        {
            if (robot.Position.x < xSplit && robot.Position.y < ySplit)
                quadrants[0].Add(robot);
            else if (robot.Position.x > xSplit && robot.Position.y < ySplit)
                quadrants[1].Add(robot);
            else if (robot.Position.x < xSplit && robot.Position.y > ySplit)
                quadrants[2].Add(robot);
            else if (robot.Position.x > xSplit && robot.Position.y > ySplit)
                quadrants[3].Add(robot);
        }

        return quadrants[0].Count * quadrants[1].Count * quadrants[2].Count * quadrants[3].Count;
    }

    private static void RunSimulation(Robot[] robots)
    {
        foreach (var robot in robots)
        {
            robot.Position.x += robot.Velocity.x + Max.x;
            robot.Position.y += robot.Velocity.y + Max.y;

            robot.Position.x %= Max.x;
            robot.Position.y %= Max.y;
        }
    }

    public override long Part2()
    {
        var robots = Data.Split("\n").Select(r => new Robot(r)).ToArray();
        for (var x = 1; x < 8000; x++)
        {
            RunSimulation(robots);
            if (!AreInTreeFormation(robots))
                continue;
            
            return x;
        }

        return 0;
    }

    private static bool AreInTreeFormation(Robot[] robots)
    {
        const byte theAnswerToLifeTheUniverseAndEverything = 42;
        const byte row = theAnswerToLifeTheUniverseAndEverything;
        for (var col = row; col < row + 30; col++)
        {
            if (!robots.Any(r => r.Position.y == row && r.Position.x == col))
                return false;
        }

        return true;
    }
}