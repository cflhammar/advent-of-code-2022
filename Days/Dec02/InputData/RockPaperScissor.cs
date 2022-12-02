namespace aoc_2022.Days.Dec02.InputData;

public class RockPaperScissor
{
    public int CalculateScore(List<List<string>> input)
    {
        var rounds = ConvertHand(input);
        return rounds.Select(round => PlayHand(round[0],round[1])).Sum();
    }

    private int PlayHand(string opponent, string me)
    {
        if (opponent == me) return 3 + HandScore(me);
        
        if ((opponent == "rock" && me == "paper") || 
            (opponent == "paper" && me == "scissor") ||
            (opponent == "scissor" && me == "rock")) return 6 + HandScore(me);
        
        return HandScore(me);;
    }

    private int HandScore(string hand)
    {
        switch (hand)
        { 
            case "rock": return 1;
            case "paper": return 2;
            default: return 3;
        }
    }

    public int Predictor(List<List<string>> input)
    {
        var rounds = input.Select(round =>
        {
            if (round[1] == "Y") round[1] = round[0];
            else if (round[1] == "X")
            {
                if (round[0] == "A") round[1] = "Z";
                if (round[0] == "B") round[1] = "X";
                if (round[0] == "C") round[1] = "Y";
            }
            else if (round[1] == "Z")
            {
                if (round[0] == "A") round[1] = "Y";
                if (round[0] == "B") round[1] = "Z";
                if (round[0] == "C") round[1] = "X";
            }

            return round;
        }).ToList();

        return CalculateScore(rounds);
    }
    
    private List<List<string>> ConvertHand(List<List<string>> raw)
    {
        return raw.Select(round => round.Select(hand =>
        {
            if (hand is "X" or "A") return "rock";
            if (hand is "Y" or "B") return "paper";
            if (hand is "Z" or "C") return "scissor";
            return "";

        }).ToList()).ToList();
    }
}