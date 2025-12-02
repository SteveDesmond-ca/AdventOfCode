using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace VTSV.AdventOfCode.Core;

public class App
{
    public void Run(string arg)
    {
        var timer = Stopwatch.StartNew();

        var puzzles = Assembly.GetEntryAssembly()!.GetTypes().Where(t => t.IsSubclassOf(typeof(Puzzle)))
            .Where(t => arg is null || t.Name == $"Dec{arg}")
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
    }
}