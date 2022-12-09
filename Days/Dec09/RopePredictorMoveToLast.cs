namespace aoc_2022.Days.Dec09;

public class RopePredictor2
{
    List<Coordinate> Knots = new();
    private List<Coordinate> KnotsPrev;

    Dictionary<string, int> visited = new Dictionary<string, int>();

    public RopePredictor2(int knots)
    {
        for (int i = 0; i <= knots; i++) Knots.Add(new Coordinate());
    }
    
    // This only works for part 1, do not use med
    public int MoveHead(List<List<string>> input)
    {
        foreach (var moveHead in input)
        {
            for (int i = 0; i < Int32.Parse(moveHead[1]); i++)
            {
                KnotsPrev = Knots.Select(c => new Coordinate() {X = c.X, Y = c.Y}).ToList();

                switch (moveHead[0])
                {
                    case "U":
                        Knots[0].Y++;
                        break;
                    case "D":
                        Knots[0].Y--;
                        break;
                    case "R":
                        Knots[0].X++;
                        break;
                    case "L":
                        Knots[0].X--;
                        break;
                }
                MoveAllKnots();
            }
        }
        return visited.Count;
    }

    public void MoveAllKnots()
    {
        for (int i = 1; i < Knots.Count; i++)
        {

            if (Math.Sqrt(Math.Pow(Knots[i].X-Knots[i-1].X,2) + Math.Pow(Knots[i].Y-Knots[i-1].Y,2)) > Math.Sqrt(2)) Knots[i] = KnotsPrev[i-1];

            if (i == Knots.Count-1)
            {
                var pos = Knots.Last().X + "." + Knots.Last().Y;
                if (visited.ContainsKey(pos)) visited[pos]++;
                else visited.Add(pos,1);
            }
        }
    }
    
    
}