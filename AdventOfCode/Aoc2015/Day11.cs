namespace Aoc2015;

public class Day11
{
    private static readonly char[] Password = "xx".ToCharArray();

    public static string NewPassword()
    {
        for (var i = Password.Length - 1; i > -1; i--)
        {
            var z = Password.ToStr();
            var curr =(char) (Password[i]+1);
            Password[i] = curr  == '{' ? 'a' : curr;
            if (Password[i] == 'a')
            {
                Password[i-1] = (char)(Password[i - 1]+1);
                i = Password.Length ;
            }
            else
                i++;
            if (Check(Password.ToStr())) Password.ToStr();

        }
        
        return Password.ToStr();
    }
    
    
    private static bool Check(string password)
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";
        if (password.Contains('i') || password.Contains('o') || password.Contains('l')) return false;
        for (var i = 0; i < password.Length - 2; i++)
        {
            if (alphabet.Contains(password[i .. (i + 2)]))
                return true;
        }

        var found = "";
        for (var i = 0; i < password.Length -1; i++)
        {
            if (password[i] != password[i + 1] || found.Contains(password[i])) continue;
            found += password[i];
            i++;
        }
        return found.Length > 1;
    }
}