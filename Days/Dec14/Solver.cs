using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec14;

public class Solver : ISolver
{
    public string Date { get; } = "Dec14";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        //var input = ParseInput("input");
        
        Console.WriteLine();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return temp;
    }
}