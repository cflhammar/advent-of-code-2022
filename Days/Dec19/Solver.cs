using System.Text.RegularExpressions;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec19;

public class Solver : ISolver
{
    public string Date { get; } = "Dec19";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var f = new MiningRobots(); 
        Console.WriteLine("Part 1: Test: " + f.FindBest(testInput, 24).Item1 + " -> 33");
        Console.WriteLine("Part 1: " + f.FindBest(input, 24).Item1);
        
        //Console.WriteLine("Part 2: Test: " + f.FindBest(testInput, 32, 3).Item2 + " -> 62");
        Console.WriteLine("Part 2: " + f.FindBest(input, 32, 3).Item2);
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        Regex rgx = new Regex(
            "Blueprint .+: Each ore robot costs (\\d+) ore. Each clay robot costs (\\d+) ore. Each obsidian robot costs (\\d+) ore and (\\d+) clay. Each geode robot costs (\\d+) ore and (\\d+) obsidian");

        var robots = new List<(int ore, int clay, (int ore, int caly) obsidian, (int ore, int obsidian) genode)>();

        foreach (string s in t2)
        {
            var r = rgx.Match(s);
            
             robots.Add((
                 int.Parse(r.Groups[1].Value),
                 int.Parse(r.Groups[2].Value),
                (int.Parse(r.Groups[3].Value),
                 int.Parse(r.Groups[4].Value)),
                 (int.Parse(r.Groups[5].Value),
                 int.Parse(r.Groups[6].Value))
         ));
        }
        
        return robots;
    }
}