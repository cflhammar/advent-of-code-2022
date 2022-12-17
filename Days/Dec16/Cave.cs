namespace aoc_2022.Days.Dec16;

public class Cave
{
    public string Name;
    public int Pressure;
    public List<string> ConnectedCaves;
    public bool IsOpen = false;
    public Dictionary<string, int> paths = new();

}