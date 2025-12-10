using VTSV.AdventOfCode.Core;

internal sealed class Dec04 : Puzzle
{
    public override long Part1()
    {
        var map = Data.Split('\n').Select(m => m.ToArray()).ToArray();
        return GetAccessible(map).Count;
    }

    private static List<Position> GetAccessible(char[][] map)
    {
        var maxX = map[0].Length;
        var maxY = map.Length;
        var accessible = new List<Position>();
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                var pos = (y, x);
                if (IsAccessible(pos, map))
                    accessible.Add(pos);
            }
        }

        return accessible;
    }

    private static bool IsAccessible(Position p, char[][] map)
    {
        if (map[p.y][p.x] == '.')
            return false;
                
        var maxX = map[0].Length;
        var maxY = map.Length;
        
        var adjacent = (p.y > 0 && p.x > 0 && map[p.y - 1][p.x - 1] == '@' ? 1 : 0)
                       + (p.y > 0 && map[p.y - 1][p.x] == '@' ? 1 : 0)
                       + (p.y > 0 && p.x < maxX - 1 && map[p.y - 1][p.x + 1] == '@' ? 1 : 0)
                       + (p.x > 0 && map[p.y][p.x - 1] == '@' ? 1 : 0)
                       + (p.x < maxX - 1 && map[p.y][p.x + 1] == '@' ? 1 : 0)
                       + (p.y < maxY - 1 && p.x > 0 && map[p.y + 1][p.x - 1] == '@' ? 1 : 0)
                       + (p.y < maxY - 1 && map[p.y + 1][p.x] == '@' ? 1 : 0)
                       + (p.y < maxY - 1 && p.x < maxX - 1 && map[p.y + 1][p.x + 1] == '@' ? 1 : 0);

        return adjacent < 4;
    }

    public override long Part2()
    {
        var map = Data.Split('\n').Select(m => m.ToArray()).ToArray();
        var accessible = GetAccessible(map);
        var removed = 0;

        do
        {
            Remove(accessible, map);
            removed += accessible.Count;
            accessible = GetAccessible(map);
        } while (accessible.Count > 0);

        return removed;
    }

    private static void Remove(List<Position> accessible, char[][] map)
    {
        foreach (var pos in accessible)
            map[pos.y][pos.x] = '.';
    }
}