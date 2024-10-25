namespace Aoc2015;

public class Day04
{
    private static readonly string Input = Util.ReadFile("/day04/input").First();
    private static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
    
    private static long MinValue(string extra = "")
    {
        var number = 1;
        var hash = CreateMD5(Input+number.ToString());
        while (!hash.StartsWith("00000"+extra))
        {
            number++;
            hash = CreateMD5(Input + number.ToString());
        }

        return number;
    }

    public static long FiveLeadingZeros => MinValue();
    public static long SixLeadingZeros => MinValue("0");
}