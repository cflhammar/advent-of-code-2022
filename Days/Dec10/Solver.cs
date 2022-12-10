using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec10;

public class Solver : ISolver
{
    public string Date { get; } = "Dec10";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var gpu = new Gpu();
        
        Console.WriteLine("Part 1: Test: " + gpu.FollowProgram(testInput) + " (13140)");
        Console.WriteLine("Part 1: " + gpu.FollowProgram(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return reader.SplitByRow(temp);
    }
}