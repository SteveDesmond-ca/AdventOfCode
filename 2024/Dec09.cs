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

    public override long Part2()
    {
        var map = Data.Select(c => byte.Parse(c.ToString())).ToArray();
        var drive = MapDrive(map);
        Defrag(drive, map);
        return Checksum(drive);
    }

    private static void Defrag(long[] drive, byte[] map)
    {
        var firstGap = 0;
        for (var fileID = map.Length / 2; fileID > 0; fileID--)
        {
            FastForward(drive, ref firstGap);
            var end = drive.AsSpan().IndexOf(fileID);
            var length = GetFileLength(drive, end, fileID);
            var start = FindFirstGap(drive, firstGap, end, length);

            if (start >= end)
                continue;

            for (var y = 0; y < length; y++)
            {
                drive[start + y] = fileID;
                drive[end + y] = -1;
            }
        }
    }

    private static void FastForward(long[] drive, ref int firstGap)
    {
        while (!IsGap(drive, firstGap))
            firstGap++;
    }

    private static int GetFileLength(long[] drive, int start, int fileID)
    {
        var length = 0;
        while (start + length < drive.Length && drive[start + length] == fileID)
            length++;
        return length;
    }

    private static int FindFirstGap(long[] drive, int start, int end, int fileLength)
    {
        while (start < end && (!IsGap(drive, start) || !CanMoveFileTo(drive, start, fileLength)))
            start++;
        return start;
    }

    private static bool CanMoveFileTo(long[] drive, int start, int fileLength)
    {
        var length = 0;
        while (start + length < drive.Length && drive[start + length] == -1)
        {
            length++;
            if (length >= fileLength)
                return true;
        }
        return false;
    }

    private static bool IsGap(long[] drive, int position)
    {
        return drive[position] == -1;
    }
}