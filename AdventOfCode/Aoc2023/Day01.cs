using System.Text.RegularExpressions;

namespace Aoc2023;

public static  class Day01
{
    private static readonly List<string> Numbers = Util.ReadFile("/day01/input");
    private static int GetNumber(string code)
    {
        var digits = Regex.Replace(code,  "[^0-9]+","");
        return digits.Length == 0 ? 0 : int.Parse($"{digits[0]}{digits[^1]}");
    }

    public static int One => Numbers.Select(GetNumber).Sum();

    private static int ReplaceAndGetNumber(string s)
    {
        var dict = new Dictionary<string, string> 
            { { "one", "o1e" }, { "two", "t2" }, { "three", "t3e" }, 
                { "four", "4" }, { "five", "5e" }, { "six", "6" }, 
                { "seven", "7n" }, { "eight", "e8t" }, { "nine", "9" }};
        foreach (var (k,v) in dict)
        {
            s = s.Replace(k, v);
        }
        return GetNumber(s);
    }
    public static int Two => Numbers.Select(ReplaceAndGetNumber).Sum();
    

}
