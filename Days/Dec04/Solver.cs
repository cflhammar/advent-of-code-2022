using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec04;

public class Solver : ISolver
{
    public string Date { get; } = "Dec04";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var elfOverlap = new ElfPairOverlap();

        var testResult = elfOverlap.CalculateElfOverlap(testInput);
   //     var result = elfOverlap.CalculateElfOverlap(input);
        
        Console.WriteLine("Part 1: Test: " + testResult.Item1 + " (2)" + "\n" + "Part 2: Test: " + testResult.Item2 + " (4)");
     //   Console.WriteLine("Part 1: " + result.Item1  + "\n" + "Part 2: " + result.Item2 );
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2,",");

        return t3;
    }
}