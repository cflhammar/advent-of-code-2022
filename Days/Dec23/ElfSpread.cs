namespace aoc_2022.Days.Dec23;

public class ElfSpread
{
    private Dictionary<int,(int x, int y)> _elfes = new();
    private int _currentMovement;
    
    public ElfSpread(List<string> input)
    {
        var elfIndex = 0;
        for (int i = 0; i < input.First().Length; i++)
        {
            for (int j = 0; j < input.Count; j++)
            {
                if (input[j][i] =='#')
                {
                    _elfes.Add(elfIndex, (i, j));
                    elfIndex++;
                }
            }
        }
    }
    
    public int PlayUntilStop()
    {
        var index = 0;
        var proceed = true;
        
        while (proceed)
        {
            proceed = Play();
            index++;
        }
        
        return index;
    }
    
    public int PlayRounds(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            Play();
        }
        
        return CountElves();
    }

    bool Play()
    {
        Dictionary<int,(int x, int y)> proposedMovements = new();

        var elvesThatWantToMove = 0;
        foreach (var (elfIndex, elf) in _elfes)
        {
            if (ElfWantToMove(elf))
            {
                elvesThatWantToMove++;
                
                switch (_currentMovement)
                {
                    case 0:
                        if (ElfCanMoveNorth(elf)) proposedMovements.Add(elfIndex,IfElfMovesNorth(elf));
                        else if (ElfCanMoveSouth(elf)) proposedMovements.Add(elfIndex,IfElfMovesSouth(elf));
                        else if (ElfCanMoveWest(elf)) proposedMovements.Add(elfIndex,IfElfMovesWest(elf));
                        else if (ElfCanMoveEast(elf)) proposedMovements.Add(elfIndex,IfElfMovesEast(elf));
                    
                        break;
                    case 1:
                        if (ElfCanMoveSouth(elf)) proposedMovements.Add(elfIndex,IfElfMovesSouth(elf));
                        else if (ElfCanMoveWest(elf)) proposedMovements.Add(elfIndex,IfElfMovesWest(elf));
                        else if (ElfCanMoveEast(elf)) proposedMovements.Add(elfIndex,IfElfMovesEast(elf));
                        else if (ElfCanMoveNorth(elf)) proposedMovements.Add(elfIndex,IfElfMovesNorth(elf));
                    
                        break;
                    case 2:
                        if (ElfCanMoveWest(elf)) proposedMovements.Add(elfIndex,IfElfMovesWest(elf));
                        else if (ElfCanMoveEast(elf)) proposedMovements.Add(elfIndex,IfElfMovesEast(elf));
                        else if (ElfCanMoveNorth(elf)) proposedMovements.Add(elfIndex,IfElfMovesNorth(elf));
                        else if (ElfCanMoveSouth(elf)) proposedMovements.Add(elfIndex,IfElfMovesSouth(elf));
                    
                        break;
                    case 3: 
                        if (ElfCanMoveEast(elf)) proposedMovements.Add(elfIndex,IfElfMovesEast(elf));
                        else if (ElfCanMoveNorth(elf)) proposedMovements.Add(elfIndex,IfElfMovesNorth(elf));
                        else if (ElfCanMoveSouth(elf)) proposedMovements.Add(elfIndex,IfElfMovesSouth(elf));
                        else if (ElfCanMoveWest(elf)) proposedMovements.Add(elfIndex,IfElfMovesWest(elf));
                    
                        break;
                }
            }
        }

        if (elvesThatWantToMove == 0) return false;
        
        // get duplicates from proposed movements
        var noOneCanMoveTo = proposedMovements.Values.GroupBy(s => s).SelectMany(grp => grp.Skip(1)).ToList();
        
        foreach (var (index, movedElf) in proposedMovements)
        {
            if (!noOneCanMoveTo.Contains(movedElf)) _elfes[index] = movedElf;
        }
        
        IncreaseMovement();
        return true;
    }

    private bool ElfWantToMove((int x, int y) elf)
    { 
        List<(int dx, int dy)> neighbours = new()
    {
        (0,1), (0,-1), (-1,0), (1,0), 
        (1,1), (-1,-1), (-1,1), (1,-1)
    };

        foreach (var (dx, dy) in neighbours)
        {
            if (_elfes.ContainsValue((elf.x + dx, elf.y + dy)))
            {
                return true;
            }
        }

        return false;
    }

    private static (int x, int y) IfElfMovesNorth((int x, int y) elf)
    {
        return (elf.x, elf.y - 1);
    }

    private static (int x, int y) IfElfMovesSouth((int x, int y) elf)
    {
        return (elf.x, elf.y + 1);
    }

    private static (int x, int y) IfElfMovesWest((int x, int y) elf)
    {
        return (elf.x - 1, elf.y);
    }

    private static (int x, int y) IfElfMovesEast((int x, int y) elf)
    {
        return (elf.x + 1, elf.y);
    }
    
    bool ElfCanMoveNorth((int x, int y) elf)
    {
        return !_elfes.ContainsValue((elf.x, elf.y - 1)) && !_elfes.ContainsValue((elf.x + 1, elf.y - 1)) && !_elfes.ContainsValue((elf.x - 1, elf.y - 1)) ;
    }
    
    bool ElfCanMoveSouth((int x, int y) elf)
    {
        return !_elfes.ContainsValue((elf.x, elf.y + 1)) && !_elfes.ContainsValue((elf.x + 1, elf.y + 1)) && !_elfes.ContainsValue((elf.x - 1, elf.y + 1)) ;
    }
    
    bool ElfCanMoveWest((int x, int y) elf)
    {
        return !_elfes.ContainsValue((elf.x - 1, elf.y)) && !_elfes.ContainsValue((elf.x - 1, elf.y + 1)) && !_elfes.ContainsValue((elf.x - 1, elf.y - 1)) ;
    }
    
    bool ElfCanMoveEast((int x, int y) elf)
    {
        return !_elfes.ContainsValue((elf.x + 1, elf.y)) && !_elfes.ContainsValue((elf.x + 1, elf.y + 1)) && !_elfes.ContainsValue((elf.x + 1, elf.y - 1)) ;
    }

    void IncreaseMovement()
    {
        _currentMovement = (_currentMovement + 1) % 4;
    }

    int CountElves()
    {
        (int min, int max) xRange = (_elfes.Values.Select(e => e.x).Min(), _elfes.Values.Select(e => e.x).Max());
        (int min, int max) yRange = (_elfes.Values.Select(e => e.y).Min(), _elfes.Values.Select(e => e.y).Max());
        
        var sum = 0;
        for (int x = xRange.min; x <= xRange.max; x++)
        {
            for (int y = yRange.min; y <= yRange.max; y++)
            {
                if (!_elfes.Values.Contains((x, y))) sum++;
            }
        }

        return sum;
    }
    
    void Print()
    {
        for (int y = -5; y < 15; y++)
        {
            var temp = "";
            for (int x = -5; x < 15; x++)
            {
                if (_elfes.ContainsValue((x, y))) temp += "#";
                else temp += ".";
            }
            Console.WriteLine(temp);
        }
        Console.WriteLine();
    }
}

/* some sort of que maybe to check movements more nice
                for (int i = 0; i < 4; i++)
                {
                    var func = (_currentMovement + i) % 4;
                    Console.WriteLine(func);
                    
                }
                Console.WriteLine();
  */