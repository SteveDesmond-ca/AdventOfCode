internal sealed class Dec07 : Puzzle
{
    public override long Part1()
    {
        var lines = Data.Split("\n").ToArray();
        var equations = lines.Select(l => l.Split(':'))
            .ToDictionary(p => long.Parse(p[0]), p => p[1].Trim().Split(' ').Select(long.Parse).ToArray());

        long sum = 0;
        foreach (var (solution, operands) in equations)
        {
            var subtotals = new List<long> { operands[0] };
            for (var x = 1; x < operands.Length; x++)
            {
                var subtotalsCount = subtotals.Count;
                for (var y = 0; y < subtotalsCount; y++)
                {
                    var subtotal = subtotals[y];
                    subtotals[y] = subtotal + operands[x];
                    subtotals.Add(subtotal * operands[x]);
                }
            }

            sum += subtotals.FirstOrDefault(sub => sub == solution);
        }

        return sum;
    }

    private static long Concat(long left, long right)
        => right switch
        {
            < 10L => 10L * left + right,
            < 100L => 100L * left + right,
            _ => 1000L * left + right
        };

    public override long Part2()
    {
        var lines = Data.Split("\n").ToArray();
        var equations = lines.Select(l => l.Split(':'))
            .ToDictionary(p => long.Parse(p[0]), p => p[1].Trim().Split(' ').Select(long.Parse).ToArray());

        long sum = 0;
        foreach (var (solution, operands) in equations)
        {
            var subtotals = new List<long> { operands[0] };
            for (var x = 1; x < operands.Length; x++)
            {
                var subtotalsCount = subtotals.Count;
                for (var y = 0; y < subtotalsCount; y++)
                {
                    var subtotal = subtotals[y];
                    subtotals[y] = subtotal + operands[x];
                    subtotals.Add(subtotal * operands[x]);
                    subtotals.Add(Concat(subtotal, operands[x]));
                }
            }

            sum += subtotals.FirstOrDefault(sub => sub == solution);
        }

        return sum;
    }
}