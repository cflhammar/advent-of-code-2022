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
        Console.WriteLine( m.FindShortestPath(testInput));
        Console.WriteLine( m.FindShortestPath(input));
        
 
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