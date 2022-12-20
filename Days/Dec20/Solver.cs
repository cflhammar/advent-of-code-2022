using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec20;

public class Solver : ISolver
{
    public string Date { get; } = "Dec20";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var md = new MixingDecryption();
        Console.WriteLine("Part 1: Test: " + md.Decrypt(testInput, 1) + " -> 3");
        Console.WriteLine("Part 1: " + md.Decrypt(input, 1));
        Console.WriteLine("Part 2: Test: " + md.Decrypt(testInput, 10, 811589153) + " -> 1623178306");
        Console.WriteLine("Part 2: " + md.Decrypt(input, 10, 811589153));
    }
    
    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        
        return reader.SplitByRow(temp).Select(long.Parse).ToList();
    }
}