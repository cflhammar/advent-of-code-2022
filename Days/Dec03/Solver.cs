using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec03;

public class Solver : ISolver
{
    public string Date { get; } = "Dec03";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var bagController = new RucksackCalculator();
        
       Console.WriteLine("Part1: Test: " +   bagController.ScoreOfOverlappingRucksackCompartments(testInput) + " (157)");
       Console.WriteLine("Part 1: " +  bagController.ScoreOfOverlappingRucksackCompartments( input));
       
       Console.WriteLine("Part 2: Test: " +   bagController.ScoreOfBagGroup(testInput) + " (70)");
       Console.WriteLine("Part 2: " +  bagController.ScoreOfBagGroup(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return reader.SplitByRow(temp);
    }
}