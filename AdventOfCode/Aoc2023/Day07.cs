namespace Aoc2023;


class CamelCardsSort : IComparer<string>
{
    public CamelCardsSort(int jokerValue = 11)
    {
       _dictionary = new()
        {
            {'A',14 },
            {'K', 13},
            {'Q',12},
            {'J',jokerValue },
            {'T',10},
        };
    }

    private readonly Dictionary<char, int> _dictionary;

    private  int Compare(char one, char two)
    {
        var a = _dictionary.TryGetValue(one, out var val) ? val : int.Parse(one.ToString());
        var b = _dictionary.TryGetValue(two, out var va) ? va : int.Parse(two.ToString());
        return a.CompareTo(b);
    }

     public int Compare(string? one, string? two)
    {
        var i = 0;
        var compared = Compare(one[i], two[i]);
        while (compared == 0 && i < one.Length - 1)
        {
            compared = Compare(one[++i], two[i]);
        }
        return compared;
    }
}

public static class Day07
{

    private static int GetPairType(string s)
    {
        var by = s.GroupBy(x => x).ToArray();
        return (by.Count(), by.Max(x => x.Count())) switch
        {
            (5, 1) => 0, // high card
            (4, 2) => 1, // one pair
            (3, 2) => 2, // two pair
            (3, 3) => 3, // three of kind
            (2, 3) => 4, // full house
            (2, 4) => 5, // four of a kind
            (1, 5) => 6, // five of a kind
            _ => -1
        };
    }
    
    private static int GetPairJoker(string s) => !s.Contains('J') ? GetPairType(s) : s.Replace("J", "").Distinct().Select(c => GetPairType(s.Replace('J', c))).Prepend(GetPairType(s)).Max();
    
    private static readonly List<string> Hands = Util.ReadFile("/day07/input");
    
    public static int One => Hands.Select(x => x.Split(" "))
        .Select(x =>  (GetPairType(x[0]), int.Parse(x[1]), x[0]))
        .OrderBy(x => x.Item1).ThenBy(x=> x.Item3, new CamelCardsSort())
        .Select((x,i) => (i+1)*x.Item2).Sum();
    
    public static int Two => Hands.Select(x => x.Split(" "))
        .Select(x =>  (GetPairJoker(x[0]), int.Parse(x[1]), x[0]))
        .OrderBy(x => x.Item1).ThenBy(x=> x.Item3, new CamelCardsSort(1))
        .Select((x,i) => (i+1)*x.Item2).Sum();

}