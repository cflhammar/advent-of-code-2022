using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec25;

public class Solver : ISolver
{
    public string Date { get; } = "Dec25";

    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var sc = new SnafuConverter();
        
        Console.WriteLine(sc.DecimalToSnafuByIteration(sc.SumSnafuNumbers(testInput)) + "  ->  2=-1=0" );
        Console.WriteLine(sc.DecimalToSnafuByIteration(sc.SumSnafuNumbers(input)));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        
        return reader.SplitByRow(temp);
    }
}