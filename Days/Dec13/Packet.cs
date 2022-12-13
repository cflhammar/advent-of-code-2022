using System.Text.Json.Nodes;

namespace aoc_2022.Days.Dec13;

public class Packet
{
    public List<Packet> SubPackets = new ();
    public int? Value;

    public Packet(JsonNode? jsn)
    {
        if (jsn != null && jsn.AsArray().Any())
        {
            
            foreach (var element in jsn.AsArray())
            {
                if (int.TryParse(element?.ToString(), out var num))
                {
                    SubPackets.Add(new Packet(num));
                }
                else if (element?.ToString() == "[]")
                {
                    SubPackets.Add(new Packet(-1));
                }
                else
                {
                    SubPackets.Add(new Packet(JsonNode.Parse(element?.ToString()!)));
                }
            }
        }
        else if (jsn?.ToString() == "[]")
        {
            SubPackets.Add(new Packet(-1));
        }
    }
    
    public Packet(int? v)
    {
        Value = v;
    }
}