using System.Threading;

namespace AdventOfCode2022;

public static class Day04
{
    private static readonly List<string> Pairs = FileReader.GetData("/day04/in.txt");
    public static int FullyOverlap => Pairs.Count(x => GetList(x).Item1.Intersect(GetList(x).Item2).Count() == SmallestList(x));

    public static int PartialOverlap => Pairs.Count(x => GetList(x).Item1.Intersect(GetList(x).Item2).Any());

    private static int SmallestList(string pair)
    {
        var (ls, ls2) = GetList(pair);
       return ls.Count < ls2.Count ? ls.Count : ls2.Count;
    }

    private static (List<int>, List<int>) GetList(string pair)
    {
        var sp = pair.Split(",");
        var elf = (int.Parse(sp[0].Split("-")[0]), int.Parse(sp[0].Split("-")[1]));
        var elf2 = (int.Parse(sp[1].Split("-")[0]), int.Parse(sp[1].Split("-")[1]));

        return (Fill(elf.Item1, elf.Item2), Fill(elf2.Item1,elf2.Item2));
    }

    private static List<int> Fill(int start, int end)
    {
        var list = new List<int>();
        for (var i = 0; i <= end - start; i++)
        {
            list.Add(start+i);
        }
        return list;
    }
}