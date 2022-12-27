namespace aoc_2022.Days.Dec24;

public class Blizzard
{
    private List<(int x, int y)> _blizzardCurrentPositions = new();
    private readonly List<(int x, int y)> _blizzardDirection = new();
    private readonly List<string> _map;
    private (int x, int y) _start;
    private (int x, int y) _stop;

    public Blizzard(List<string> input)
    {
        _map = input;

        for (int x = 0; x < _map.First().Length; x++)
        {
            if (_map[0][x] == '.')
            {
                _start = (x, 0);
            }
            
            if (_map[_map.Count - 1][x] == '.')
            {
                _stop = (x, _map.Count - 1);
            }
        }
        
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input.First().Length; j++)
            {
                switch (input[i][j])
                {
                    case '>':
                        _blizzardCurrentPositions.Add((j, i));
                        _blizzardDirection.Add((1, 0));
                        
                        break;
                    case '<':
                        _blizzardCurrentPositions.Add((j, i));
                        _blizzardDirection.Add((-1, 0));
                        break;
                    case '^':
                        _blizzardCurrentPositions.Add((j, i));
                        _blizzardDirection.Add((0, -1));
                        break;
                    case 'v':
                        _blizzardCurrentPositions.Add((j, i));
                        _blizzardDirection.Add((0, 1));
                        break;
                }
            }
        }
    }
    
    public int ThereAndBackAgainAndAgain()
    {
        var sum = 0;
        sum += FindShortestPath();

        var temp = _start;
        _start = _stop;
        _stop = temp;

        sum += FindShortestPath() + 1;  // add one for waiting when turning around
        
        temp = _start;
        _start = _stop;
        _stop = temp;

        sum += FindShortestPath() + 1; // add one for waiting when turning around
        
        return sum;
    }
    
    public int FindShortestPath()
    {
        var set = new HashSet<(int x, int y)>();
        set.Add(_start);
        var time = 0;
        
        while (true)
        {
            MoveBlizzard();
            var next = new HashSet<(int x, int y)>();
            
            foreach (var clone in set)
            {
                if (clone == _stop)
                {
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
    }
    
    bool PositionIsSafe((int x, int y) pos)
    {
        if (pos == _start || pos == _stop) return true;
        
        bool isWithinBoundsX = pos.x > 0 && pos.x < _map.First().Length - 1;
        bool isWithinBoundsY = pos.y > 0 && pos.y < _map.Count - 1;
        bool isNotBlizzard = !_blizzardCurrentPositions.Contains(pos);

        return isWithinBoundsX && isWithinBoundsY && isNotBlizzard;
    }
    
    void MoveBlizzard()
    {
        for (int i = 0; i < _blizzardCurrentPositions.Count; i++)
        {
            var xNew = (_blizzardCurrentPositions[i].x + _blizzardDirection[i].x) % (_map.First().Length - 2);
            if (xNew == 0) xNew = _map.First().Length - 2;
            
            var yNew = (_blizzardCurrentPositions[i].y + _blizzardDirection[i].y) % (_map.Count - 2);
            if (yNew == 0) yNew = _map.Count - 2;

            _blizzardCurrentPositions[i] = (xNew, yNew);
        }
    }

/*
    private void Print(HashSet<(int x, int y)> elfPositions)
    {
        for (int y = 0; y < _map.Count; y++)
        {
            var temp = "";
            for (int x = 0; x < _map.First().Length; x++)
            { 
                if (_map[y][x] == '#') temp += "#";
                else if (_blizardCurrentPosisions.Contains((x, y))) temp += "O";
                else if (elfPositions.Contains((x, y))) temp += "e";
                else temp += ".";
            }
            Console.WriteLine(temp);
        }
        Console.WriteLine();
    }
*/
}