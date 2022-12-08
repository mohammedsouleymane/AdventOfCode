using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace AdventOfCode2022;

public static class Day07
{
    private static IEnumerable<string> Lines = Util.ReadFile("/day07/input").Where(x=> !x.StartsWith("dir"));
    private static Stack<string> Paths = new Stack<string>();
    private static Dictionary<string, int> sizes = new Dictionary<string, int>();
    private static readonly List<int> Sizes = GetDirectorySizes();
    public static readonly int SumOfTotalSizeMin100000 = Sizes.Where(x => x <= 100000).Sum();
    public static readonly int SpaceToFreeUp = Sizes.Where(c => c + 70000000 - Sizes.Max() >= 30000000).Min();




    private static List<int> GetDirectorySizes()
    {
        if(!Lines.Any())
            return sizes.Values.ToList();
        var line = Lines.First();
        Lines = Lines.Skip(1);
        if (line.Contains(".."))
        {
            Paths.Pop();
        }
        else if (line.StartsWith("$ cd"))
        {
            Lines = Lines.Skip(1);
            Paths.Push(Paths.ToStr() + line.Split(" ")[2]);
            GetDirectorySizes();
        }
        else if (char.IsDigit(line[0]))
        {
            foreach (var dir in Paths)
            {
                sizes[dir] = sizes.GetValueOrDefault(dir) + int.Parse(line.Split(" ")[0]);;
            }
            GetDirectorySizes();
        }
        if(Lines.Any())
            GetDirectorySizes();
        return sizes.Values.ToList();
    }
}