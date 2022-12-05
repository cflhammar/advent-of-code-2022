namespace aoc_2022.Days.Dec05;

public class Port
{
    private List<Stack<string>> _stacks = new();

    public Port(List<List<string>> startingStack)
    {
        startingStack.First().ForEach(x => _stacks.Add(new Stack<string>()));

        for (int i = startingStack.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < startingStack[i].Count; j++)
            {
                if (startingStack[i][j] != ".") _stacks[j].Push(startingStack[i][j]);
            }
        }
    }

    public string FollowInstructions(List<List<string>> instructions, bool part1)
        {
            foreach (var instruction in instructions)
            {
                int numberOfItemsToMove = Int32.Parse(instruction[0]);
                var stacksFromTo = instruction[1].Split(" to ").Select(Int32.Parse).ToList();
                var itemsToMove = new List<string>(); 
   
                for (int i = 0; i < numberOfItemsToMove; i++)
                {
                    var item = _stacks[stacksFromTo[0]-1].Pop();
                    itemsToMove.Add(item);
                }
                
                if (!part1) itemsToMove.Reverse();
                itemsToMove.ForEach(x => _stacks[stacksFromTo[1]-1].Push(x));
                
            }
            return string.Join("", _stacks.Select(stack => stack.Peek()));
        }
}