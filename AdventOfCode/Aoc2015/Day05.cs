namespace Aoc2015;

public class Day05
{
    private static readonly List<string> strings = Util.ReadFile("/day05/input");

    private static bool NiceStrings(string str)
    {
         const string vowels = "aeiou";
        string[] forbiddenStrings = ["ab", "cd", "pq", "xy"];
        if (forbiddenStrings.Any(str.Contains))
            return false;
        if (str.Where(vowels.Contains).Count() < 3)
            return false;

        for (var i = 0; i < str.Length - 1; i++)
        {
            if (str[i] == str[i + 1])
                return true;
        }

        return false;
    }
    private static bool NiceStringsWithoutErrors(string str)
    {
        var pairs = str[.. ^1].Zip(str[1..]).Select((x,_) => $"{x.First}{x.Second}").ToList();
        var symm = false;
        for (var i = 0; i < str.Length -2 ; i++)
        {
            if (str[i] != str[i + 2]) continue;
            symm = true;
            break;
        }

        if (!symm) return false;
        
        for (var i = 0; i < pairs.Count ; i++) 
        { 
            if (str[(i+2)..].Contains(pairs[i])) 
                return true;
        }
            
        return false;
    }

    public static int AmountOfNice => strings.Where(NiceStrings).Count();
    public static int AmountOfNiceWithoutErrors => strings.Where(NiceStringsWithoutErrors).Count();

}