using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec09;

public class Solver : ISolver
{
    public string Date { get; } = "Dec09";
    
    public void Solve()
    {
        var testInput1 = ParseInput("test1");
        var testInput2 = ParseInput("test2");
        var input = ParseInput("input");

        var rp = new RopePredictor(1);
        Console.WriteLine(" Part 1: Test:" + rp.MoveHead(testInput1) + " (13)");
        
        rp = new RopePredictor(1);
        Console.WriteLine(" Part 1: " + rp.MoveHead(input));
        
        rp = new RopePredictor(9);
        Console.WriteLine(" Part 2: Test:" + rp.MoveHead(testInput2) + " (36)");
        
        rp = new RopePredictor(9);
        Console.WriteLine(" Part 2: " + rp.MoveHead(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        
        return reader.SplitListOfStringToListListOfStringByDelimeter(t2, " ");
    }
}