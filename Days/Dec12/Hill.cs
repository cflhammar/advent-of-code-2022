using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec12;

public class Hill
{
    private static List<(int, int)> _neighbours = new() {(-1,0), (1,0), (0, -1), (0,1)};

    public int FindShortestPath(List<List<char>> map, bool reverse = false)
    {
         if (reverse) map = FlipMap(map);
        
        (int x, int y) start = new();
        foreach (var row in Enumerable.Range(0, map.Count))
        {
            foreach (var col in Enumerable.Range(0, map.First().Count))
            {
                if (map[row][col] == 'S') start = (row, col);
            }
        }

        return BfSearch(map, start);
    }
    
    private List<List<char>> FlipMap(List<List<char>> map)
    {
        var newMap = new List<List<char>>();
        
        foreach (var row in Enumerable.Range(0, map.Count))
        {
            newMap.Add(new List<char>());
            foreach (var col in Enumerable.Range(0, map.First().Count))
            {
                if (map[row][col] == 'S' || map[row][col] == 'a') newMap[row].Add('E');
                else if (map[row][col] == 'E') newMap[row].Add('S');
                else newMap[row].Add((char)('z' -  map[row][col] + 'a'));
            }
        }

        return newMap;
    }

    public int BfSearch(List<List<char>> map, (int,int) start)
    {
        var visited = new HashSet<(int x, int y)>();
        var q = new Queue<((int x, int y), int dist)>();
        
        q.Enqueue((start,0));
        while (q.Any())
        {
            ((int x, int y), int currentDist) = q.Dequeue();

            if (!visited.Add((x,y))) continue;
            
            var currentVal = map[x][y];
            if (currentVal == 'E') return currentDist;
            
            foreach ((int dx, int dy) in _neighbours)
            {
                var xNext = x + dx;
                var yNext = y + dy;

                if ((xNext >= 0 && xNext < map.Count) && (yNext >= 0 && yNext < map.First().Count))
                {
                    var nextVal = map[xNext][yNext] != 'E' ? map[xNext][yNext] : 'z' ;
                    if (currentVal == 'S') currentVal = 'a';
                    
                    if (nextVal - currentVal <= 1)
                    {
                        q.Enqueue(((xNext, yNext),currentDist + 1));
                    }
                }
            }
        }
        return 0;
    }
    
}