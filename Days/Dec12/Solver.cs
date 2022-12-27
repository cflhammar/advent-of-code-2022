using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec12;

public class Solver : ISolver
{
    public string Date { get; } = "Dec12";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var m = new Hill();
        
        Console.WriteLine("Part 1: Test: + " + m.FindShortestPath(testInput) + " ->  31" );
        Console.WriteLine("Part 1 + " + m.FindShortestPath(input));
        
        Console.WriteLine("Part 2: Test: + " + m.FindShortestPath(testInput, true) + " ->  29" );
        Console.WriteLine("Part 2 + " + m.FindShortestPath(input, true));
        
        
 
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfCharByNoDelimeter(t2);

      //  var t4 = t3.Select(x => x.Select(a => (int) a).ToList()).ToList();

        return t3;
    }
}