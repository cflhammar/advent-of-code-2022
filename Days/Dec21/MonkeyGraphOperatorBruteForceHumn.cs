using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec21;

public class MonkeyGraphOperatorBruteForceHuumn
{
    

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

// 100000000
// 122400000