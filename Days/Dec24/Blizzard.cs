namespace aoc_2022.Days.Dec24;

public class Blizzard
{
    private List<(int x, int y)> _blizPos = new();
    private List<(int x, int y)> _blizMove = new();
    private List<string> _map;
    private (int x, int y) start;
    private (int x, int y) stop;

    public Blizzard(List<string> input)
    {
        _map = input;

        for (int x = 0; x < _map.First().Length; x++)
        {
            if (_map[0][x] == '.')
            {
                start = (x, 0);
            }
            
            if (_map[_map.Count - 1][x] == '.')
            {
                stop = (x, _map.Count - 1);
            }
        }
        
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input.First().Length; j++)
            {
                switch (input[i][j])
                {
                    case '>':
                        _blizPos.Add((j, i));
                        _blizMove.Add((1, 0));
                        
                        break;
                    case '<':
                        _blizPos.Add((j, i));
                        _blizMove.Add((-1, 0));
                        break;
                    case '^':
                        _blizPos.Add((j, i));
                        _blizMove.Add((0, -1));
                        break;
                    case 'v':
                        _blizPos.Add((j, i));
                        _blizMove.Add((0, 1));
                        break;
                }
            }
        }
    }
    
    public int ThereAndBackAgainAndAgain()
    {
        var sum = 0;
        sum += Bfs();

        var temp = start;
        start = stop;
        stop = temp;

        sum += Bfs() + 1;
        
        temp = start;
        start = stop;
        stop = temp;

        sum += Bfs() + 1;

        
        Console.WriteLine(sum);
        return sum;


    }
    
    
    

    public int Bfs()
    {
        var set = new HashSet<(int x, int y)>();
        set.Add(start);
        var time = 0;
        
        while (true)
        {
            MoveBlizzard();
            var next = new HashSet<(int x, int y)>();
            
            foreach (var clone in set)
            {
                if (clone == stop)
                {
                    Console.WriteLine(time);
                    return time;
                }
                
                if (PositionIsSafe((clone.x + 1, clone.y)))
                {
                    next.Add((clone.x + 1, clone.y));
                }
                
                if (PositionIsSafe((clone.x - 1, clone.y)))
                {
                    next.Add((clone.x - 1, clone.y));

                }
                
                if (PositionIsSafe((clone.x, clone.y - 1)))
                {
                    next.Add((clone.x, clone.y - 1));

                }
                
                if (PositionIsSafe((clone.x, clone.y + 1)))
                {
                    next.Add((clone.x, clone.y + 1));

                }
                
                if (PositionIsSafe((clone.x, clone.y)))
                {
                    next.Add((clone.x, clone.y));
                }
                
            }
            time++;
            set = next;
        }
        
        return 0;
    }

    public class State
    {
        public (int x, int y) Pos;
        public int Dist;
    }

    bool PositionIsSafe((int x, int y) pos)
    {
        if (pos == start || pos == stop) return true;
        
        bool isWithinBoundsX = pos.x > 0 && pos.x < _map.First().Length - 1;
        bool isWithinBoundsY = pos.y > 0 && pos.y < _map.Count - 1;
        bool isNotBlizzard = !_blizPos.Contains(pos);

        return isWithinBoundsX && isWithinBoundsY && isNotBlizzard;
    }
    
    void MoveBlizzard()
    {
        for (int i = 0; i < _blizPos.Count; i++)
        {
            var xNew = (_blizPos[i].x + _blizMove[i].x) % (_map.First().Length - 2);
            if (xNew == 0) xNew = _map.First().Length - 2;
            
            var yNew = (_blizPos[i].y + _blizMove[i].y) % (_map.Count - 2);
            if (yNew == 0) yNew = _map.Count - 2;

            _blizPos[i] = (xNew, yNew);
        }
    }
    
    void Print(HashSet<(int x, int y)> positions)
    {
        for (int y = 0; y < _map.Count; y++)
        {
            var temp = "";
            for (int x = 0; x < _map.First().Length; x++)
            { 
                if (_map[y][x] == '#') temp += "#";
                else if (_blizPos.Contains((x, y))) temp += "O";
                else if (positions.Contains((x, y))) temp += "e";
                else temp += ".";
            }
            Console.WriteLine(temp);
        }
        Console.WriteLine();
    }
}