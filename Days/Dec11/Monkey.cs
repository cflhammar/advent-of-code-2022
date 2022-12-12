namespace aoc_2022.Days.Dec11;

public class Monkey
{
    public List<long> Items;
    public string OperationSign;
    public string OperationVal;
    public long Divider; 
    public int TrueThrowTo ;
    public int FalseThrowTo ;
    public long Inspected;
    
    public Monkey(List<string> inpu)
    {
        Items = inpu[1].Replace("Starting items: ", "").Split(", ").Select(long.Parse).ToList();
        var op = inpu[2].Replace(" Operation: new = old ", "").Split(" ");
        OperationSign = op.ElementAt(1);
        OperationVal = op.Last();
        Divider = long.Parse(inpu[3].Replace("Test: divisible by ", ""));
        TrueThrowTo = Int32.Parse( inpu[4].Split("monkey ").Last());
        FalseThrowTo = Int32.Parse( inpu[5].Split("monkey ").Last());
    }
}