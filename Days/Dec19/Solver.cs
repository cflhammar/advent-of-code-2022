using System;
using System.Text.RegularExpressions;
using System.Windows.Markup;
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
        Console.WriteLine("Part 1: Test:" + f.FindAll(testInput, 24) + "-> 33");
        Console.WriteLine("Part 1: " + f.FindAll(input, 24));
        
        Console.WriteLine("Part 2" + f.FindThree(input, 32));

        Console.WriteLine();
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
             
                 Int32.Parse(r.Groups[1].Value),
                 Int32.Parse(r.Groups[2].Value),
                (Int32.Parse(r.Groups[3].Value),
                 Int32.Parse(r.Groups[4].Value)),
                 (Int32.Parse(r.Groups[5].Value),
                 Int32.Parse(r.Groups[6].Value))
         ));
        }
        
        
        return robots;
    }
}