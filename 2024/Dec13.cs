internal sealed class Dec13 : Puzzle
{
    private struct Machine
    {
        public readonly Position A;
        public readonly Position B;
        public readonly Position Prize;

        public Machine(string spec)
        {
            var match = RegexHelpers.Dec13().Match(spec).Groups;
            A = (int.Parse(match[2].Value), int.Parse(match[1].Value));
            B = (int.Parse(match[4].Value), int.Parse(match[3].Value));
            Prize = (int.Parse(match[6].Value), int.Parse(match[5].Value));
        }
    }
    
    public override long Part1()
    {
        var machines = Data.Split("\n\n").Select(m => new Machine(m)).ToArray();
        return machines.Select(GetMin).Where(m => m is not null).Cast<int>().Sum();
    }

    private static int? GetMin(Machine machine)
    {
        var maxA = Math.Min(Math.Min(machine.Prize.x / machine.A.x, machine.Prize.y / machine.A.y), 100);
        var maxB = Math.Min(Math.Min(machine.Prize.x / machine.B.x, machine.Prize.y / machine.B.y), 100);
        int? min = null;
        for (var a = maxA; a >= 0; a--)
        {
            var b = FindB(machine, a, maxB);
            if (b is not null && (min is null || 3 * a + b < min))
                min = 3 * a + b;
        }

        return min;
    }

    private static int? FindB(Machine machine, int a, int maxB)
    {
        var aX = a * machine.A.x;
        var aY = a * machine.A.y;
        
        for (var b = 0; b < maxB; b++)
        {
            var position = new Position(aY + b * machine.B.y, aX + b * machine.B.x);
            if (position.x > machine.Prize.x || position.y > machine.Prize.y)
                break;

            if (position.x == machine.Prize.x && position.y == machine.Prize.y)
                return b;
        }

        return null;
    }

    public override long Part2()
    {
        return 0;
    }
}