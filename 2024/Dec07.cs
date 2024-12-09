internal sealed class Dec07 : Puzzle
{
    private enum Operators
    {
        Plus = 0,
        Times = 1,
        Concatenate = 2
    }

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
                    subtotals[y] = DoMath(subtotal, Operators.Plus, operands[x]);
                    subtotals.Add(DoMath(subtotal, Operators.Times, operands[x]));
                }
            }

            if (subtotals.Any(sub => sub == solution))
                sum += solution;
        }

        return sum;
    }

    private static long DoMath(long left, Operators op, long right)
        => op switch
        {
            Operators.Plus => left + right,
            Operators.Times => left * right,
            Operators.Concatenate => long.Parse(left.ToString() + right),
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
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
                    subtotals[y] = DoMath(subtotal, Operators.Plus, operands[x]);
                    subtotals.Add(DoMath(subtotal, Operators.Times, operands[x]));
                    subtotals.Add(DoMath(subtotal, Operators.Concatenate, operands[x]));
                }
            }

            if (subtotals.Any(sub => sub == solution))
                sum += solution;
        }

        return sum;
    }
}