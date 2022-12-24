using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec24;

public class Solver : ISolver
{
    public string Date { get; } = "Dec24";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var b = new Blizzard(input);
        b.ThereAndBackAgainAndAgain();
        
        
        
        Console.WriteLine();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        
        return t2;
    }
}