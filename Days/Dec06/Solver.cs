using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec06;

public class Solver : ISolver
{
    public string Date { get; } = "Dec06";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

      var sp = new SubPacket();

      Console.WriteLine(sp.FindOccurence(testInput, 4));
      Console.WriteLine(sp.FindOccurence(input, 4));
      
      Console.WriteLine(sp.FindOccurence(testInput, 14));
      Console.WriteLine(sp.FindOccurence(input, 14));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return temp;
    }
}