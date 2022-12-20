using System.Diagnostics;

namespace aoc_2022.Days.Dec19;

public class MiningRobots
{

    public MiningRobots()
    {
        
    }

    public int FindAll(List<(int oreForOre, int oreForclay, (int ore, int clay) obsidian, (int ore, int obsidian) genode)> robotData, int maxTime)
    {
        var sum = 0;
        var index = 1;

        Stopwatch stopwatch = new Stopwatch();
        foreach (var setting in robotData)
        {
            sum += index * Bfs(setting, maxTime);
            index++;
        }

        Console.WriteLine(sum);
        return sum;
    }

    public int FindThree(List<(int oreForOre, int oreForclay, (int ore, int clay) obsidian, (int ore, int obsidian) genode)> robotData, int maxTime)
    {
        var sum = 1;
        var index = 0;

        Stopwatch stopwatch = new Stopwatch();
        foreach (var setting in robotData)
        {
            index++;
            stopwatch.Start();
            sum *= Bfs(setting, maxTime);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            if (index == 3) break;
        }

        Console.WriteLine(sum);
        return sum;
    }
    
    private int Bfs((int oreForOre, int oreForclay, (int ore, int clay) obsidian, (int ore, int obsidian) genode) robotCost, int maxTime)
    {
        var maxOreNeededPerRounds = new List<int>() {robotCost.oreForOre, robotCost.oreForclay, robotCost.obsidian.ore, robotCost.genode.ore}.Max();
        var memory = new HashSet<string>();
        var max = 0;
        
        var startingState = new State();
        var qu = new Queue<State>();
        qu.Enqueue(startingState);
        
        while (qu.Any())
        {
            var currentState = qu.Dequeue();
            if (currentState.Time > maxTime)
            {
                if (currentState.Genode > max) max = currentState.Genode;
                continue;
            }
            
   //         if (max > (currentState.Genode + (32 - currentState.Time) * currentState.GenodeRobot + (32 - currentState.Time) * (32 - currentState.Time +1 ) / 2 ))
   //             continue;
            
            var hash = currentState.ToString();
            if (memory.Contains(hash)) continue;
            else memory.Add(hash);
            
            //or build genode robot
            if (currentState.Ore >= robotCost.genode.ore && currentState.Obsidian >= robotCost.genode.obsidian && currentState.Time <= maxTime -1)
            {
                var next = new State()
                {
                    Clay = currentState.Clay + currentState.ClayRobot,
                    Ore = currentState.Ore + currentState.OreRobot - robotCost.genode.ore,
                    Obsidian = currentState.Obsidian + currentState.ObsidianRobot - robotCost.genode.obsidian,
                    Genode = currentState.Genode + currentState.GenodeRobot,
                    ClayRobot = currentState.ClayRobot,
                    OreRobot = currentState.OreRobot,
                    ObsidianRobot = currentState.ObsidianRobot,
                    GenodeRobot = currentState.GenodeRobot + 1,
                    Time = currentState.Time + 1
                };
                qu.Enqueue(next);
            }
            
            
            {
                //or build obsidian robot
                if (currentState.Ore >= robotCost.obsidian.ore && currentState.Clay >= robotCost.obsidian.clay && currentState.Time <= maxTime -1)
                {
                    var next = new State()
                    {
                        Clay = currentState.Clay + currentState.ClayRobot - robotCost.obsidian.clay,
                        Ore = currentState.Ore + currentState.OreRobot - robotCost.obsidian.ore,
                        Obsidian = currentState.Obsidian + currentState.ObsidianRobot,
                        Genode = currentState.Genode + currentState.GenodeRobot,
                        ClayRobot = currentState.ClayRobot,
                        OreRobot = currentState.OreRobot,
                        ObsidianRobot = currentState.ObsidianRobot + 1,
                        GenodeRobot = currentState.GenodeRobot,
                        Time = currentState.Time + 1
                    };
                    qu.Enqueue(next);
                }
            
                //or build clay robot
                if (currentState.Ore >= robotCost.oreForclay && currentState.ClayRobot < robotCost.obsidian.clay && currentState.Time <= maxTime -1)
                {
                    var next = new State()
                    {
                        Clay = currentState.Clay + currentState.ClayRobot,
                        Ore = currentState.Ore + currentState.OreRobot - robotCost.oreForclay,
                        Obsidian = currentState.Obsidian + currentState.ObsidianRobot,
                        Genode = currentState.Genode + currentState.GenodeRobot,
                        ClayRobot = currentState.ClayRobot + 1,
                        OreRobot = currentState.OreRobot,
                        ObsidianRobot = currentState.ObsidianRobot,
                        GenodeRobot = currentState.GenodeRobot,
                        Time = currentState.Time + 1
                    };
                    qu.Enqueue(next);
                }

            
                //or build ore robot
                if (currentState.Ore >= robotCost.oreForOre && currentState.OreRobot < maxOreNeededPerRounds && currentState.Time <= maxTime -1)
                {
                    var next = new State()
                    {
                        Clay = currentState.Clay + currentState.ClayRobot,
                        Ore = currentState.Ore + currentState.OreRobot - robotCost.oreForOre,
                        Obsidian = currentState.Obsidian + currentState.ObsidianRobot,
                        Genode = currentState.Genode + currentState.GenodeRobot,
                        ClayRobot = currentState.ClayRobot,
                        OreRobot = currentState.OreRobot + 1,
                        ObsidianRobot = currentState.ObsidianRobot,
                        GenodeRobot = currentState.GenodeRobot,
                        Time = currentState.Time + 1
                    };
                    qu.Enqueue(next);
                }
            
                // do nothing and mine
                var nexxt = new State()
                {
                    Clay = currentState.Clay + currentState.ClayRobot,
                    Ore = currentState.Ore + currentState.OreRobot,
                    Obsidian = currentState.Obsidian + currentState.ObsidianRobot,
                    Genode = currentState.Genode + currentState.GenodeRobot,
                    ClayRobot = currentState.ClayRobot,
                    OreRobot =  currentState.OreRobot,
                    ObsidianRobot = currentState.ObsidianRobot,
                    GenodeRobot = currentState.GenodeRobot,
                    Time = currentState.Time + 1
                };
                qu.Enqueue(nexxt);
            }
        }

        Console.WriteLine(max);
        return max;
    }
}

public class State
{
    public int Ore = 0;
    public int OreRobot = 1;
    public int Clay = 0;
    public int ClayRobot = 0;
    public int Obsidian = 0;
    public int ObsidianRobot = 0;
    public int Genode = 0;
    public int GenodeRobot = 0;
    public int Time = 1;

    public override string ToString()
    {
        return Time + ": " + Ore + "," + Clay + "," + Obsidian + "," + Genode + "; " + OreRobot + "," + ClayRobot + "," + ObsidianRobot + "," + GenodeRobot;
        
    }
}