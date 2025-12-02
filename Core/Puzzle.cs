using System.IO;

namespace VTSV.AdventOfCode.Core;

public abstract class Puzzle
{
    protected readonly string Data; 
    
    protected Puzzle()
    {
        Data = File.ReadAllText($"{GetType().Name}.txt").TrimEnd('\n');
    }
    
    public abstract long Part1();
    public abstract long Part2();
}