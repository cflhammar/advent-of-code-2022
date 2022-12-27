namespace aoc_2022.Days.Dec14;
public class SandCave
{
    private List<List<Type>> _cave2D = new();
    private int _yMax;

    public SandCave(List<List<List<int>>> input)
    {
        _yMax = GetMaxY(input);
        DrawLines(input, _yMax);
    }
    
    public int PourSandUntilFull(bool printTest)
    {
 
        var placementInfo = DropOneSand(0);
        var topOfTheHill = placementInfo.topOfTheHill;
        
        var partOne = false;
        
        while (topOfTheHill != -1)
        {
            placementInfo = DropOneSand(topOfTheHill);
            topOfTheHill = placementInfo.topOfTheHill;

            if (!partOne && placementInfo.heightOfSandPlaced == _yMax-1)
            {
                Console.WriteLine("Part 1: Size: " + (SumAll(printTest) - 1));
                partOne = true;
            }
        }
        
        return SumAll(printTest);
    }

    private (int topOfTheHill, int heightOfSandPlaced) DropOneSand(int topOfTheHill)
    {
        (int x, int y) sand = (500,0);

        while (true)
        {
            if (sand.y + 1 < _cave2D.First().Count)
            {
                if (_cave2D[sand.x][sand.y + 1].IsAir)
                {
                    sand = (sand.x, sand.y + 1);
                    if (sand.x == 500) topOfTheHill = sand.y;
                    continue;
                }
                else if (_cave2D[sand.x-1][sand.y + 1].IsAir)
                {
                    sand = (sand.x - 1, sand.y + 1);
                    continue;
                }
                else if (_cave2D[sand.x+1][sand.y + 1].IsAir)
                {
                    sand = (sand.x + 1, sand.y + 1);
                    continue;
                }
                else if ((sand.x == 500)) topOfTheHill--;
            }
            break;
        }
        
        _cave2D[sand.x][sand.y].ToSand();
        return (topOfTheHill, sand.y);
    }


    private void DrawLine(List<int> point1, List<int> point2)
    {

        var xFrom = Math.Min(point1[0], point2[0]);
        var xTo = Math.Max(point1[0], point2[0]);
        var yFrom = Math.Min(point1[1], point2[1]);
        var yTo = Math.Max(point1[1], point2[1]);
 
        for (int x = xFrom; x <= xTo; x++)
        {
            for (int y = yFrom; y <= yTo; y++)
            {
                _cave2D[x][y].ToRock();
            }
        }
    }

    private int SumAll(bool printTest)
    {
        var sum = 0;
        for (int i = 0; i < _cave2D.Count;  i++)
        {
            for (int a = 0 ; a < _cave2D.First().Count; a++)
            {
                if (_cave2D[i][a].IsSand)
                {
                    sum++;
                }
            }
        }

        if (printTest) Print();
        return sum;
    }

    private void Print()
    {
        Thread.Sleep(70);
        var a = _cave2D.Skip(485).Take(30).ToList();
        for (int i = 0; i < a.First().Count; i++)
        { 
                var temp = new List<Type>();
                
                foreach (var row in a)
                {
                    temp.Add(row[i]);
                }
                var c = string.Join("", temp);
                Console.WriteLine(c);
        }
    }


    private void DrawLines(List<List<List<int>>> input, int yMax)
    {
        for (int x = 0; x < 1000; x++)
        {
            _cave2D.Add(new List<Type>());
            for (int y = 0; y < yMax; y++)
            {
                _cave2D[x].Add(new Type());
            }
        }

        foreach (var longLine in input)
        {
            for (int i = 0; i < longLine.Count - 1; i++)
            {
                DrawLine(longLine[i], longLine[i+1]);
            }
        }
    }

    private int GetMaxY(List<List<List<int>>> input)
    {
        var maxy = 0;
        foreach (var longLine in input)
        {
            foreach (var point in longLine)
            {
                if (point[1] > maxy) maxy = point[1];
            }
        }

        return maxy+2;
    }
}