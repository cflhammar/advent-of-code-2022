using aoc_2022.Days.Dec02.InputData;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec02;

public class Solver : ISolver
{
    public string Date { get; } = "Dec02";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var a = new RockPaperScissor();

        Console.WriteLine("Part 1: Test: " + a.CalculateScore(testInput) + " (15)");
        Console.WriteLine("Part 1: " + a.CalculateScore(input));
        
        Console.WriteLine("Part 2: Test:" + a.Predictor(testInput) + " (12)");
        Console.WriteLine("Part 2: " + a.Predictor(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var temp2 = reader.SplitByRow(temp);
        var temp3 = reader.SplitListOfStringToListListOfStringByDelimeter(temp2, " ");
        
        return temp3;
    }

}