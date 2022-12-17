namespace aoc_2022.Days.Dec16;

public class Valves
{
    public Dictionary<string, Cave> caves = new();
    public long max = 0;
    public List<(List<string> path, List<int> time)> completePaths = new();

    public List<string> valvesWorthVisit;


    public Valves(List<List<string>> input)
    {
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

            
            caves.Add(cave.Name, cave);
        }

        foreach (var cave in caves)
        {
            CavesPathsBfs(cave.Value);
        }

         /*
        foreach (var (key, cave) in caves)
        {
            Console.WriteLine(key);
            Console.WriteLine(cave.Pressure);   
            Console.WriteLine(string.Join(", ", cave.ConnectedCaves));   
            Console.WriteLine();
            
        }
        */
        
        valvesWorthVisit = caves.Keys.Where(name => caves[name].Pressure > 0).ToList();

        MakeAllPaths();
        GetMaxPath();
    }

    private long GetMaxPath()
    {
        long max = 0;
    //    var compare = new List<string>() {"AA", "FA", "GA", "HA", "IA", "JA", "KA", "LA", "MA", "NA", "OA", "PA"};
        
        foreach (var (path, time) in completePaths)
        {
            /*
            if ( path[1] == "FA" && path[2] == "GA" && path[3] == "HA"  && path[4] == "IA" &&   path[5] == "JA" &&   path[6] == "KA" &&   path[7] == "LA" &&   path[8] == "MA"   )
            {
                Console.WriteLine(string.Join("->", path));    
                Console.WriteLine(string.Join("->", time));    
                Console.WriteLine();
            }                           git sta
            */                                                              
   
        //    if (path.SequenceEqual(compare))    Console.Wr”iteLine("found");

            var score = GetPathValue(path, time);
            if (score > max)
            {
                Console.WriteLine(string.Join("-",path));
                max = score;
            }
        }
        
        Console.WriteLine();
        Console.WriteLine(max);
        return max;
    }

    private long GetPathValue(List<string> path, List<int> time)
    {
        long sum = 0;
        for (int i = 0; i < path.Count(); i++)
        {
            var cave = path[i];
            var timeOpened = time[i];
            var pressure = caves[cave].Pressure;

            sum += (30 - timeOpened) * pressure;
          //  Console.WriteLine(sum);
        }

        return sum;
    }
    

    private void MakeAllPaths()
    {
        var time = 30;
        
        var myPath = new List<string>();
        var myPathTime = new List<int>();
        var toVisit = valvesWorthVisit;
        var spent = 0;

        FollowPath("AA", myPath, myPathTime, toVisit, spent);
    }


    private void FollowPath(string current,List<string> myPath, List<int> myPathTime, List<string> toVisit, int spent)
    {
        myPath.Add(current);
        myPathTime.Add(spent);
        toVisit.Remove(current);

        if (spent > 30) Console.WriteLine("errpr!");
        
        if (toVisit.Count == 0) 
        {
            completePaths.Add((myPath, myPathTime));
        }
        
        /*
        if (myPath.Count > 8 && myPath[1] == "FA" && myPath[2] == "GA" && myPath[3] == "HA" && myPath[4] == "IA" && myPath[5] == "JA" &&     
            myPath[6] == "KA" && myPath[7] == "LA" && myPath[8] == "MA")                                             
        {                                                                                                                                    
            Console.WriteLine("ajdjoa");                                                                                                     
        }
        */

        foreach (var valveToVisit in toVisit)
        {
            /*
            if (myPath.Count > 8 && myPath[1] == "FA" && myPath[2] == "GA" && myPath[3] == "HA" && myPath[4] == "IA" && myPath[5] == "JA" &&       
                myPath[6] == "KA" && myPath[7] == "LA" && myPath[8] == "MA" && valveToVisit == "NA")                                                                       
            {                                                                                                                                     
                Console.WriteLine("ajdjoa");                                                                                                      
            } 
            */                                                                                                                                    
            
            var copyMyPath = new List<string>(myPath);
            var copyMyPathTime = new List<int>(myPathTime);
            var copyToVisit = new List<string>(toVisit);
            int copySpent = spent;

            var steps = caves[current].paths[valveToVisit] + 1;
            int copySteps = steps;

            if ((spent + copySteps <= 30))
            {
                FollowPath(valveToVisit, copyMyPath, copyMyPathTime ,copyToVisit, copySpent + copySteps);
            }
            else
            {
                completePaths.Add((copyMyPath, copyMyPathTime));
            }
        }
        
    }
    
    public void CavesPathsBfs(Cave root)
    {
        var qu = new Queue<Cave>();

        var paths = root.paths;
        
        var explored = new HashSet<string>();
        explored.Add(root.Name);
        
        qu.Enqueue(root);

        while (qu.Count > 0)
        {
            var current = qu.Dequeue();

            foreach (var cave in caves[current.Name].ConnectedCaves)
            {
                if (!explored.Contains(cave))
                {
                    explored.Add(cave);


                    if (paths.ContainsKey(current.Name))
                    {
                        paths.Add(cave, paths[current.Name] + 1);
                    }
                    else
                    {
                        paths.Add(cave, 1);
                    }
                    
                    qu.Enqueue(caves[cave]);
                }
            }
        }
    }
}
  