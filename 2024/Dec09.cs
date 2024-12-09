internal sealed class Dec09 : Puzzle
{
    public override long Part1()
    {
        var map = Data.Select(c => byte.Parse(c.ToString())).ToArray();
        var drive = MapDrive(map);
        Compress(drive);
        return Checksum(drive);
    }

    private static long[] MapDrive(byte[] map)
    {
        var drive = new long[map.Sum(m => m)];
        var pos = 0;
        for (var x = 0; x < map.Length; x++)
        {
            var isFile = x % 2 == 0;
            var fileID = x / 2;
            for (var y = 0; y < map[x]; y++)
            {
                drive[pos++] = isFile ? fileID : -1;
            }
        }

        return drive;
    }

    private static void Compress(long[] drive)
    {
        var lastDataBlock = drive.Length - 1;
        for (var x = 0; x <= lastDataBlock; x++)
        {
            if (drive[x] > -1)
                continue;

            while (drive[lastDataBlock] == -1)
                lastDataBlock--;

            if (lastDataBlock <= x)
                return;

            drive[x] = drive[lastDataBlock];
            drive[lastDataBlock] = -1;
        }
    }

    private static long Checksum(long[] drive)
    {
        long checksum = 0;
        for (var x = 1; x < drive.Length; x++)
        {
            if (drive[x] == -1)
                continue;

            checksum += x * drive[x];
        }

        return checksum;
    }

    private static void Print(long[] drive)
        => Console.WriteLine(string.Join(" ", drive.Select(d => d > -1 ? d.ToString() : ".")));

    public override long Part2()
    {
        var map = Data.Select(c => byte.Parse(c.ToString())).ToArray();
        var drive = MapDrive(map);
        Defrag(drive, map);
        return Checksum(drive);
    }

    private static void Defrag(long[] drive, byte[] map)
    {
        for (var fileID = map.Length / 2; fileID > 0; fileID--)
        {
            var end = drive.AsSpan().IndexOf(fileID);
            var length = GetFileLength(drive[end..], fileID);
            var start = FindFirstGap(drive[..end], length);

            if (start >= end)
                continue;

            for (var y = 0; y < length; y++)
            {
                drive[start + y] = fileID;
                drive[end + y] = -1;
            }
        }
    }

    private static int GetFileLength(long[] span, int fileID)
    {
        var x = 0;
        while (x < span.Length && span[x] == fileID)
            x++;
        return x;
    }

    private static int FindFirstGap(long[] drive, int fileLength)
    {
        var start = 0;
        while (start < drive.Length && (drive[start] > -1 || GetGapLength(drive, start) < fileLength))
            start++;
        return start;
    }

    private static int GetGapLength(long[] drive, int start)
    {
        var length = 0;
        while (start + length < drive.Length && drive[start + length] == -1)
            length++;
        return length;
    }
}