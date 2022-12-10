namespace aoc_2022.Days.Dec09;

public class RopePredictor
{
    List<Coordinate> Knots = new();
    Dictionary<string, int> visited = new ();

    public RopePredictor(int knots)
    {
        for (int i = 0; i <= knots; i++) Knots.Add(new Coordinate());
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
        for (int i = 0; i < Knots.Count-1; i++)
        {
            var leader = Knots[i];
            var follower = Knots[i + 1];

            Knots[i+1] = MoveKnot(leader, follower);

            if (i + 1 == Knots.Count-1)
            {
                var pos = Knots.Last().X + "." + Knots.Last().Y;
                if (visited.ContainsKey(pos)) visited[pos]++;
                else visited.Add(pos,1);
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