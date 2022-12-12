using System.Data;

namespace aoc_2022.Days.Dec12;

public class BFSMap
{
    static List<(int x, int y)> neighbors = new List<(int x, int y)>() { (-1, 0), (1, 0), (0, -1), (0, 1)};

    
    public string CreteBFSMap(List<List<char>> grid)
    {
        var height = grid.Count;
        var width = grid[0].Count;

        var newGrid = new List<List<char>>();
        var costs = new int[height, width];

        foreach (var x in Enumerable.Range(0, height))
        {
            foreach (var y in Enumerable.Range(0, width))
            {
                if (grid[x][y] == 'S')
                {
                    costs[x, y] = 1;
                }
                else if (grid[x][y] == 'E')
                {
                    costs[x,y] = 26;
                }
                else
                {
                    costs[x,y] = (grid[x][y] - 'a') + 1;
                }
            }
        }
        return BFS(grid, height, width, costs).ToString();
    }
    

    private static int BFS(List<List<char>> grid, int height, int width, int[,] costs = null)
    {
        var locQueue = new Queue<((int x, int y), int cost)>();
        foreach(var x in Enumerable.Range(0, height))
        {
            foreach(var y in Enumerable.Range(0, width))
            {
                if ((costs == null && grid[x][y] == 'S') || (costs != null && costs[x,y] == 1))
                {
                    locQueue.Enqueue(((x, y), 0));
                }
            }
        }
        var seen = new HashSet<(int x, int y)>();

        while(locQueue.Any())
        {
            ((int x, int y), int cost) = locQueue.Dequeue();
            if(!seen.Add((x,y)))
            {
                continue;
            }
            if (grid[x][y] == 'E')
            {
                return cost;
            }

            foreach((int dx, int dy) in neighbors)
            {
                var dxP = x + dx;
                var dyP = y + dy;
                if((dxP >= 0 && dxP < height) && (dyP >= 0 && dyP < width))
                {
                    if (costs == null)
                    {
                        if (grid[x][y] == 'S' ? grid[dxP][dyP] - 'a' <= 1 : grid[dxP][dyP] - grid[x][y] <= 1)
                            locQueue.Enqueue(((dxP, dyP), cost + 1));
                    }
                    else
                    {
                        if (costs[dxP, dyP] <= 1 + costs[x, y])
                            locQueue.Enqueue(((dxP, dyP), cost + 1));
                    }
                       
                }
            }
        }
        return 0;
    }
    
  
}