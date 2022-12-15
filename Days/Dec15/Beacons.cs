using aoc_2022.Days.Dec15.InputData;

namespace aoc_2022.Days.Dec15;

public class Sensors
{
    public HashSet<PlacedSensor> PlacedSensors = new HashSet<PlacedSensor>();
    public HashSet<(int x, int y)> visited = new HashSet<(int x, int y)>();

    public Sensors(List<List<string>> input)
    {
        foreach (var line in input)
        {
            CreateSensor(line);
        }
        
    }

    public int FindBeacon()
    {
        var xMin = 0;
        var xMax = 4000000;
        var yMin = 0;
        var yMax = 4000000;

        (int px , int py) pos = (0, 0);
        
        var sum = 0;
        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                if (visited.Contains((x, y))) break;
                
                var possible = true;
                foreach (var sensor in PlacedSensors)
                {

                    if (possible == false) break;
                    
                    if (!sensor.PosIsNotWithinDistance(x, y))
                    {
                        visited.Add((x, y));
                        possible = false;
                    }
                }
                
                if (possible)
                {
                    Console.WriteLine(x + "." + y);
                    pos = (x, y);
                    break;
                }
            }
        }

        return pos.px * 4000000 + pos.py;
    }



    public void FindBeaconOnEdge()
    {
        foreach (var placedSensor in PlacedSensors)
        {
            TraverseEdge(placedSensor);
        }
        
    }

    public void TraverseEdge(PlacedSensor placedSensor)
    {
       var limit = 4000000;
     //   var limit = 20;

        var dist = placedSensor.DistanceToSensor() + 1;
        var x = placedSensor.Sensor.x;
        var y = placedSensor.Sensor.y;
        

        (int x, int y) pos1 = (x - dist, y);
        (int x, int y)  pos2 = (x, y + dist);
        (int x, int y)  pos3 = (x + dist, y);
        (int x, int y)  pos4 = (x , y - dist);

        for (int i = 0; i <= dist; i++)
        {
            pos1 = (pos1.x + 1, pos1.y + 1);
            pos2 = (pos2.x + 1, pos2.y - 1);
            pos3 = (pos3.x - 1, pos3.y - 1);
            pos4 = (pos4.x - 1, pos4.y + 1);

            var possible1 = true;
            var possible2 = true;
            var possible3 = true;
            var possible4 = true;
            
            foreach (var sensor in PlacedSensors)
            {
                if (!sensor.PosIsNotWithinDistance(pos1.x, pos1.y) && possible1)
                {
                  //  visited.Add(pos1);
                    possible1 = false;
                }
                if (!sensor.PosIsNotWithinDistance(pos2.x, pos2.y) && possible2)
                {
               //     visited.Add(pos2);
                    possible2 = false;
                }
                if (!sensor.PosIsNotWithinDistance(pos3.x, pos3.y) && possible3)
                {
              ///      visited.Add(pos3);
                    possible3 = false;
                }
                if (!sensor.PosIsNotWithinDistance(pos4.x, pos4.y) && possible4)
                {
           //         visited.Add(pos4);
                    possible4 = false;
                }
            }
            
            if (possible1 && pos1.x >= 0 && pos1.x <= limit && pos1.y >= 0 && pos1.y <= limit) Console.WriteLine((long) pos1.x *4000000 + pos1.y);
            if (possible2 && pos2.x >= 0 && pos2.x <= limit && pos2.y >= 0 && pos2.y <= limit) Console.WriteLine((long) pos2.x *4000000+ pos2.y);
            if (possible3 && pos3.x >= 0 && pos3.x <= limit && pos3.y >= 0 && pos3.y <= limit) Console.WriteLine((long) pos3.x *4000000 + pos3.y);
            if (possible4 && pos4.x >= 0 && pos4.x <= limit && pos4.y >= 0 && pos4.y <= limit) Console.WriteLine((long) pos4.x *4000000 + pos4.y);
  
        }
        
    }
    
    
    
    

    public int NumberOfNoBeaconPosAtRow(int y)
    {
        var xMin = PlacedSensors.Select(x => x.Sensor.x - x.DistanceToSensor()).Min();
        var xMax = PlacedSensors.Select(x => x.Sensor.x + x.DistanceToSensor()).Max();
        
        
        var sum = 0;
        for (int x = xMin; x < xMax; x++)
        {

            var pos = (x, y);

            foreach (var sensor in PlacedSensors)
            {
                if (!sensor.PosIsNotWithinDistance(x, y))
                {
 //                   Console.WriteLine(x + ". " + y);
                    sum++;
                    break;
                }
                    
                
            }
            

        }

        return sum;
    }

    public void CreateSensor(List<string> line)
    {
        var sens = line[0].Replace("Sensor at ","").Split(",");
        var beac = line[1].Replace("closest beacon is at ","").Split(",");;

        var beacX = beac[0].Split("=")[1];
        var beacY = beac[1].Split("=")[1];
        
        var sensX = sens[0].Split("=")[1];
        var sensY = sens[1].Split("=")[1];

        var placedSens = new PlacedSensor()
        {
            Sensor = (Int32.Parse(sensX), Int32.Parse(sensY)),
            Beacon = (Int32.Parse(beacX), Int32.Parse(beacY)),
        };


        PlacedSensors.Add(placedSens);
    }
}