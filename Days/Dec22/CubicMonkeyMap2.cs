using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec22;

public class CubicMonkeyMap2
{
    private List<List<char>> _map;
    private string _instrucs;
    private int _instrucsCounter = 0;
    private int _direction;
    private int x;
    private int y;
    
    
    public CubicMonkeyMap2(List<List<char>> inputmap, string instruction)
    {
        _map = inputmap;
        _instrucs = instruction;
        
        FindStart(); 
        FollowInstructions();

        var n = 0;
       if (_direction == 90) n = 3;
       if (_direction == 180) n = 2;
       if (_direction == 270) n = 1;
       

       Console.WriteLine((y+1)*1000 + (x+1) *4 + n );
    }

    public void FollowInstructions()
    {
        while (_instrucsCounter < _instrucs.Length)
        {
              var direction = GetNextInstruction();
              Console.WriteLine(direction);
              Move(direction);
            
        }
    }

    private void Move(dynamic direction)
    {
        if (direction is int)
        {
            for (int i = 0; i < direction; i++)
            {
                MoveForward();
              //  Console.WriteLine(_direction + ", x: " + x + ", y: " + y);
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
                if (x + 1 < _map.First().Count && _map[y][x + 1] == '.')
                {
                    x++;
                }
                else if (x + 1 < _map.First().Count && _map[y][x + 1] == '#')
                {
                    break;
                }
                else
                {
                    // 2 to 4
                    if (y < fourthY)
                    {
                        var newY = fourthY * 2 + (fourthY - 1 - x) ;
                        var newX = thirdX * 2 - 1;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();
                        RotateRight();

                        x = newX;
                        y = newY;
                        
                    }
                    // 3 to 2
                    if ( y >= fourthY && y < fourthY * 2)
                    {
                        var newY = fourthY - 1;
                        var newX = thirdX + y;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();

                        x = newX;
                        y = newY;
                        
                    }
                    // 4 to 2
                    if ( y >= fourthY * 2 && y < fourthY * 3)
                    {
                        var newY = fourthY * 3 - 1 - y;
                        var newX = thirdX * 3 - 1;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();
                        RotateLeft();

                        x = newX;
                        y = newY;
                        
                    }
                    // 6 to 4
                    if (y >= fourthY * 3)
                    {
                        var newY = fourthY * 3 - 1;
                        var newX = thirdX + y - 3 * fourthY;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();

                        x = newX;
                        y = newY;
                    }
                }

                break;
            
            // moving left
            case 180:
                if (x -1 >= 0 && _map[y][x - 1] == '.')
                {
                    x--;
                }
                else if (x -1 >= 0 && _map[y][x - 1] == '#')
                {
                    break;
                }
                else
                {
                    // 1 to 5
                    if (y < fourthY)
                    {
                        var newY= fourthY - y + 2 * fourthY;
                        var newX = 0;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();
                        RotateLeft();

                        x = newX;
                        y = newY;
                        
                    }
                    // 3 to 5
                    if ( y >= fourthY && y < fourthY * 2)
                    {
                        var newY= fourthY * 2;
                        var newX = y - fourthY;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();

                        x = newX;
                        y = newY;
                    }
                    // 5 to 1
                    if ( y >= fourthY * 2 && y < fourthY * 3)
                    {
                        var newY = fourthY * 3 - 1 - y ;
                        var newX = thirdX;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();
                        RotateRight();

                        x = newX;
                        y = newY;
                    }
                    // 6 to 1
                    if (y >= fourthY * 3)
                    {
                        var newY = 0;
                        var newX = thirdX + y - fourthY * 3 ;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();
                        RotateRight();
                        RotateRight();

                        x = newX;
                        y = newY;
                    }
                }

                break;
            
            // moving Up
            case 90:
                
                if (y- 1 >= 0 && _map[y-1][x] == '.')
                {
                    y--;
                }
                else if ( y- 1 >= 0 && _map[y-1][x] == '#')
                {
                    break;
                }
                else
                {
                    // 5 to 3
                    if (x < thirdX)
                    {
                        var newY = x + fourthY;
                        var newX = thirdX ;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateRight();

                        x = newX;
                        y = newY;
                    }
                    // 1 to 6
                    if ( x >= thirdX && x < thirdX * 2 )
                    {
                        var newY = fourthY * 3 + (x - thirdX);
                        var newX = 0;
                        if (_map[newY][newX] == '#') return;
                    
                        RotateLeft();
                        RotateLeft();
                        RotateLeft();

                        x = newX;
                        y = newY;
                    }
                    // 2 to 6
                    if ( x >= thirdX * 2)
                    {
                        var newY = fourthY * 4 -1;
                        var newX = x - thirdX * 2;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateRight(); 
                        RotateRight();
                        RotateRight();
                        RotateRight();
                        
                        x = newX;
                        y = newY;
                    }
                
                }
                break;
            // moving down
            case 270:
                if (y + 1 < _map.Count && _map[y+1][x] == '.')
                {
                    y++;
                }
                else if (y + 1 < _map.Count && _map[y+1][x] == '#')
                {
                    break;
                }
                else
                {
                    // 6 to 2 
                    if (x < thirdX)
                    {
                        var newY = 0;
                        var newX = thirdX * 2 + x;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateLeft(); 
                        RotateLeft();
                        RotateLeft();
                        RotateLeft();
                        
                        x = newX;
                        y = newY;
                    }
                    // 4 to 6
                    if ( x >= thirdX && x < thirdX * 2 )
                    {
                        var newY = x - thirdX + fourthY * 3;
                        var newX = thirdX - 1;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateRight();
                        
                        x = newX;
                        y = newY;
                        
                    }
                    // 2 to 3
                    if ( x >= thirdX * 2)
                    {
                        var newY = x - thirdX;
                        var newX = thirdX * 2 - 1;
                        if (_map[newY][newX] == '#') return;
                        
                        RotateRight();
                        
                        x = newX;
                        y = newY;
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
                x = findNewX;
                break;
            }
        }
    }
    
    
    private dynamic GetNextInstruction()
    {
        int num;
        if (_instrucsCounter < _instrucs.Length - 1 && int.TryParse(_instrucs.Substring(_instrucsCounter, 2), out num))
        {
            _instrucsCounter++;
            _instrucsCounter++;
            return num;

        }
        else if (_instrucsCounter < _instrucs.Length && int.TryParse(_instrucs.Substring(_instrucsCounter, 1), out num))
        {
            _instrucsCounter++;
            return num;
        }
        else if (_instrucsCounter < _instrucs.Length - 1)
        {
             var direction = _instrucs.Substring(_instrucsCounter, 1);
            _instrucsCounter++;
            return direction;
        }

        return 0;
    }


    void Print()
    {
        foreach (var row in _map)
        {
            var temp = string.Join("", row);
            Console.WriteLine(temp);
        }
        
        Console.WriteLine();
    }
}

// 100005
// 101781
// 85061
// 15550
// 15546

// 15266

// 81306
// not guessed: 81482

// 140160
// not guessed 140244
// not guessed 140360

// 145406
// 155080


// 73242

// 35294