using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec18;

public class Solver : ISolver
{
    public string Date { get; } = "Dec18";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var testLw = new LavaInWater(testInput);
        var lw = new LavaInWater(input);
        
        Console.WriteLine("Part 1: Test:  " + testLw.FreeSides() + " (64)");
        Console.WriteLine("Part 1: " + lw.FreeSides());
       
        Console.WriteLine("Part 2: Test:  " + testLw.WaterArea() + " (58)");
        Console.WriteLine("Part 2: " + lw.WaterArea());
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, ",");

        return t3;
    }
}