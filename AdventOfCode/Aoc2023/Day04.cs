namespace Aoc2023;

public static class Day04
{
    private static readonly List<string> SratchCards = Util.ReadFile("/day04/input");

    private static readonly IEnumerable<string>[] MatchesPerCard = SratchCards
        .Select(x =>
            x.Split(": ")[1].Replace("  ", " ").Trim()
                .Split(" | ")) // replaces doubles spaces to avoid counting empty strings 
        .Select(x => x[0].Split(" ").Intersect(x[1].Split(" ")))
        .ToArray(); // returns intersection between the two sides

    public static readonly double Points = MatchesPerCard.Select(x => x.Count())
        .Where(x => x > 0).Sum(x => Math.Pow(2, x - 1)); // 2 to the power to amount of winning numbers - 1 = point

    private static void AddToDict(int k, int v, Dictionary<int, int> dictionary)
    {
        if (!dictionary.ContainsKey(k))
            dictionary.Add(k, v);
        else
            dictionary[k] += v ;
    }
    public static int Copies()
    {
        var dictionary = new Dictionary<int, int>();
        for (var i = 0; i < MatchesPerCard.Length; i++)
        {
            var copies = 0;

            if (dictionary.TryGetValue(i, out var value))
                copies = value;
            AddToDict(i,1, dictionary);

            var range = MatchesPerCard[i].Count() + i <= MatchesPerCard.Length
                ? MatchesPerCard[i].Count() + i
                : MatchesPerCard.Length - i;

            for (var j = i + 1; j <= range; j++)
            {
                AddToDict(j,copies + 1, dictionary);
            }
        }

        return dictionary.Values.Sum();
    }
}