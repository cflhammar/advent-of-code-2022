using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec17;

public class Solver : ISolver
{
    public string Date { get; } = "Dec17";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var t = new Tetris(testInput);
        Console.WriteLine( "Part 1: Test: "+ t.Play(2022) + " (3068)");
        
        t = new Tetris(input);
        Console.WriteLine( "Part 1: "+ t.Play(2022) + " (3149)");
        
        t = new Tetris(testInput);
        Console.WriteLine( "Part 2: Test: "+ t.Play(1000000000000) + " (1514285714288)");
        
        t = new Tetris(input);
        Console.WriteLine("Part 2: "+ t.Play(1000000000000));
        
        
        Console.WriteLine();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return temp;
    }
}

// 1559960937503
