using Microsoft.VisualBasic;

namespace aoc_2022.Days.Dec16;

public class Valves
{
    private readonly Dictionary<string, Cave> _caves = new();
    private readonly int _players;
    private readonly int _maxTime;
    private readonly List<string> _valvesWorthVisit;
    private Dictionary<string, long> _paths = new();
    
    public Valves(List<List<string>> input, int players = 1)
    {
        _players = players;
        _maxTime = _players == 1 ? 30 : 26;

        foreach (var line in input)
        {
            var thisCave = line[0].Replace("Valve ", "").Replace(" has flow rate", "").Split("=");

            List<string> connectCaves;
            if (line[1].Length > 4)
            {
                connectCaves = line[1].Replace(" ", "").Split(",").ToList();
            }
            else
            {
                connectCaves = new List<string>()
                {
                    line[1].Replace(" ", "")
                };
            }
            
            var cave = new Cave()
            {
                Name = thisCave[0],
                Pressure = Int32.Parse(thisCave[1]),
                ConnectedCaves = connectCaves
            };
            
            _caves.Add(cave.Name, cave);
        }

        foreach (var cave in _caves)
        {
            AddPossibleMovesFromCaveWithBfs(cave.Value);
        }

        _valvesWorthVisit = _caves.Keys.Where(name => _caves[name].Pressure > 0).ToList();
        MakeAllPaths();
    }

    public long GetMaxFlowRate()
    {
        if (_players == 1) return GetMaxPath();
        else return GetTwoMaxPaths();
    }

    private long GetMaxPath()
    {
        return _paths.Max(x => x.Value);
    }

    private long GetTwoMaxPaths()
    {
        var pathsSortedByFlowRate = _paths.OrderByDescending(x => x.Value).ToList();
        long max = 0;
        
        for (int i = 0; i < pathsSortedByFlowRate.Count; i++)
        {
            for (int j = i + 1; j < pathsSortedByFlowRate.Count; j++)
            {
                var me = pathsSortedByFlowRate[i];
                var myPath = me.Key.Split(";")[0].Split(":");
                
                var elephant = pathsSortedByFlowRate[j];
                var ePath = elephant.Key.Split(";")[0].Split(":");
                
                if (myPath.Intersect(ePath).Count() > 1) continue;

                var score = me.Value + elephant.Value;
                if (score > max) max = score;

                // only compare the top 1000 paths
                if (me.Value + elephant.Value < max) break;
            }
            if (i > 1000 ) break;
        }
        
        return max;
    }


    private long GetPathFlowRate(List<string> path, List<int> time)
    {
        long sum = 0;
        for (int i = 0; i < path.Count(); i++)
        {
            var cave = path[i];
            var timeOpened = time[i];
            var pressure = _caves[cave].Pressure;

            sum += (_maxTime - timeOpened) * pressure;
        }

        return sum;
    }
    

    private void MakeAllPaths()
    {
        var myPath = new List<string>();
        var myPathTime = new List<int>();
        var toVisit = _valvesWorthVisit;
        var spent = 0;

        FollowPath("AA", myPath, myPathTime, toVisit, spent);
    }

    private void AddPathHashToPossiblePaths(List<string> where, List<int> when)
    {
        var hash = "";
        hash += string.Join(":", where);
        hash += ";";
        hash += string.Join(":", when);

        var totalFlowRate = GetPathFlowRate(where, when);
        
        _paths.TryAdd(hash, totalFlowRate);
    }

    private void FollowPath(string current,List<string> myPath, List<int> myPathTime, List<string> toVisit, int spent)
    {
        myPath.Add(current);
        myPathTime.Add(spent);
        toVisit.Remove(current);
        
        AddPathHashToPossiblePaths(myPath, myPathTime);
        
        foreach (var valveToVisit in toVisit)
        {
            var copyMyPath = new List<string>(myPath);
            var copyMyPathTime = new List<int>(myPathTime);
            var copyToVisit = new List<string>(toVisit);
            int copySpent = spent;

            var steps = _caves[current].paths[valveToVisit] + 1;
            int copySteps = steps;

            if (spent + copySteps <= _maxTime)
            {
                FollowPath(valveToVisit, copyMyPath, copyMyPathTime ,copyToVisit, copySpent + copySteps);
            }
            else
            {
                AddPathHashToPossiblePaths(myPath, myPathTime);
            }
        }
    }
    
    public void AddPossibleMovesFromCaveWithBfs(Cave root)
    {
        var qu = new Queue<Cave>();
        var paths = root.paths;
        
        var explored = new HashSet<string> {root.Name};
        qu.Enqueue(root);

        while (qu.Count > 0)
        {
            var current = qu.Dequeue();

            foreach (var cave in _caves[current.Name].ConnectedCaves)
            {
                if (!explored.Contains(cave))
                {
                    explored.Add(cave);
                    
                    if (paths.ContainsKey(current.Name)) paths.Add(cave, paths[current.Name] + 1);
                    else paths.Add(cave, 1);
                    
                    qu.Enqueue(_caves[cave]);
                }
            }
        }
    }
}
  