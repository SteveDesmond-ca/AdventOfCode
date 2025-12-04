using System.IO.Abstractions;

namespace VTSV.AdventOfCode.Core;

public abstract class Puzzle
{
    protected readonly string Data; 
    
    protected Puzzle() : this(new FileSystem())
    {
    }

    private Puzzle(IFileSystem fs)
    {
        var filename = $"{GetType().Name}.txt";
        if (fs.File.Exists(filename))
            Data = fs.File.ReadAllText(filename).TrimEnd('\n');
    }

    public bool HasData() => !string.IsNullOrWhiteSpace(Data);
    
    public abstract long Part1();
    public abstract long Part2();
}