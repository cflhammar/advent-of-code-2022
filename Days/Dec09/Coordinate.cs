namespace aoc_2022.Days.Dec09;

public class Coordinate
{
 public int X;
 public int Y;

 public double DistanceToOtherPoint(Coordinate other)
 {
  return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
 }
}