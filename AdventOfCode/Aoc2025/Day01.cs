using Utilities;

namespace Aoc2025;

public static class Day01
{
    private static readonly List<string> Rotations = Util.ReadFile("/day01/input");
    public static int Password(bool perClick = false)
    {
        var dial = 50;
        var password = 0;
        foreach (var rotation in Rotations)
        {
            var num = int.Parse(rotation[1..]);
            for (var i = 0; i < num; i++)
            {
                if (rotation[0] == 'L')
                    dial -= 1;
                else
                    dial += 1;
                if (dial == -1) dial = 99;
                if (dial == 100) dial = 0;

                if (!perClick) continue;
                if (dial == 0) password++;

            }
            
            if(perClick) continue;
            if (dial == 0) password++;

        }

        return password;
    }
}