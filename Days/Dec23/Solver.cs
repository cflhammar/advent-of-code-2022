using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec23;

public class Solver : ISolver
{
    public string Date { get; } = "Dec23";
    
    public void Solve()
    {
        var testInput = ParseInput("test2");
        var input = ParseInput("input");

        var esTest = new ElfSpread(testInput);
        var es = new ElfSpread(input);
        
        Console.WriteLine("Part 1: Test: " + esTest.PlayRounds(10) + " -> 110");
        Console.WriteLine("Part 1: " + es.PlayRounds(10) );
        
        Console.WriteLine("Part 2: Test:  " + (esTest.PlayUntilStop() + 10) + " -> 20");  // add 10 rounds already played in part1
        Console.WriteLine("Part 2: " + (es.PlayUntilStop() + 10));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t = reader.SplitByRow(temp);

        return t;
    }
}