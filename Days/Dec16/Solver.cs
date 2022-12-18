using System;
using System.Text.RegularExpressions;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec16;

public class Solver : ISolver
{
    public string Date { get; } = "Dec16";
    
    public void Solve()
    {
        var testInput = ParseInput("test1"); 
        var input = ParseInput("input");
       
       var cs = new Valves(testInput);
       Console.WriteLine("Part 1: Test: " +  cs.GetMaxFlowRate() + " -> 1651");

       cs = new Valves(input);
       Console.WriteLine("Part 1: " + cs.GetMaxFlowRate() + " -> 1792");


       cs = new Valves(testInput, 2);
       Console.WriteLine("Part 2: Test: " + cs.GetMaxFlowRate() + " -> 1707");

       
       cs = new Valves(input, 2);
       Console.WriteLine("Part 2: " + cs.GetMaxFlowRate() + "");

    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        var t2 = reader.SplitByRow(temp);

        Regex r = new Regex("; tunnel.? lead.? to valve.? ");
        var t3 = t2.Select(x => r.Split(x).ToList()).ToList();
        
        return t3;
    }
}