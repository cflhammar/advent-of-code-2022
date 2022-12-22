namespace aoc_2022.Days.Dec22;

public class CubicMonkeyMap2
{
    private readonly List<List<char>> _map;
    private readonly string _instructions;
    private int _instructionCounter;
    private int _direction;
    private int _x;
    private int _y;
    
    
    public CubicMonkeyMap2(List<List<char>> inputMap, string instruction)
    {
        _map = inputMap;
        _instructions = instruction;
        
        FindStart();
    }

    public int FollowInstructions()
    {
        while (_instructionCounter < _instructions.Length)
        {
              var direction = GetNextInstruction();
              Move(direction);
        }
        
        var n = 0;
        if (_direction == 90) n = 3;
        if (_direction == 180) n = 2;
        if (_direction == 270) n = 1;

        return ((_y + 1) * 1000 + (_x + 1) * 4 + n);
    }

    private void Move(dynamic direction)
    {
    
        if (direction is int)
        {
            for (int i = 0; i < direction; i++)
            {
                MoveForward();
            }
        }
        else
        {
            switch (direction)
            {
                case "R":
                    RotateRight();
                    break;
                case "L":
                    RotateLeft();
                    break;
            }
        }
    }

    private void MoveForward()
    {
        var fourthY = _map.Count / 4;
        var thirdX = _map.First().Count / 3;
        
        switch (_direction)
        {
            // moving right
            case 0:
                if (_x + 1 < _map.First().Count && _map[_y][_x + 1] == '.')
                {
                    _x++;
                }
                else if (_x + 1 < _map.First().Count && _map[_y][_x + 1] == '#')
                {
                    break;
                }
                else
                {

                    // 2 to 4
                    if (_y < fourthY)
                    {
                        var newY = fourthY - _y - 1+ 2 * fourthY;
                        var newX = thirdX * 2 - 1;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();
                        RotateRight();

                        _x = newX;
                        _y = newY;
                    }
                    // 3 to 2
                    else if ( _y >= fourthY && _y < fourthY * 2)
                    {
                        var newY = fourthY - 1;
                        var newX = thirdX + _y;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();

                        _x = newX;
                        _y = newY;
                        
                    }
                    // 4 to 2
                    else if ( _y >= fourthY * 2 && _y < fourthY * 3)
                    {
                        var newY = fourthY * 3 - 1 - _y;
                        var newX = thirdX * 3 - 1;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();
                        RotateLeft();

                        _x = newX;
                        _y = newY;
                    }
                    // 6 to 4
                    else if (_y >= fourthY * 3)
                    {
                        var newY = fourthY * 3 - 1;
                        var newX = thirdX + _y - 3 * fourthY;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();

                        _x = newX;
                        _y = newY;
                    }

                }

                break;
            
            // moving left
            case 180:
                if (_x -1 >= 0 && _map[_y][_x - 1] == '.')
                {
                    _x--;
                }
                else if (_x -1 >= 0 && _map[_y][_x - 1] == '#')
                {
                    break;
                }
                else
                {

                    // 1 to 5
                    if (_y < fourthY)
                    {
                        var newY= fourthY - 1 - _y + 2 * fourthY;
                        var newX = 0;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();
                        RotateLeft();

                        _x = newX;
                        _y = newY;
                    }
                    // 3 to 5
                    else if ( _y >= fourthY && _y < fourthY * 2)
                    {
                        var newY= fourthY * 2;
                        var newX = _y - fourthY;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();

                        _x = newX;
                        _y = newY;
                    }
                    // 5 to 1
                    else if ( _y >= fourthY * 2 && _y < fourthY * 3)
                    {
                        var newY = fourthY * 3 - 1 - _y ;
                        var newX = thirdX;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();
                        RotateRight();

                        _x = newX;
                        _y = newY;
                    }
                    // 6 to 1
                    else if (_y >= fourthY * 3)
                    {
                        var newY = 0;
                        var newX = thirdX + _y - fourthY * 3 ;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();
                        RotateRight();
                        RotateRight();

                        _x = newX;
                        _y = newY;
                    }
                }

                break;
            
            // moving Up
            case 90:
                
                if (_y- 1 >= 0 && _map[_y-1][_x] == '.')
                {
                    _y--;
                }
                else if ( _y- 1 >= 0 && _map[_y-1][_x] == '#')
                {
                    break;
                }
                else
                {

                    // 5 to 3
                    if (_x < thirdX)
                    {
                        var newY = _x + fourthY;
                        var newX = thirdX ;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();

                        _x = newX;
                        _y = newY;
                    }
                    // 1 to 6
                    else if ( _x >= thirdX && _x < thirdX * 2 )
                    {
                        var newY = fourthY * 3 + (_x - thirdX);
                        var newX = 0;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();
                        RotateLeft();
                        RotateLeft();

                        _x = newX;
                        _y = newY;
                    }
                    // 2 to 6
                    else if ( _x >= thirdX * 2)
                    {
                        var newY = fourthY * 4 -1;
                        var newX = _x - thirdX * 2;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateRight(); 
                        RotateRight();
                        RotateRight();
                        RotateRight();
                        
                        _x = newX;
                        _y = newY;
                    }
                }
                break;
            
            // moving down
            case 270:
                if (_y + 1 < _map.Count && _map[_y+1][_x] == '.')
                {
                    _y++;
                }
                else if (_y + 1 < _map.Count && _map[_y+1][_x] == '#')
                {
                    break;
                }
                else
                {

                    // 6 to 2 
                    if (_x < thirdX)
                    {
                        var newY = 0;
                        var newX = thirdX * 2 + _x;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateLeft(); 
                        RotateLeft();
                        RotateLeft();
                        RotateLeft();
                        
                        _x = newX;
                        _y = newY;
                    }
                    // 4 to 6
                    else if ( _x >= thirdX && _x < thirdX * 2 )
                    {
                        var newY = _x - thirdX + fourthY * 3;
                        var newX = thirdX - 1;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateRight();
                        
                        _x = newX;
                        _y = newY;
                        
                    }
                    // 2 to 3
                    else if ( _x >= thirdX * 2)
                    {
                        var newY = _x - thirdX;
                        var newX = thirdX * 2 - 1;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateRight();
                        
                        _x = newX;
                        _y = newY;
                    }
                }
                break;
        }
    }

