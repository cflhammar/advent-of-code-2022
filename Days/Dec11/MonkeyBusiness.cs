namespace aoc_2022.Days.Dec11;

public class MonkeyBusiness
{
    public List<Monkey> Monkeys;

    public MonkeyBusiness(List<Monkey> monkeys)
    {
        Monkeys = monkeys;
    }
    
    public int CalculateMonkeyBusiness()
    {
        long commonDiv = 1;
        foreach (var m in Monkeys)
        {
            commonDiv *= m.Divider;
        }
        
        
        for (int i = 0; i < 10000; i++)
        {
            var currentMonkey = 0;
            foreach (var monkey in Monkeys)
            {
                monkey.Inspected += monkey.Items.Count;
                foreach (var item in monkey.Items)
                {
              //      monkey.Inspected++;
                    long val = 0; 
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
                       Monkeys[monkey.TrueThrowTo].Items.Add(val);
                   }
                   else
                   {
                       Monkeys[monkey.FalseThrowTo].Items.Add(val);
                   }
                }

                monkey.Items = new List<long>();
                
                currentMonkey++;
            }
        }

        var sortedMonk = Monkeys.Select(x => x.Inspected).OrderByDescending(x => x).ToList();
        Console.WriteLine(sortedMonk[0] + "-" + sortedMonk[1] + "=" + sortedMonk[0] * sortedMonk[1]);
        return 1;
    }
}

//121103
//26910865656
