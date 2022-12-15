using aoc_2022.Days.Dec15.InputData;

namespace aoc_2022.Days.Dec15;

public class Sensors
{
    public HashSet<PlacedSensor> PlacedSensors = new HashSet<PlacedSensor>();
    public HashSet<(int x, int y)> visited = new HashSet<(int x, int y)>();
    private int SearchLimit = 0;

    public Sensors(List<List<string>> input)
    {
        foreach (var line in input)
        {
            CreateSensor(line);
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
                if (!sensor.PosIsNotWithinDistance(pos) && PosIsNotBeacon(pos))
                {
                    sum++;
                    break;
                }
            }
        }

        return sum;
    }

    private bool PosIsNotBeacon((int x, int y) pos)
    {
      return PlacedSensors.All(x => x.Beacon != pos);
    }

    public long FindBeaconOnEdge(int searchLimit)
    {
        SearchLimit = searchLimit;
        (int x, int y) pos = (0,0);
        foreach (var placedSensor in PlacedSensors)
        {
            pos = TraverseEdge(placedSensor, searchLimit);
            if (pos != (0, 0)) break;
        }

        return (long) pos.x * 4000000 + pos.y;
    }

    public (int, int) TraverseEdge(PlacedSensor placedSensor, int searchLimit)
    {
        var dist = placedSensor.DistanceToSensor();
        
        var pos = TraverseLeftToTop(placedSensor.Sensor, dist + 1);
        if (pos != (0,0)) TraverseTopToRight(placedSensor.Sensor, dist + 1);
        if (pos != (0,0)) TraverseRightToBottom(placedSensor.Sensor, dist + 1);
        if (pos != (0,0)) TraverseBottomToLeft(placedSensor.Sensor, dist + 1);

        return pos != (0, 0) ? pos : (0, 0);
    }

    public (int x, int y) TraverseLeftToTop((int x, int y) sensor, int dist)
    {
        (int x, int y) pos = (sensor.x - dist, sensor.y);
        
        for (int i = 0; i <= dist; i++)
        {
            pos = (pos.x + 1, pos.y + 1);

            var positionCanHoldBeacon = true;
            foreach (var otherSensor in PlacedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= SearchLimit && pos.y >= 0 && pos.y <= SearchLimit)
            {
                return (pos);
            }
        }

        return (0, 0);
    }
    
    public (int x, int y) TraverseTopToRight((int x, int y) sensor, int dist)
    {
        (int x, int y)  pos = (sensor.x, sensor.y + dist);
        
        for (int i = 0; i <= dist; i++)
        {
            pos = (pos.x + 1, pos.y - 1);

            var positionCanHoldBeacon = true;
            foreach (var otherSensor in PlacedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= SearchLimit && pos.y >= 0 && pos.y <= SearchLimit)
            {
                return (pos);
            }
        }

        return (0, 0);
    }
    
    public (int x, int y) TraverseRightToBottom((int x, int y) sensor, int dist)
    {
        (int x, int y) pos = (sensor.x + dist, sensor.y);
        
        for (int i = 0; i <= dist; i++)
        {
            pos = (pos.x - 1, pos.y - 1);

            var positionCanHoldBeacon = true;
            foreach (var otherSensor in PlacedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= SearchLimit && pos.y >= 0 && pos.y <= SearchLimit)
            {
                return (pos);
            }
        }

        return (0, 0);
    }
    
    public (int x, int y) TraverseBottomToLeft((int x, int y) sensor, int dist)
    {
        (int x, int y) pos = (sensor.x, sensor.y - dist);
        
        
        for (int i = 0; i <= dist; i++)
        {
            pos = (pos.x - 1, pos.y + 1);
            var positionCanHoldBeacon = true;
            foreach (var otherSensor in PlacedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= SearchLimit && pos.y >= 0 && pos.y <= SearchLimit)
            {
                return (pos);
            }
        }

        return (0, 0);
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