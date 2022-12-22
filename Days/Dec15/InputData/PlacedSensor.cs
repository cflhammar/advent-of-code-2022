namespace aoc_2022.Days.Dec15.InputData;

public class PlacedSensor
{
    public (int x, int y) Sensor;
    public (int x, int y) Beacon;

    public int DistanceToSensor()
    {
       return Math.Abs(Sensor.x - Beacon.x) + Math.Abs(Sensor.y - Beacon.y);
    }
    
    public int DistanceToPos(int x, int y)
    {
        return Math.Abs(Sensor.x - x) + Math.Abs(Sensor.y - y);
    }

    public bool PosIsNotWithinDistance((int x, int y) pos)
    {
        return DistanceToPos(pos.x, pos.y) > DistanceToSensor();
    }
    
}