namespace aoc_2022.Days.Dec03;

public class RucksackCalculator
{
    public int ScoreOfOverlappingRucksackCompartments(List<string> bags)
    {
        var score = 0;
        foreach (var bag in bags)
        {
            var firstCompartment = bag.Substring(0, bag.Length / 2);
            var secondCompartment = bag.Substring(bag.Length / 2);

            score += GetCharScore(firstCompartment.Intersect(secondCompartment).First());
        }

        return score;
    }

    public int ScoreOfBagGroup(List<string> bags)
    {
        var score = 0;
        for (int i = 0; i < bags.Count; i += 3)
        {
            var overlap = bags[i].Intersect(bags[i + 1]).Intersect(bags[i + 2]).First();
            score += GetCharScore(overlap);
        }

        return score;

    }

    private int GetCharScore(char overlap)
    {
        if (Char.IsUpper(overlap)) return (int) overlap - (65 - 27);
        else return (int) overlap - (97 - 1);

    }
}