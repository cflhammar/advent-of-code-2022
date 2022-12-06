namespace aoc_2022.Days.Dec06;

public class SubPacket
{
    public int FindOccurence(string sequence, int length)
    {
        for (int i = 0; i < sequence.Length - length; i++)
        {
            var sub = sequence.Substring(i, length);
            if (sub == string.Join("",sub.Distinct())) return i + length;
        }

        return 0;
    }
}