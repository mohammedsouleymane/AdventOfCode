using System.Text.RegularExpressions;

namespace Aoc2015;

public class Day08
{
    private static readonly List<string> Literals = Util.ReadFile("/day08/input");
    public static int NumberOfChars => Literals.Sum(x => x.Length -Regex.Unescape(x[1..^1]).Length);
    public static int EncodedChars => Literals.Sum(x => 2+ x.Count(c => c is '\\' or '\"'));
}