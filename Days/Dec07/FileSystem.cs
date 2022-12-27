namespace aoc_2022.Days.Dec07;

public class FileSystem
{
    private Directory? _root = new(){Name = "root"};
    
   private List<int> _folderSizes = new (); 

    public int SumOfAllDirsBelowSize(int size)
    {
        _folderSizes.Add(SizeOfDirAndItsContents(_root));
        return _folderSizes.Where(x => x < size).Sum();
    }

    public int FreeUpSpace(int size)
    {
        var currentlyFree = 70000000 - _folderSizes.Last();

        var minFolderSize = 70000000; 
        foreach (var folder in _folderSizes)
        {
            if (currentlyFree + folder > size && folder < minFolderSize) minFolderSize = folder;
        }
        
        return  minFolderSize;
    }

    private int SizeOfDirAndItsContents(Directory? dir)
    {
        var sizeOfFiles = dir!.Files.Select(d => d.Size).Sum();
        var sizeOfSubFolders = dir.SubDir.Select(SizeOfDirAndItsContents).Sum();
        
        _folderSizes.Add( sizeOfFiles+sizeOfSubFolders);

        return sizeOfFiles + sizeOfSubFolders;
    }
    
    public void CreateFileSystem(List<string> commands)
    {
        var currentFolder = _root;
        
        for  (int i = 0; i < commands.Count; i++)
        {
            if (commands[i] == "$ cd /") currentFolder = _root;
            
            else if (commands[i] == "$ cd ..")
            {
                currentFolder = currentFolder?.RootDir;
            }
            
            else if (commands[i].Contains("$ cd "))
            {
                currentFolder = currentFolder?.SubDir.First(x => x.Name == commands[i].Split(" ")[2]);
            }
            
            else if (commands[i] == "$ ls")
            {
                for (int j = i + 1; j < commands.Count; j++)
                {
                    if (commands[j].Contains(" cd "))
                    {
                        i = j-1;
                        break;
                    }
                    
                    if (commands[j].Contains("dir"))
                    {
                        var dir = new Directory()
                        {
                            Name = commands[j].Split(" ")[1],
                            RootDir = currentFolder!
                        };
                        currentFolder?.SubDir.Add(dir);
                    }
                    
                    else
                    {
                        var file = new Fyle() {Size = Int32.Parse(commands[j].Split(" ").First())};
                        currentFolder?.Files.Add(file);
                    }
                }
            }
        }
    }
}