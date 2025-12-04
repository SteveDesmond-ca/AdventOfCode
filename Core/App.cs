using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace VTSV.AdventOfCode.Core;

public static class App
{
    public static void Run(string arg)
    {
        var timer = Stopwatch.StartNew();

        var puzzles = Assembly.GetEntryAssembly()!.GetTypes().Where(t => t.IsSubclassOf(typeof(Puzzle)))
            .Where(t => arg is null || t.Name == $"Dec{arg}")
            .Select(Activator.CreateInstance).Cast<Puzzle>().ToArray();

        Console.WriteLine($"Found {puzzles.Length} puzzles ({timer.ElapsedMilliseconds}ms)");

        foreach (var puzzle in puzzles)
        {
            var name = puzzle.GetType().Name;
            Console.WriteLine();

            if (!puzzle.HasData())
            {
                Console.WriteLine($"{name} has no data");
                continue;
            }

            timer.Restart();
            Console.WriteLine($"{name} Part 1: {puzzle.Part1()} ({timer.ElapsedMilliseconds}ms)");

            timer.Restart();
            Console.WriteLine($"{name} Part 2: {puzzle.Part2()} ({timer.ElapsedMilliseconds}ms)");
        }
    }
}