using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec14;

public class Solver : ISolver
{
    public string Date { get; } = "Dec14";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var cs = new SandCave(testInput);
        Console.WriteLine(cs.PourSandUntilFull(true));
        
       cs = new SandCave(input);
        Console.WriteLine(cs.PourSandUntilFull(false));
        
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, " -> ");
        var t4 = t3.Select(x => x.Select(y => y.Split(",").Select(Int32.Parse).ToList()).ToList()).ToList();
        
        return t4 ;
    }
}