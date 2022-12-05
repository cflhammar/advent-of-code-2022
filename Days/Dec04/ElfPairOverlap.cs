namespace aoc_2022.Days.Dec04;

public class ElfPairOverlap
{
    public (int,int) CalculateElfOverlap(List<List<string>> input)
    {
        int fullOverlap = 0, partialOverlap = 0;

        foreach (var elfPair in input)
        {
            var elf1 = elfPair[0].Split("-").Select(Int32.Parse).ToList();
            var elf2 = elfPair[1].Split("-").Select(Int32.Parse).ToList();;

            if (elf1[0] <= elf2[0] && elf1[1] >= elf2[1] || 
                elf1[0] >= elf2[0] && elf1[1] <= elf2[1])
            {
                fullOverlap++;
            }

            if (elf1[0] >= elf2[0] && elf1[0] <= elf2[1] || 
                elf1[1] >= elf2[0] && elf1[1] <= elf2[1] || 
                elf2[0] >= elf1[0] && elf2[0] <= elf1[1] || 
                elf2[1] >= elf1[0] && elf2[1] <= elf1[1]
    )
            {
                partialOverlap++;
            }
        }

        return (fullOverlap, partialOverlap);
    }
}