    private void RotateLeft()
    {
        _direction = (_direction + 90) % 360;
    }

    private void RotateRight()
    {
        _direction -= 90;
        if (_direction < 0) _direction = 360 + _direction;
    }
    
    

    void FindStart()
    {
        for (int findNewX = 0; findNewX < _map.Count; findNewX++)
        {
            if (_map[0][findNewX] == '.')
            {
                _x = findNewX;
                break;
            }
        }
    }
    
    
    private dynamic GetNextInstruction()
    {
        int num;
        if (_instructionCounter < _instructions.Length - 1 && int.TryParse(_instructions.Substring(_instructionCounter, 2), out num))
        {
            _instructionCounter++;
            _instructionCounter++;
            return num;

        }
        else if (_instructionCounter < _instructions.Length && int.TryParse(_instructions.Substring(_instructionCounter, 1), out num))
        {
            _instructionCounter++;
            return num;
        }
        else if (_instructionCounter < _instructions.Length - 1)
        {
             var direction = _instructions.Substring(_instructionCounter, 1);
            _instructionCounter++;
            return direction;
        }

        return 0;
    }


    void Print()
    {
        for (int i = 0; i < _map.Count; i++)
        { 
            var temp = ""; 
            for (int j = 0; j < _map.First().Count; j++)
            {
                if (i == _y && j == _x) temp += "x";
                else  temp += _map[i][j];
            }

            Console.WriteLine(temp);
        }
        Console.WriteLine();
    }
}
