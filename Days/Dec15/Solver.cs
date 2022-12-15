using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec15;

public class Solver : ISolver
{
    public string Date { get; } = "Dec15";
    
    public void Solve()
    {
        var testInput = ParseInput("input");
        //var input = ParseInput("input");

        var s = new Sensors(testInput);

       // Console.WriteLine(s.NumberOfNoBeaconPosAtRow(10));
      //  Console.WriteLine(s.NumberOfNoBeaconPosAtRow(2000000));
        s.FindBeaconOnEdge();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t = reader.SplitByRow(temp);
        var t2 = t.Select(x => x.Split(":").ToList()).ToList();

        return t2;
    }
}