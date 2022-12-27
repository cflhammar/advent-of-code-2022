namespace aoc_2022.Days.Dec25;

public class SnafuConverter
{

    public double SumSnafuNumbers(List<string> snafuNumbers)
    {
        double sum = 0;
        foreach (var snafu in snafuNumbers)
        {
            sum += SnafuToDecimal(snafu);
        }

        return sum;
    }
    
    private double SnafuToDecimal(string snafu)
    {
        var numbers =  snafu.Select(x =>
        {
            switch (x)
            {
                case '1': return 1;
                case '2': return 2;
                case '-': return -1;
                case '=': return -2;
                default: return  0;
            }
        }).ToList();

        var power5 = 0;
        double sum = 0;
        for (int i = numbers.Count -1; i >= 0; i--)
        {
            sum += numbers[i] * Math.Pow(5, power5);
            power5++;
        }

        return sum;
    }

    public string DecimalToSnafuByIteration(double decimalNumber)
    {
        double current = 0;
        var snafu = "";
        
        for (int power5 = 20; power5 >= 0; power5--)
        {
            var options = new List<double>();
            for (int op = 2; op >= -2; op--)
            {
                options.Add(op * Math.Pow(5, power5));
            }

            double minDiff = double.MaxValue;
            double best = 5;
            for (int i = 0; i < options.Count; i++)
            {
                var diff = Math.Abs(decimalNumber - (current + options[i]));
                if (diff < minDiff)
                {
                    minDiff = diff;
                    best = options[i];
                }
            }
            
            var index = options.IndexOf(best);

            current += best;
            
            switch (index)
            {
                case 0: 
                    snafu += "2" ;
                    break;
                case 1: 
                    snafu += "1" ;
                    break;
                case 2:
                    if (snafu == "") break;
                    snafu += "0";
                    break;
                case 3: 
                    snafu += "-" ;
                    break;
                case 4:
                    snafu += "=" ;
                    break;
            }
        }

        return snafu;
    }
    
    // as expected, there was a much better way to do this :)
    public string DecimalToSnafuByNewBase(double decimalNumber)
    {
        var snafu = "";

        while (decimalNumber > 0)
        {
            var remainder = decimalNumber % 5;
            switch (remainder)
            {
                case 0: 
                    snafu = "0" + snafu;
                    break;
                case 1: 
                    snafu = "1" + snafu;
                    break;
                case 2: 
                    snafu = "2"+ snafu;
                    break;
                case 3: 
                    snafu = "=" + snafu;
                    decimalNumber += 5;
                    break;
                case 4:
                    snafu = "-" + snafu;
                    decimalNumber += 5;
                    break;
            }
            decimalNumber = Math.Floor(decimalNumber / 5);
        }
        
        return snafu;
    }
}