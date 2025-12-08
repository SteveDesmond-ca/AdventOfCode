using VTSV.AdventOfCode.Core;

internal sealed class Dec06 : Puzzle
{
    private enum Operation
    {
        Addition,
        Multiplication
    }

    private record struct Problem(Operation Operation, long[] Numbers)
    {
        public long Solve()
        {
            return Operation switch
            {
                Operation.Addition => Add(Numbers),
                Operation.Multiplication => Multiply(Numbers),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static long Add(long[] n)
            => n.Sum();

        private static long Multiply(long[] n)
            => n.Aggregate<long,long>(1, (prev, curr) => prev * curr);
    }

    public override long Part1()
    {
        var lines = Data.Split('\n').Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();
        var operations = lines[^1].Select(o => o == "+" ? Operation.Addition : Operation.Multiplication).ToArray();
        var problems = operations.Select((o, i) => new Problem(o,
                [long.Parse(lines[0][i]), long.Parse(lines[1][i]), long.Parse(lines[2][i]), long.Parse(lines[3][i])]))
            .ToArray();
        var solutions = problems.Select(p => p.Solve());
        return solutions.Sum();
    }

    public override long Part2()
    {
        var lines = Data.Split('\n');
        var splits = lines[^1].Select((c, i) => c is not ' ' ? i : (int?)null)
            .Where(i => i is not null)
            .Cast<int>()
            .ToArray();

        long total = 0;
        for (var x = 1; x <= splits.Length; x++)
        {
            var start = splits[x-1];
            var end = x < splits.Length ? splits[x] : lines[^1].Length + 1;
            var current = end - 2;
            var nums = new List<long>();
            while (current >= start)
            {
                var num = long.Parse($"{lines[0][current]}{lines[1][current]}{lines[2][current]}{lines[3][current]}");
                nums.Add(num);
                current--;
            }

            var op = lines[^1][start] == '+' ? Operation.Addition : Operation.Multiplication;
            var problem = new Problem(op, nums.ToArray());
            total += problem.Solve();
        }
        return total;
    }
}