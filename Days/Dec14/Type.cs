namespace aoc_2022.Days.Dec14;

public class Type
{
    public bool IsRock = false;
    public bool IsSand = false;
    public bool IsAir = true;
    
    public void ToSand()
    {
        IsSand = true;
        IsAir = false;
    }

    public void ToRock()
    {
        IsRock = true;
        IsAir = false;
    }

    public void ToAir()
    {
        IsSand = false;
        IsAir = true;
    }

    public override string ToString()
    {
        if (IsAir) return ".";
        if (IsRock) return "#";
        else return "o";
    }
    
}