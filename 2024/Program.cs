using System.Diagnostics;
using System.Reflection;

var timer = Stopwatch.StartNew();

var puzzles = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Puzzle)))
    .Where(t => args.Length == 0 || t.Name == $"Dec{args[0]}")
    .Select(Activator.CreateInstance).Cast<Puzzle>().ToArray();

Console.WriteLine($"Found {puzzles.Length} puzzles ({timer.ElapsedMilliseconds}ms)");

foreach (var puzzle in puzzles)
{
    Console.WriteLine();

    timer.Restart();
    Console.WriteLine($"{puzzle.GetType().Name} Part 1: {puzzle.Part1()} ({timer.ElapsedMilliseconds}ms)");

    timer.Restart();
    Console.WriteLine($"{puzzle.GetType().Name} Part 2: {puzzle.Part2()} ({timer.ElapsedMilliseconds}ms)");
}