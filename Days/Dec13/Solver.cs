using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec13;

public class Solver : ISolver
{
    public string Date { get; } = "Dec13";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var v = new DistressSignal();
        Console.WriteLine("Part 1: Test: "+v.GetPacketsInCorrectOrder(testInput) + " (13)");
        Console.WriteLine("Part 1: "+v.GetPacketsInCorrectOrder(input));
        
        Console.WriteLine("Part 2: Test: "+v.SortSignals(testInput) + " (140)");
        Console.WriteLine("Part 2: "+v.SortSignals(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        var t2 = reader.SplitByEmptyRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByRow(t2);
        
        return t3;
    }
}