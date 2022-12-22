namespace aoc_2022.Days.Dec22;

public class MonkeyMap
{
    private List<List<char>> _map;
    private string _instrucs;
    private int _instrucsCounter = 0;
    private int _direction;
    private int x;
    private int y;
    
    
    public MonkeyMap(List<List<char>> inputmap, string instruction)
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
            }
        }
        else
        {
            switch (direction)
            {
                case "R":
                    _direction -= 90;
                    if (_direction < 0) _direction = 360 + _direction;
                    break;
                case "L":
                    _direction = (_direction + 90) % 360;
                    break;
            }
        }
    }

    private void MoveForward()
    {
        switch (_direction)
        {
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
                    for (int findNewX = 0; findNewX < x; findNewX++)
                    {
                        if (_map[y][findNewX] == '.')
                        {
                            x = findNewX;
                            break;
                        }
                        else if (_map[y][findNewX] == '#')
                        {
                            return;
                        }
                    }
                } 
                break;
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
                    for (int findNewX = _map.First().Count-1; findNewX > x; findNewX--)
                    {
                        if (_map[y][findNewX] == '.')
                        {
                            x = findNewX;
                            break;
                        }
                        else if (_map[y][findNewX] == '#')
                        {
                            return;
                        }
                    }
                }
                break;
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
                    for (int findNewY = _map.Count-1; findNewY >= 0; findNewY--)
                    {
                        if (_map[findNewY][x] == '.')
                        {
                            y = findNewY;
                            break;
                        }
                        else if (_map[findNewY][x] == '#')
                        {
                            return;
                        }
                    }
                }   
                break;
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
                    for (int findNewY = 0; findNewY < y; findNewY++)
                    {
                        if (_map[findNewY][x] == '.')
                        {
                            y = findNewY;
                            break;
                        } else if (_map[findNewY][x] == '#')
                        {
                            return;
                        }
                    }
                }    
                break;
        }
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