using System.Reflection;

var puzzles = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Puzzle)))
    .Select(Activator.CreateInstance).Cast<Puzzle>();

foreach (var puzzle in puzzles)
{
    Console.WriteLine(puzzle.GetType().Name + " Part 1: " + puzzle.Part1());
    Console.WriteLine(puzzle.GetType().Name + " Part 2: " +puzzle.Part2());
}