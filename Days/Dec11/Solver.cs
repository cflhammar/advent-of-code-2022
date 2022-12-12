using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec11;

public class Solver : ISolver
{
    public string Date { get; } = "Dec11";
    
    public void Solve()
    {
        var testInput = ParseInput("input");
        //var input = ParseInput("input");
        var mb = new MonkeyBusiness(testInput);
        mb.CalculateMonkeyBusiness();
        
        Console.WriteLine();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByEmptyRow(temp);

        var dict = new Dictionary<int, Monkey>();
        
        var t3 = t2.Select(x => new Monkey(reader.SplitByRow(x))).ToList();

        return t3;
    }
}