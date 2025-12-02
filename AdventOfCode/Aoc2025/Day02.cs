using Utilities;

namespace Aoc2025;

public static class Day02
{
    private static readonly string IdRanges = Util.ReadFile("/day02/input")
        .First();


    private static bool InvalidId(string id, bool one=true)
    {
        if (one)
        { 
            if (id.Length % 2 != 0) return false;
           return id[..(id.Length / 2)] == id[(id.Length/2)..];
        }

        // checks for the sequences
        // ex "111" check with offset 1 1=1=1 so true
        // ex "1122" check with offset 1 fails at 1=2, check with offset 2 fails at 11=22
        for (var i = 1; i <= id.Length / 2  ; i++)
        {
            if(id.Length % i != 0) continue; // offset has to be divisible by ids length
            var check = true;
            for (var j = i; j < id.Length; j+=i)
            {
                if (id[..i] == id[j..(j + i)]) continue;
                check = false;
                break;
            }

            if (check)
                return check;
        }
        
        return false;
    }
    public static long InvalidRanges(bool one = true)
    {
        var ranges = IdRanges.Split(",")
            .Select(x => x.Split("-")).Select(x=> (long.Parse(x[0]) , long.Parse(x[1])));

        long sum = 0;
        foreach ( var (s, e) in ranges)
        {
            for (var i = s; i <= e; i++)
            {
                if (InvalidId(i.ToString(), one)) sum += i;
            }
        }
        return sum;
    }
    
}