using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec21;

public class MonkeyGraphOperator
{
    public long Calculate(List<List<string>> input)
    {
       var graph = CreateGraph(input);

       long sum = GetGraphValue(graph["root"]);
       return sum;
    }

    private long GetGraphValue(Node node)
    {
        if (node.Value > 0) return node.Value;

        switch (node.Operation)
        {
            case "+":
                return GetGraphValue(node.Left) + GetGraphValue(node.Right);
                break;
            case "-":
                return GetGraphValue(node.Left) - GetGraphValue(node.Right);

                break;
            case "*":
                return GetGraphValue(node.Left) * GetGraphValue(node.Right);

                break;
            case "/":
                return GetGraphValue(node.Left) / GetGraphValue(node.Right);
                break;
        }

        return 0;
    }
    
    public long SearchForHumn(List<List<string>> input)
    {
        var graph = CreateGraph(input);

        long above = 0;
        long below = 99999999999999;
        long step = 10000000000;
       
        for (long humn = above; humn < below; humn += step)
        {
            graph["humn"].Value = humn;
            long left = GetGraphValue(graph["root"].Left);
            long right = GetGraphValue(graph["root"].Right);
            
            /*
            Console.WriteLine("humn:" + humn + " ->  l: " +  left + " - r: " + right + " = " + (left - right) );
            Console.WriteLine("below: " + below + ", above: " +  above + ", step: " + step);
            Console.WriteLine((below - above) + ": " + (below - above)/10 );
            */
            
            if (left == right)
            {
                return humn;
            }
            if (left < right)
            {
                below = humn;
                humn = above;
                step = (below - above) / 10 > 10 ? (below - above) / 10 : 1;
            }
            else
            {
                above = humn;
            }
        }

        return 0;
    }
    
    private Dictionary<string, Node> CreateGraph(List<List<string>> input)
    {
        Dictionary<string, Node> graph = new();

        foreach (var line in input)
        {
            var name = line[0];
            
            if (Int32.TryParse(line[1], out var num))
            {
                if (graph.ContainsKey(name)) graph[name].Value = num;
                else graph.Add(name, new Node(){Name = name, Value = num});
            }
            else
            {
                Regex regex = new Regex("(.+) ([+-/*]) (.+)");
                var match = regex.Match(line[1]);

                var left = match.Groups[1].Value;
                var op = match.Groups[2].Value;
                var right = match.Groups[3].Value;

                if (!graph.ContainsKey(name))
                {
                    var node = new Node()
                    {
                        Name = name,
                        Operation = op
                    };
      
                    if (!graph.ContainsKey(left))
                    {
                        graph.Add(left, new Node(){Name = left});
                    }
                    node.Left = graph[left];
                    
                    if (!graph.ContainsKey(right))
                    {
                        graph.Add(right, new Node(){Name = right});
                    }
                    node.Right = graph[right];
                    
                    graph.Add(name, node);
                }
                else  // key alrady exists
                {
                    graph[name].Operation = op;
                    
                    if (!graph.ContainsKey(left))
                    {
                        graph.Add(left, new Node(){Name = name});
                    }
                    graph[name].Left = graph[left];
                    
                    if (!graph.ContainsKey(right))
                    {
                        graph.Add(right, new Node(){Name = name});
                    }
                    graph[name].Right = graph[right];
                }
            }
        }

        return graph;
    }
}


public class Node
{
    public Node Left;
    public Node Right;
    public string Name;
    public long Value;
    public string Operation;
}