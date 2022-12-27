using System.Text.Json.Nodes;
namespace aoc_2022.Days.Dec13;
public class DistressSignal
{
    public int SortSignals(List<List<string>> input)
    {
        var flat = input.SelectMany(x => x).ToList();
        
        flat.AddRange(new List<string>(){"[[2]]","[[6]]"});
        flat.Sort((y,x) => 
            ComparePackets(new Packet(JsonNode.Parse(x)), new Packet(JsonNode.Parse(y))));
        
        return (flat.IndexOf("[[2]]") + 1) * (flat.IndexOf("[[6]]") + 1);
    }
    
    
    public int GetPacketsInCorrectOrder(List<List<string>> input)
    {
        var index = 1;
        var sum = 0;
        foreach (var pair in input)
        {
            var leftPacket = new Packet(JsonNode.Parse(pair[0]));
            var rightPacket = new Packet(JsonNode.Parse(pair[1]));
            
            if (ComparePackets(leftPacket, rightPacket) > 0) sum += index;
            index++;
        }
        
        return sum;
    }

    private int ComparePackets(Packet left, Packet right)
    {
        // Both packets have a int value
        if (left.Value != null && right.Value != null)
        {
            if (left.Value < right.Value) return 1;
            if (left.Value > right.Value) return -1;

            return 0;
        }
        
        // A packet is out of values to compare
        if (left.Value == -1) return 1;
        if (right.Value == -1) return -1;
        
        // Both packets have sub-packets
        if (left.Value == null && right.Value == null)
        {
            int comparison;
            for (int i = 0; i < left.SubPackets.Count && i < right.SubPackets.Count; i++)
            { 
             comparison = ComparePackets(left.SubPackets[i], right.SubPackets[i]);
             if (comparison != 0) return comparison;
            }
            
            // if sub packet values of overlapping length are equal compare by remaining values
            if (left.SubPackets.Count == right.SubPackets.Count) return 0; 
            return left.SubPackets.Count < right.SubPackets.Count ? 1 : -1;
        }

        // subpacket has value on different level -> adjust by adding value as subpacket
        if (left.Value == null && right.Value != null)
        {
            right.SubPackets.Add(new Packet(right.Value));
            right.Value = null;
            
            return ComparePackets(left, right);
        }
        if (left.Value != null && right.Value == null)
        {
            left.SubPackets.Add(new Packet(left.Value));
            left.Value = null;
            
            return ComparePackets(left, right);
        }
        
        return 0;
    }
}