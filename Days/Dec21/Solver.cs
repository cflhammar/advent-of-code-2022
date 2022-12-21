using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec21;

public class Solver : ISolver
{
    public string Date { get; } = "Dec21";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var mgo = new MonkeyGraphOperator();
        
        Console.WriteLine("Part1: Test: " + mgo.Calculate(testInput) + " -> 152");
        Console.WriteLine("Part1: " + mgo.Calculate(input));
        
        Console.WriteLine("Part2: Test: " + mgo.SearchForHumn(testInput) + " -> 301");
        //Console.WriteLine("Part2: " + mgo.SearchForHumn(input));
        
        Console.WriteLine();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, ": ");
        return t3;
    }
}

//1123791725