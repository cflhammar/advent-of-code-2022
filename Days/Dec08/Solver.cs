using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec08;

public class Solver : ISolver
{
    public string Date { get; } = "Dec08";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var testForest = new Forest(testInput);
        var forest = new Forest(input);
        
        var testScore = testForest.CountVisibleTrees();
        var score = forest.CountVisibleTrees();
        
        Console.WriteLine("Part 1: Test: " + testScore.totalVisible + " (21)");
        Console.WriteLine("Part 1: " + score.totalVisible);
        
        Console.WriteLine("Part 2: Test: " + testScore.scenicScore + " (8)");
        Console.WriteLine("Part 2: " + score.scenicScore);
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfCharByNoDelimeter(t2);

        return t3.Select(x=>x.Select(p=> Int32.Parse(p.ToString())).ToList()).ToList();
    }
}