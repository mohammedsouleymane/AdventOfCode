namespace Aoc2024;

public static class Day05
{
    private static readonly List<string> Lines = Util.ReadFile("/day05/input");

    private static List<string> Get(Dictionary<string, List<string>> dic, string key) => dic.TryGetValue(key, out var value) ? value : [];

    private static bool Valid(string[] update, bool two)
    {
        var wrong = 0;
        var ordering = 
            Lines
                .TakeWhile(x => x.Length != 0)
                .GroupBy(x=> x.Split("|") [0])
                .ToDictionary(group => group.Key, group => group.Select(x=> x.Split("|")[1]).ToList());
        for (var i = 0; i < update.Length; i++)
        {
            var current = update[i];
            var after = update.Skip(i+1).FirstOrDefault(x=>Get(ordering,x).Contains(current)); // checks that number that have to come after any of the number after it in the ordering
            if (after == null) continue; 
            if (two)
            {
                var j = Array.FindIndex(update, x => x == after);
                update.Swap(i,j);
                i--;
                wrong = 1;
            }
            else
                return false;
        }
        return two switch
        {
            true when wrong == 1 => true,
            true  => false,
            _ => true
        };
    }
    public static int CheckForValidUpdates(bool two = false)
    {
        return Lines
            .SkipWhile(x => x.Length != 0)
            .Skip(1)
            .Select(x=> x.Split(","))
            .Where(update => Valid(update, two))
            .Sum(update => int.Parse(update[update.Length / 2]));   
    }
}