using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec22;

public class CubicMonkeyMap
{
    private List<List<char>> _map;
    private string _instrucs;
    private int _instrucsCounter = 0;
    private int _direction;
    private int x;
    private int y;
    
    
    public CubicMonkeyMap(List<List<char>> inputmap, string instruction)
    { 
        var helper = new MatrixOperations();
        _map = helper.Rotate90(inputmap);
        _map = helper.Rotate90(_map);
        _map = helper.Rotate90(_map);
        
        
        _instrucs = instruction;
        
       //FindStart();
       x = 0;
       y = 99;
       _direction = 90;
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
                Console.WriteLine(_direction + ", x: " + x + ", y: " + y);
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
        var thirdY = _map.Count / 3;
        var fourthX = _map.First().Count / 4;
        
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
                    // from 2 to 3
                    if (y < thirdY && x < fourthX)
                    {
                        var newY = thirdY;
                        var newX = fourthX + (thirdY - 1 - y);
                        if (_map[newY][newX] == '0') return;
                    
                        RotateRight();

                        x = newX;
                        y = newY;
                    } 
                    // from 4 to 6
                    else if( y >= thirdY && y < thirdY * 2 && x >= 2 * fourthX && x < fourthX * 3)
                    {
                        var newY = thirdY * 2;
                        var newX = 3 * fourthX + ( thirdY * 2 - y - 1);
                        
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateRight();
                    
                        x = newX;
                        y = newY;
                    }
                    else
                        // from 6 to 2
                    {
                        var newY = y - thirdY * 3 ;
                        var newX = 0;
                        
                        if (_map[newY][newX] == 'o') return;
                        
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
                
                    // from 2 to 6
                    if (y < thirdY && x < fourthX)
                    {
                        var newY =  thirdY * 2 + x;
                        var newX = fourthX * 4 - 1;
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        // not needed ??
                        
                        x = newX;
                        y = newY;
                    }
                    // from 1 to 6  (old 2 to 6 not changed)
                    else if (y >= thirdY && y < thirdY * 2 && x < fourthX)
                    {
                        var newY = _map.Count - 1;
                        var newX = 3 * fourthX + (2 * thirdY - 1 - y);
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateLeft();
                        RotateLeft();
                        RotateLeft();

                        x = newX;
                        y = newY;
                    }
                    // from 5 to 3
                    else
                    {
                        var newY = thirdY * 2 - 1;
                        var newX = fourthX + (3 * thirdY - y);
                    
                        if (_map[newY][newX] == 'o') return;
                    
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
                    //  2 to 4  (old 1 to 2)
                    if (y < thirdY && x < fourthX)
                    {
                        var newY = thirdY;
                        var newX = 2 * fourthX + fourthX - 1 - x; 
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateLeft();
                        RotateLeft();
                    
                        x = newX;
                        y = newY;
                    }
                
                    //  4 to 2  ( old from 2 to 1)
                    if (x >= fourthX * 2 && x < fourthX * 3 && y >= thirdY  && y < thirdY * 2)
                    {
                        var newY = 0;
                        var newX = (fourthX * 3 - x - 1); 
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateRight();
                        RotateRight();
                    
                        x = newX;
                        y = newY;
                    }
                    
                    //   3 to 2 (old 3 to 1)
                    if (x >= fourthX && x < fourthX * 2 &&  y >= thirdY  && y < thirdY * 2)
                    {
                        var newY = fourthX * 2 - 1 - x ;
                        var newX = fourthX - 1;
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateRight();
                    
                        x = newX;
                        y = newY;
                    }
                    
                    // 6 to 4
                    if (x >= 3 * fourthX && y >= thirdY * 3 )
                    {
                        var newY = thirdY * 2 + (fourthX * 4 - 1 - x); 
                        var newX = 3 * fourthX - 1;
                    
                        if (_map[newY][newX] == 'o') return;
                    
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
                    // 2 to 5 
                    if (x < fourthX  && y >= thirdY  && y < thirdY * 2)
                    {
                        var newY = 3 * thirdY -1; 
                        var newX = 2 * fourthX + (fourthX - 1 - x);
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateLeft();
                        RotateLeft();

                        x = newX;
                        y = newY;
                    }
                    
                    // 5 to 2
                    if (x >= fourthX * 2 && x < fourthX * 3  && y >= thirdY * 2 )
                    {
                        var newY = 2 * thirdY -1; 
                        var newX = 3 * fourthX - 1 - x ;
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateRight();
                        RotateRight();

                        x = newX;
                        y = newY;
                    }
                    
                    // 3 to 5
                    if (x >= fourthX && x < fourthX * 3  && y >= thirdY && y < thirdY * 2  )
                    {
                        var newY = 2 * thirdY + (2 * fourthX - 1 - x); 
                        var newX = 2 * fourthX ;
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateLeft();

                        x = newX;
                        y = newY;
                    }
                    
                    // 6 to 2
                    if (x >= fourthX * 3 && y >= thirdY * 2)
                    {
                        var newY = thirdY + fourthX * 4 - 1 - x; 
                        var newX = 0;
                    
                        if (_map[newY][newX] == 'o') return;
                    
                        RotateLeft();

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
}

// 100005