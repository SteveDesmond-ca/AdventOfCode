using System.Numerics;
using VTSV.AdventOfCode.Core;

internal sealed class Dec08 : Puzzle
{
    public override long Part1()
    {
        var boxes = GetBoxes();
        var sorted = GetDistances(boxes)
            .OrderBy(d => d.Value)
            .Select(d => d.Key)
            .Take(1000)
            .ToArray();

        var circuits = new List<HashSet<int>>();
        foreach (var (x, y) in sorted)
            AddCircuits(circuits, x, y);

        return circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1, (prev, curr) => prev * curr.Count);
    }

    private Vector3[] GetBoxes()
    {
        return Data.Split('\n')
            .Select(Box)
            .OrderBy(b => b.Length())
            .ToArray();
    }

    private static Vector3 Box(string line)
    {
        var coords = line.Split(',').Select(int.Parse).ToArray();
        return new Vector3(coords[0], coords[1], coords[2]);
    }

    private static Dictionary<(int x, int y), float> GetDistances(Vector3[] boxes)
    {
        var distances = new Dictionary<(int x, int y), float>();
        for (var x = 0; x < boxes.Length; x++)
        {
            for (var y = x + 1; y < boxes.Length; y++)
            {
                distances[(x, y)] = (boxes[x] - boxes[y]).Length();
            }
        }

        return distances;
    }

    private static void AddCircuits(List<HashSet<int>> circuits, int x, int y)
    {
        var matches = circuits.Where(c => c.Contains(x) || c.Contains(y)).ToArray();
        switch (matches.Length)
        {
            case 0:
                circuits.Add([x, y]);
                break;
            case 1:
                matches[0].Add(x);
                matches[0].Add(y);
                break;
            default:
                Join(circuits, matches);
                break;
        }
    }

    private static void Join(List<HashSet<int>> circuits, HashSet<int>[] matches)
    {
        foreach (var box in matches[1])
            matches[0].Add(box);

        circuits.Remove(matches[1]);
    }

    public override long Part2()
    {
        var boxes = GetBoxes();
        var sorted = GetDistances(boxes)
            .OrderBy(d => d.Value)
            .Select(d => d.Key)
            .ToArray();

        var circuits = new List<HashSet<int>>();
        foreach (var (x, y) in sorted)
        {
            AddCircuits(circuits, x, y);

            if (circuits is [{ Count: 1000 }])
                return (long)(boxes[x].X * boxes[y].X);
        }

        throw new Exception("Nothing found!");
    }
}