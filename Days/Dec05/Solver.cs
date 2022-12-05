using System.Reflection.Metadata.Ecma335;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec05;

public class Solver : ISolver
{
    public string Date { get; } = "Dec05";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
       var input = ParseInput("input");

        var port = new Port(testInput.Item1 );
        Console.WriteLine("Part 1: Test: " + port.FollowInstructions(testInput.Item2, true) + " (CMZ)");

        port = new Port(input.Item1 );
        Console.WriteLine("Part 1: " + port.FollowInstructions(input.Item2, true));
        
        port = new Port(testInput.Item1 );
        Console.WriteLine("Part 2: Test: " + port.FollowInstructions(testInput.Item2,  false)+ " (MCD)");

        port = new Port(input.Item1 );
        Console.WriteLine("Part 2:" + port.FollowInstructions(input.Item2,  false));

    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByEmptyRow(temp);

        var over = reader.SplitByRow(t2[0]);
        var o2 = over.Select(row => row.Replace("[ ]", "[.]").Replace("[", "").Replace("]", "").Split(" ").ToList()).ToList();
        
        var under = reader.SplitByRow(t2[1]);
        var u2 = under.Select(z=> z.Replace("move ", "").Split(" from").ToList()).ToList();
        //var over2 = over.Select(x => x.Replace("[", "").Replace("]", ""));

        return (o2.SkipLast(1).ToList(), u2);
    }
}