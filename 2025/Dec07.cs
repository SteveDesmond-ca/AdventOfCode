using VTSV.AdventOfCode.Core;

internal sealed class Dec07 : Puzzle
{
    public override long Part1()
    {
        var lines = Data.Split('\n');
        var beams = new HashSet<int> { lines[0].IndexOf('S') };
        var numSplits = 0;
        foreach (var line in lines[1..])
        {
            var splits = GetSplits(line);

            foreach (var split in splits)
            {
                if (beams.Remove(split))
                {
                    beams.Add(split - 1);
                    beams.Add(split + 1);
                    numSplits++;
                }
            }
        }

        return numSplits;
    }

    private static int[] GetSplits(string line)
    {
        return line.Select((c, i) => c is '^' ? i : (int?)null)
            .Where(i => i is not null)
            .Cast<int>()
            .ToArray();
    }

    public override long Part2()
    {
        var lines = Data.Split('\n');
        var beams = new long[lines[0].Length];
        beams[lines[0].IndexOf('S')] = 1;
        
        foreach (var line in lines[1..])
        {
            var splits = GetSplits(line);
            
            foreach (var split in splits)
            {
                if (beams[split] == 0)
                    continue;
                
                beams[split - 1] += beams[split];
                beams[split + 1] += beams[split];
                beams[split] = 0;
            }
        }

        return beams.Sum();
    }
}