using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec22;

public class Solver : ISolver
{
    public string Date { get; } = "Dec22";
    
    public void Solve()
    {
        var testInput = ParseInput("test2");
        var input = ParseInput("input");

        var mm = new MonkeyMap(testInput.Item1, testInput.Item2);
        Console.WriteLine("Part 1: Test: " + mm.FollowInstructions() + " -> 6032");
        
        mm = new MonkeyMap(input.Item1, input.Item2);
        Console.WriteLine("Part 1: " + mm.FollowInstructions());
        
        var tcmm = new TestCubicMonkeyMap(testInput.Item1, testInput.Item2);
        Console.WriteLine("Part 2: Test: " + tcmm.FollowInstructions() + " -> 5031");
        
        var cmm = new CubicMonkeyMap(input.Item1, input.Item2);
        Console.WriteLine("Part 2: " + cmm.FollowInstructions());
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t = reader.SplitByEmptyRow(temp);
        
        var mapRaw = reader.SplitByRow(t[0]);
        var maxRowLength = mapRaw.Select(x => x.Length).Max();

        // add padding end of rows
        for (int row = 0; row < mapRaw.Count(); row++)
        {
            var rowMax = mapRaw[row].Length;
            for (int i = 0; i < maxRowLength - rowMax; i++)
            {
                mapRaw[row] += " ";
            }
        }
        
        var t3 = reader.SplitListOfStringToListListOfCharByNoDelimeter(mapRaw);

        return (t3, t[1]);
    }
}