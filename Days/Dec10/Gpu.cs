namespace aoc_2022.Days.Dec10;

public class Gpu
{ 
    public int FollowProgram(List<string> input)
    {
        var x = 1;
        var cycle = 0;
        var score = 0;
        var message = "";
        var interestingCycles = new List<int>() {20, 60, 100, 140, 180, 220};

        foreach (var command in input)
        {
            var cycles = 1;
            var addV = 0;
            switch (command)
            {
                case "noop": break;
                default:   
                    cycles++;
                    addV = Int32.Parse(command.Split(" ")[1]);
                    break;
            }
  
            for (int i = 0; i < cycles; i++)
            {
                cycle++;
                if (interestingCycles.Contains(cycle)) score += x * cycle;
                
                if (IsWithinSprite(cycle, x)) message += '█';
                else message += " ";
            }

            x += addV;
        }
        
        Print(message);
        return score;
    }

    private bool IsWithinSprite(int cycle, int x)
    {
        var sprite = x - 1;
        return (cycle - 1) % 40 >= sprite && (cycle - 1) % 40 < sprite + 3;
    }

    private void Print(string message)
    {
        for (int i = 1; i <= message.Length; i += 40)
        {
            var sub = message.Substring(i - 1, 40);
            Console.WriteLine(string.Join("",sub));
        }
    }
}