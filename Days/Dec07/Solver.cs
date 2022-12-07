using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec07;

public class Solver : ISolver
{
    public string Date { get; } = "Dec07";
    
    public void Solve()
    { 
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var fs = new FileSystem();
        fs.CreateFileSystem(testInput);
    
        Console.WriteLine("Part 1: Test: "+fs.SumOfAllDirsBelowSize(100000) + " (95437)");
        Console.WriteLine("Part 2: Test: "+fs.FreeUpSpace(30000000) + " (24933642)");
        
        fs = new FileSystem();
        fs.CreateFileSystem(input);
        
        Console.WriteLine("Part 1: "+fs.SumOfAllDirsBelowSize(100000));
        Console.WriteLine("Part 2: "+fs.FreeUpSpace(30000000));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return reader.SplitByRow(temp);
    }
}