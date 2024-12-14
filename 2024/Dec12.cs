internal sealed class Dec12 : Puzzle
{
    public override long Part1()
    {
        var map = Data.Split("\n").Select(r => r.ToArray()).ToArray();
        var regions = GetRegions(map);
        return regions.Sum(GetPrice);
    }

    private static int GetPrice(char[,] region)
        => GetArea(region) * GetPerimeter(region);

    private static int GetArea(char[,] region)
    {
        var area = 0;
        for (var y = 0; y < region.GetLength(0); y++)
        {
            for (var x = 0; x < region.GetLength(1); x++)
            {
                if (region[y, x] != default)
                    area++;
            }
        }

        return area;
    }

    private static int GetPerimeter(char[,] region)
    {
        var perimeter = 0;
        var length = region.GetLength(0);
        var width = region.GetLength(1);
        for (var y = 0; y < length; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (region[y, x] == default)
                    continue;

                if (y == 0 || region[y - 1, x] == default)
                    perimeter++;
                if (x == width - 1 || region[y, x + 1] == default)
                    perimeter++;
                if (y == length - 1 || region[y + 1, x] == default)
                    perimeter++;
                if (x == 0 || region[y, x - 1] == default)
                    perimeter++;
            }
        }

        return perimeter;
    }

    private static List<char[,]> GetRegions(char[][] map)
    {
        var regions = new List<char[,]>();
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                if (map[y][x] != default)
                    regions.Add(GetRegion(map, (y, x)));
            }
        }

        return regions;
    }

    private static char[,] GetRegion(char[][] map, Position position)
    {
        var region = new char[map.Length, map[0].Length];
        var letter = map[position.y][position.x];
        ExpandRegion(map, region, letter, position);
        return region;
    }

    private static void ExpandRegion(char[][] map, char[,] region, char letter, Position position)
    {
        if (map[position.y][position.x] != letter || region[position.y, position.x] == letter)
            return;

        region[position.y, position.x] = letter;
        map[position.y][position.x] = default;

        if (position.y > 0)
            ExpandRegion(map, region, letter, (position.y - 1, position.x));
        if (position.x < map[0].Length - 1)
            ExpandRegion(map, region, letter, (position.y, position.x + 1));
        if (position.y < map.Length - 1)
            ExpandRegion(map, region, letter, (position.y + 1, position.x));
        if (position.x > 0)
            ExpandRegion(map, region, letter, (position.y, position.x - 1));
    }

    public override long Part2()
    {
        var map = Data.Split("\n").Select(r => r.ToArray()).ToArray();
        var regions = GetRegions(map);
        return regions.Sum(GetDiscountedPrice);
    }

    private static int GetDiscountedPrice(char[,] region)
    {
        var area = GetArea(region);
        var sides = GetSides(region);
        return area * sides;
    }

    private static void Debug(char[,] region)
    {
        var length = region.GetLength(0);
        var width = region.GetLength(1);
        for (var y = 0; y < length; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.Write(region[y, x] != default ? region[y,x] : '.');
            }

            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private static int GetSides(char[,] region)
    {
        var sides = 0;
        var length = region.GetLength(0);
        var width = region.GetLength(1);
        for (var y = 0; y < length; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (region[y, x] == default)
                    continue;

                if ((y == 0 || region[y - 1, x] == default) && (x == 0 || region[y, x - 1] == default || (y > 0 && region[y - 1, x - 1] != default)))
                    sides++;
                if ((x == width - 1 || region[y, x + 1] == default) && (y == 0 || region[y - 1, x] == default || (x < width - 2 && region[y - 1, x + 1] != default)))
                    sides++;
                if ((y == length - 1 || region[y + 1, x] == default) && (x == width - 1 || region[y, x + 1] == default || (y < length - 2 && region[y + 1, x + 1] != default)))
                    sides++;
                if ((x == 0 || region[y, x - 1] == default) && (y == length - 1 || region[y + 1, x] == default || (x > 0 && region[y + 1, x - 1] != default)))
                    sides++;
            }
        }

        return sides;
    }
}