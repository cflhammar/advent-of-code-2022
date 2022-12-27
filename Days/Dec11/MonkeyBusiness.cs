namespace aoc_2022.Days.Dec11;

public class MonkeyBusiness
{
    private List<Monkey> _monkeys;

    public MonkeyBusiness(List<Monkey> monkeys)
    {
        _monkeys = monkeys;
    }
    
    public int CalculateMonkeyBusiness()
    {
        long commonDiv = 1;
        foreach (var m in _monkeys)
        {
            commonDiv *= m.Divider;
        }
        
        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in _monkeys)
            {
                monkey.Inspected += monkey.Items.Count;
                foreach (var item in monkey.Items)
                {

                    long val; 
                    switch (monkey.OperationVal)
                   {
                        case "old": 
                            val = item;
                            break;
                        default:
                            val =  long.Parse(monkey.OperationVal);
                            break;
                   }

                   switch (monkey.OperationSign)
                   {
                       case "+":
                           val = item + val;
                           break;
                       default: 
                           val = item * val;
                           break;
                   }

                  val = val % commonDiv;

                   if (val % monkey.Divider == 0)
                   {
                       _monkeys[monkey.TrueThrowTo].Items.Add(val);
                   }
                   else
                   {
                       _monkeys[monkey.FalseThrowTo].Items.Add(val);
                   }
                }

                monkey.Items = new List<long>();
            }
        }

        var sortedMonk = _monkeys.Select(x => x.Inspected).OrderByDescending(x => x).ToList();
        Console.WriteLine(sortedMonk[0] + "-" + sortedMonk[1] + "=" + sortedMonk[0] * sortedMonk[1]);
        return 1;
    }
}

//121103
//26910865656
