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
        Console.WriteLine(mm.FollowInstructions() + " -> 6032");
        
        mm = new MonkeyMap(input.Item1, input.Item2);
        Console.WriteLine(mm.FollowInstructions());
        

        var tcmm = new TestFoldCubicMonkeyMap(testInput.Item1, testInput.Item2);
        Console.WriteLine( tcmm.FollowInstructions() + " -> 5031");
        
        var cmm = new CubicMonkeyMap2(input.Item1, input.Item2);
        Console.WriteLine( cmm.FollowInstructions());

    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t = reader.SplitByEmptyRow(temp);
        var t2 = reader.SplitByRow(t[0]);

        var t22 = t2.Select(s => s.Replace(" ", " ")).ToList();
        var max = t2.Select(x => x.Length).Max();
        
        for (int row = 0; row < t22.Count(); row++)
        {
            var rowMax = t22[row].Length;
            for (int i = 0; i < max - rowMax; i++)
            {
                t22[row] += " ";
            }
        }
        
        var t3 = reader.SplitListOfStringToListListOfCharByNoDelimeter(t22);

        return (t3, t[1]);
    }
}