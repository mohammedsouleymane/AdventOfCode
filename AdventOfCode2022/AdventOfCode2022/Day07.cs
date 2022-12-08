using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace AdventOfCode2022;

public static class Day07
{
    private static readonly IEnumerable<(string s, int i)> Lines = Util.ReadFile("/day07/input").Select((s, i) => (s,i));
    private static readonly List<int> Sizes = new ();
    private static readonly int Root = DirSize();
    public static readonly int SumOfTotalSizeMin100000 = Sizes.Where(x => x <= 100000).Sum();
    public static readonly int SpaceToFreeUp = Sizes.Where(c => c + 70000000 - Root >= 30000000).Min();
    private static int _linesToSkip = 0;

    private static int DirSize(int dir = 0)
    {
        var ls = Lines.Skip( dir ).Skip(2).TakeWhile(x => !x.s.StartsWith("$")).ToList();
        var size = ls.Where(x => !x.s.Contains("dir")).Sum(x => int.Parse(x.s.Split(" ")[0]));
        size += ls.Where(x => x.s.Contains("dir")).Sum(x => DirSize(Cd(x.i)));

        _linesToSkip = dir > _linesToSkip ? dir : _linesToSkip;
        Sizes.Add(size);
        return size;
    }
    private static int Cd(int index)
    {
        var child = Lines.Take(index+1).Last().s.Split(" ")[1];
        return Lines.Skip(_linesToSkip +1).First(x => x.s.Contains($"cd {child}")).i;
    }
}