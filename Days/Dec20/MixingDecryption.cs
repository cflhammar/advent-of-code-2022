namespace aoc_2022.Days.Dec20;

public class MixingDecryption
{
    public long Decrypt(List<long> numbers, int rounds, int multiplier = 1)
    {
        var linkedList = CreateList(numbers);

        foreach (var (_, value) in linkedList)
        {
            value.Val *= multiplier ;
        }
        
        for (int i = 0; i < rounds; i++)
        {
            linkedList = DecryptOneTime(linkedList);
        }

        long sum = 0;
        var n = linkedList.First(x => x.Value.Val == 0).Value;
        for (int i = 1; i <= 3000; i++)
        {
            n = n.Next;
            if (i % 1000 == 0) sum += n.Val;
        }
        
        return sum;
    }


    private Dictionary<int, LinkedNumber> DecryptOneTime(Dictionary<int, LinkedNumber> linkedList) 
{
    for (int index = 0; index < linkedList.Count; index++)
    {
        var number =  linkedList[index].Val % (linkedList.Count - 1);

        if (number > 0)
        {
            for (int i = 0; i < number; i++)
            {
                var node = linkedList[index];
                var prev = node.Prev;
                var next = node.Next;
                var nextNext = next.Next;

                prev.Next = next;
                next.Prev = prev;

                node.Prev = next;
                next.Next = node;

                node.Next = nextNext;
                nextNext.Prev = node;
            }
        }
        else
        {
            for (long i = number; i < 0; i++)
            {
                var node = linkedList[index];
                var prev = node.Prev;
                var next = node.Next;
                var prevPrev = prev.Prev;

                prevPrev.Next = node;
                node.Prev = prevPrev;

                node.Next = prev;
                prev.Prev = node;

                prev.Next = next;
                next.Prev = prev;
            }
        }
    }

    return linkedList;
}
    
    private Dictionary<int, LinkedNumber> CreateList(List<long> numbers)
    {
        Dictionary<int, LinkedNumber> dict = new();

        var index = 0;
        foreach (var number in numbers)
        {
            var val = number;
            var node = new LinkedNumber(val);
            dict.Add(index, node);
            index++;
        }

        dict[0].Next = dict[1];
        dict[0].Prev = dict[index-1];

        for (int i = 1; i < numbers.Count - 1; i++)
        {
            dict[i].Prev = dict[i-1];
            dict[i].Next = dict[i+1];
        }
        
        dict[index-1].Next = dict[0];
        dict[index-1].Prev = dict[index-2];
        
        return dict;
    }
    
    public class LinkedNumber
    {
        public long Val;
        public LinkedNumber Next;
        public LinkedNumber Prev;
    
        public LinkedNumber(long v)
        {
            Val = v;
        }
    }
    
    /*
    private void PrintList(Dictionary<int,LinkedNumber> d)
    {
        var node = d[0];
        var s = "";
        foreach (var unused in d)
        {
            s += " -> "  +  node.Val;
            node = node.Next;
        }

        Console.WriteLine(s);
    }
*/
}

