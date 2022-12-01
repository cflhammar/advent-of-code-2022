using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Template;

public class Solver : ISolver
{
    public string Date { get; } = "XXXXXX";
    
    public void Solve()
    {
        throw new NotImplementedException();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return temp;
    }
}