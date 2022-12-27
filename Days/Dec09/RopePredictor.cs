namespace aoc_2022.Days.Dec09;

public class RopePredictor
{
    private List<Coordinate> _knots = new();
    private Dictionary<string, int> _visited = new ();

    public RopePredictor(int knots)
    {
        for (int i = 0; i <= knots; i++) _knots.Add(new Coordinate());
    }
    
    public int MoveHead(List<List<string>> input)
    {
        foreach (var moveHead in input)
        {
            for (int i = 0; i < Int32.Parse(moveHead[1]); i++)
            {
                switch (moveHead[0])
                {
                    case "U":
                        _knots[0].Y++;
                        break;
                    case "D":
                        _knots[0].Y--;
                        break;
                    case "R":
                        _knots[0].X++;
                        break;
                    case "L":
                        _knots[0].X--;
                        break;
                }
                MoveAllKnots();
            }
        }
        return _visited.Count;
    }

    public void MoveAllKnots()
    {
        for (int i = 0; i < _knots.Count-1; i++)
        {
            var leader = _knots[i];
            var follower = _knots[i + 1];

            _knots[i+1] = MoveKnot(leader, follower);

            if (i + 1 == _knots.Count-1)
            {
                var pos = _knots.Last().X + "." + _knots.Last().Y;
                if (_visited.ContainsKey(pos)) _visited[pos]++;
                else _visited.Add(pos,1);
            }
        }
    }

    private Coordinate MoveKnot(Coordinate leader, Coordinate follower)
    {  
        if (leader.DistanceToOtherPoint(follower) > Math.Sqrt(2))
        {
            if (leader.X > follower.X) follower.X++;
            if (leader.X < follower.X) follower.X--;
            if (leader.Y > follower.Y) follower.Y++;
            if (leader.Y < follower.Y) follower.Y--;
        }

        return follower;
    }
}