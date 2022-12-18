namespace aoc_2022.Days.Dec18;

public class LavaInWater
{
    readonly HashSet<(int x, int y, int z)> _lava = new ();
    private readonly (int min, int max) _xRange;
    private readonly (int min, int max) _yRange;
    private readonly (int min, int max) _zRange;

    private readonly List<(int x, int y, int z)> _neighbours = new List<(int x, int y, int z)>()
    {
        (1, 0, 0),
        (-1, 0, 0),
        (0, 1, 0),
        (0, -1, 0),
        (0, 0, 1),
        (0, 0, -1)
    };
    
    public LavaInWater(List<List<string>> input)
    {
        foreach (var point in input)
        {
            _lava.Add(( Int32.Parse(point[0]), Int32.Parse(  point[1]), Int32.Parse( point[2])));
        }
        
        _xRange =(_lava.Min(p => p.x) - 1,  _lava.Max(p => p.x) + 1);
        _yRange =(_lava.Min(p => p.y) - 1,  _lava.Max(p => p.y) + 1);
        _zRange =(_lava.Min(p => p.z) - 1,  _lava.Max(p => p.z) + 1);
    }

    public int WaterArea()
    {
        var water =   FillWithWater((_xRange.min, _yRange.min, _zRange.min));
        var sum = 0;
        
        foreach (var lava in _lava)
        {
            foreach (var d in _neighbours)
            {
                var cube = (lava.x + d.x, lava.y + d.y, lava.z + d.z);
                if (water.Contains(cube)) sum++;
            }
        }
     
        return sum;
    }

    private HashSet<(int x, int y, int z)> FillWithWater((int x, int y, int z) start)
    {
        var water = new HashSet<(int x, int y, int z)>();
        var q = new Queue<(int x, int y, int z)>();

        water.Add(start);
        q.Enqueue(start);

        while (q.Any())
        {
            var waterCube = q.Dequeue();
            foreach (var d in _neighbours)
            {
                var next = (waterCube.x + d.x, waterCube.y + d.y, waterCube.z + d.z);

                if (!water.Contains(next) && IsWithinRange(next) && !_lava.Contains(next))
                {
                    water.Add(next);
                    q.Enqueue(next);
                }
            }
        }

        return water;
    }

    private bool IsWithinRange((int x, int y, int z) p)
    {
        return (p.x >= _xRange.min && p.x <= _xRange.max &&
                p.y >= _yRange.min && p.y <= _yRange.max &&
                p.z >= _zRange.min && p.z <= _zRange.max);
    }

    public int FreeSides()
  {
      var sum = 0;
      foreach (var p in _lava)
      {
          foreach (var n in _neighbours)
          {
              var checkPos = (p.x + n.x, p.y + n.y, p.z + n.z);
              if (!_lava.Contains(checkPos))
              {
                  sum++;
              }
          }
      }

      return sum;
  }
}