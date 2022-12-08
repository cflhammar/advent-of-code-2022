using System.Data;

namespace aoc_2022.Days.Dec08;

public class Forest
{
    public List<List<int>> Trees = new ();

    public Forest(List<List<int>> trees)
    {
        Trees = trees;
    }

    public (int totalVisible, int scenicScore) CountVisibleTrees()
    {
        var max = 0;
        var visible = 0;
        for (int i = 1; i < Trees.Count-1; i++)
        {
            for (int j = 1; j < Trees.First().Count-1; j++)
            {
                var tree = TreeIsVisible(i, j);
                if (tree.isVisible) visible++;
                var scenicScore = tree.scenicScore;
                if (scenicScore > max) max = scenicScore;
            }
        }
        
        return (visible + Trees.First().Count * 4 - 4, max);
    }

    public (bool isVisible, int scenicScore) TreeIsVisible(int row, int col)
    {
        var above = TreeIsVisibleFromAndTreesVisibleToAbove(row, col);
        var below = TreeIsVisibleFromAndTreesVisibleToBelow(row, col);
        var left = TreeIsVisibleFromAndTreesVisibleToLeft(row, col);
        var right = TreeIsVisibleFromAndTreesVisibleToRight(row, col);

        var visible = above.isVisible || below.isVisible || right.isVisible || left.isVisible;
        
        return (visible, above.treesVisible * below.treesVisible * right.treesVisible * left.treesVisible);
    }

    public (bool isVisible, int treesVisible) TreeIsVisibleFromAndTreesVisibleToRight(int row, int col)
    {
        var visible = 0;
        for (int i = row + 1; i < Trees.Count; i++)
        {
            visible++;
            if (Trees[i][col] >= Trees[row][col]) return (false, visible);
        }
        return (true, visible);
    }
    
    public (bool isVisible, int treesVisible) TreeIsVisibleFromAndTreesVisibleToLeft(int row, int col)
    {
    var visible = 0;
        for (int i = row - 1; i >= 0; i--)
        {
            visible++;
            if (Trees[i][col] >= Trees[row][col]) return (false, visible);
        }
        return (true,visible);
    }
    
    public (bool isVisible, int treesVisible) TreeIsVisibleFromAndTreesVisibleToAbove(int row, int col)
    {
    var visible = 0;
        for (int i = col + 1; i < Trees.First().Count; i++)
        {
            visible++;
            if (Trees[row][i] >= Trees[row][col]) return (false, visible);
        }
        return (true,visible);
    }
    
    public (bool isVisible, int treesVisible) TreeIsVisibleFromAndTreesVisibleToBelow(int row, int col)
    {
    var visible = 0;
        for (int i = col - 1; i >= 0; i--)
        {
            visible++;
            if (Trees[row][i] >= Trees[row][col]) return (false, visible);
        }
        return (true,visible);
    }
    
    
    
    
}