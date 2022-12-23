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

        var es = new ElfSpread(testInput);
        Console.WriteLine(es.PlayRounds(10) + " -> 110");
        es = new ElfSpread(testInput);
        Console.WriteLine(es.PlayUntilStop() + " -> 20");
        
        es = new ElfSpread(input);
        Console.WriteLine(es.PlayRounds(10) + " -> 4181");
       // es = new ElfSpread(testInput);
       // Console.WriteLine(es.PlayUntilStop() + " -> 20");
        
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t = reader.SplitByRow(temp);

        return t;
    }
}