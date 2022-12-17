namespace aoc_2022.Days.Dec17;
public class Tetris
{
    private List<List<string>> _board = new List<List<string>>();
    private readonly Dictionary<int, List<(int x, int y)>> _shapes = new();
    private readonly List<char> _jetStream;
    private int _onJetNumber = -1;
    private int _currentHeight;
    private long _totalHeightGained;
    private Dictionary<string, (long round, long top)> _memory = new();
    private bool _cycleIsFound;
    
    public Tetris(string input)
    {
        for (int row = 0; row < 7; row++)
        {
            _board.Add(new List<string>());
            for (int y = 0; y < 10; y++)
            {
                _board[row].Add(".");
            }
        }

        _jetStream = input.ToList();
        
        var minus = new List<(int x, int y)>()
        {
            (0, 0), (1, 0), (2, 0), (3, 0)
        };
        var plus = new List<(int x, int y)>()
        {
            (0, 1), (1, 1), (1, 0), (2, 1), (1, 2)
        };
        var rL = new List<(int x, int y)>()
        {
            (0, 0), (1, 0), (2, 0), (2, 1), (2, 2)
        };
        var i = new List<(int x, int y)>()
        {
            (0, 0), (0, 1), (0, 2), (0, 3)
        };
        
        var box = new List<(int x, int y)>()
        {
            (0, 0), (0, 1), (1, 0), (1, 1)
        };
        
        _shapes.Add(0,minus);
        _shapes.Add(1,plus);
        _shapes.Add(2,rL);
        _shapes.Add(3,i);
        _shapes.Add(4,box);
    }
    

    public long Play(long rounds)
    {
        for (long i = 0; i < rounds; i++)
        {
            var shapeNumber = i % _shapes.Count;
            DropOneRock(shapeNumber);
            
            var linesRemoved = DeleteBoardSpaceBelowIfHeightIsGreaterThan(100);
            _currentHeight -= linesRemoved;
            
            var currentState = new State()
            {
                JetNumber = _onJetNumber,
                ShapeNumber = (int) shapeNumber,
                ColumnHeights = GetBoardColumnHeights()
            };

            var currentStateHash = currentState.ToString();

            if (_cycleIsFound) continue;
            else
            {
                if (!_memory.ContainsKey(currentStateHash))
                {
                    _memory.Add(currentStateHash, (i, _totalHeightGained));
                }
                else
                {
                    var cycleSize = i - _memory[currentStateHash].round;
                    var heightGainedInCycle = _totalHeightGained - _memory[currentStateHash].top;

                    long remainingRounds = rounds - i;
                    long remainingCycles = remainingRounds / cycleSize;

                    var heightAfterCycles = _totalHeightGained + remainingCycles * heightGainedInCycle;
                    _totalHeightGained = heightAfterCycles;

                    i += remainingCycles * cycleSize;
                    _cycleIsFound = true;
                }
            }
        }
        
        return _totalHeightGained;
    }

    
    private void DropOneRock(long shapeNumber)
    {
        var rockShape = _shapes[(int) shapeNumber];
        var dropFromHeight =  _currentHeight + 3 ;

        AddBoardSpaceAboveIfNeeded();
        
       List<(int x , int y)> placedShape = rockShape.Select(p=> (p.x + 2, p.y + dropFromHeight)).ToList();
       
        while (true)
        {
            var jet = GetJetStream();

            if (jet == '>' && CanMoveRight(placedShape))
            {
                placedShape = placedShape.Select( p=> (p.x + 1, p.y)).ToList();
            }
            else if (jet == '<' && CanMoveLeft(placedShape))
            {
                placedShape = placedShape.Select( p=> (p.x -1, p.y)).ToList();
            }
            
            if (CanMoveDown(placedShape))
            {
                placedShape = placedShape.Select(p => (p.x, p.y - 1)).ToList();
            }
            else
            {
                break;
            }
        }

        PlaceRock(placedShape);
        
        var max = placedShape.Max(z => z.y) + 1;
        if (max > _currentHeight)
        {
            var heightAdded = max - _currentHeight;
            if (heightAdded > 0)
            {
                _totalHeightGained += heightAdded;
            }
            
            _currentHeight = max;
        }
    }

    private void PlaceRock( List<(int x, int y)> placedShape)
    {
        foreach (var (x, y) in placedShape)
        {
            _board[x][y] = "x";
        }
    }
    
    private char GetJetStream()
    {
        _onJetNumber++;
        _onJetNumber = _onJetNumber % _jetStream.Count;
        return  _jetStream[_onJetNumber];
    }

    private void AddBoardSpaceAboveIfNeeded()
    {
        if (_board.First().Count - _currentHeight < 7)
        {
            for (int row = 0; row < 7; row++)
            {
                for (int y = 0; y < 7; y++)
                {
                    _board[row].Add(".");
                }
            }
        }
    }
    
    private int DeleteBoardSpaceBelowIfHeightIsGreaterThan(int boardMaxSize)
    {
        var remove = _board.First().Count - boardMaxSize;
        if (remove > 0)
        {
            var newBoard = new List<List<string>>();
            foreach (var column in _board)
            {
                var newCol = column.Skip(remove).ToList();
                newBoard.Add(newCol);
            }

            _board = newBoard;
        }
        
        return remove > 0 ? remove : 0;
    }
    

    private bool CanMoveDown(List<(int x, int y)> shape)
    {
        foreach (var (x, y) in shape)
        {
            if ( y - 1 < 0 || _board[x][y - 1] != "."  )
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMoveRight(List<(int x, int y)> shape)
    {
        foreach (var (x, y) in shape)
        {
            if ( x + 1 > 6 || _board[x + 1][y] != "."  )
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMoveLeft(List<(int x, int y)> shape)
    {
        foreach (var (x, y) in shape)
        {
            if ( x - 1 < 0 || _board[x - 1][y] != "."  )
            {
                return false;
            }
        }
        return true;
    }
    
    public class State
    {
        public int ShapeNumber;
        public int JetNumber;
        public List<int> ColumnHeights = new();
        
        public override string ToString()
        {
            return ShapeNumber + "." + JetNumber + "." + string.Join("-", ColumnHeights);
        }
    }
    
    public List<int> GetBoardColumnHeights()
    {
        var columnHeights = new List<int>();
        foreach (var column in _board)
        {
            var columnHeight = -1;

            var counter = 0;
            foreach (var e in column)
            {
                if (e == "x")
                {
                    columnHeight = counter;
                }
                counter++;
            }
            
            columnHeights.Add(columnHeight);
        }

        return columnHeights;
    }
    
    private void Print(List<(int x, int y)> shape)
    {
        Thread.Sleep(200);
        for (int i = _board.First().Count - 1; i >= 0 ; i--)
        {
            var temp = "   ";
            for (int j = 0; j < _board.Count; j++)
            {
                if (shape.Contains((j, i)))
                {
                    temp += "o ";
                }
                else
                {
                 temp += _board[j][i] + " ";
                    
                }
            }
            Console.WriteLine(temp);
        }
        Console.WriteLine(  );
    }
    
    private void Print()
    {
        Thread.Sleep(70);
        for (int i = _board.First().Count - 1; i >= 0 ; i--)
        {
            var temp = "    ";
            foreach (var t in _board)
            {
                temp += t[i];
            }
            Console.WriteLine(temp);
        }
        Console.WriteLine(  );
    }
}