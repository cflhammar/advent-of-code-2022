using aoc_2022.Days.Dec15.InputData;

namespace aoc_2022.Days.Dec15;

public class Sensors
{
    private readonly HashSet<PlacedSensor> _placedSensors = new();
 //   private HashSet<(int x, int y)> _visited = new HashSet<(int x, int y)>();
    private int _searchLimit;

    public Sensors(List<List<string>> input)
    {
        foreach (var line in input)
        {
            CreateSensor(line);
        }
    }
    
    public int NumberOfNoBeaconPosAtRow(int y)
    {
        var xMin = _placedSensors.Select(x => x.Sensor.x - x.DistanceToSensor()).Min();
        var xMax = _placedSensors.Select(x => x.Sensor.x + x.DistanceToSensor()).Max();
        
        var sum = 0;
        for (int x = xMin; x < xMax; x++)
        {
            var pos = (x, y);

            foreach (var sensor in _placedSensors)
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
      return _placedSensors.All(x => x.Beacon != pos);
    }

    public long FindBeaconOnEdge(int searchLimit)
    {
        _searchLimit = searchLimit;
        (int x, int y) pos = (0,0);
        foreach (var placedSensor in _placedSensors)
        {
            pos = TraverseEdge(placedSensor);
            if (pos != (0, 0)) break;
        }

        return (long) pos.x * 4000000 + pos.y;
    }

    public (int, int) TraverseEdge(PlacedSensor placedSensor)
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
            foreach (var otherSensor in _placedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= _searchLimit && pos.y >= 0 && pos.y <= _searchLimit)
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
            foreach (var otherSensor in _placedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= _searchLimit && pos.y >= 0 && pos.y <= _searchLimit)
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
            foreach (var otherSensor in _placedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= _searchLimit && pos.y >= 0 && pos.y <= _searchLimit)
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
            foreach (var otherSensor in _placedSensors)
            {
                if (positionCanHoldBeacon && !otherSensor.PosIsNotWithinDistance(pos))
                {
                    positionCanHoldBeacon = false;
                    break;
                }
            }

            if (positionCanHoldBeacon && pos.x >= 0 && pos.x <= _searchLimit && pos.y >= 0 && pos.y <= _searchLimit)
            {
                return (pos);
            }
        }

        return (0, 0);
    }

    public void CreateSensor(List<string> line)
    {
        var sensor = line[0].Replace("Sensor at ","").Split(",");
        var beacon = line[1].Replace("closest beacon is at ","").Split(",");

        var beaconX = beacon[0].Split("=")[1];
        var beaconY = beacon[1].Split("=")[1];
        
        var sensorX = sensor[0].Split("=")[1];
        var sensorY = sensor[1].Split("=")[1];

        var placedSens = new PlacedSensor()
        {
            Sensor = (Int32.Parse(sensorX), Int32.Parse(sensorY)),
            Beacon = (Int32.Parse(beaconX), Int32.Parse(beaconY)),
        };
        
        _placedSensors.Add(placedSens);
    }
}