internal abstract class Puzzle
{
    protected readonly string Data; 
    
    protected Puzzle()
    {
        Data = File.ReadAllText($"{GetType().Name}.txt");
    }
    
    public abstract int Part1();
    public abstract int Part2();
}