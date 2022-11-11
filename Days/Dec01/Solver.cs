using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec01;

public class Solver : ISolver
{
    public string Date { get; } = "Dec01";
    
    public void Solve()
    {
        var data = ParseInput("test1");
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();

        var temp = reader.GetFileContent(Date,fileName);
        
        return temp;
    }
}