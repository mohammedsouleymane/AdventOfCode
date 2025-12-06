namespace Aoc2025;

public static class Day06
{
    private static readonly List<string> CephalopodMathWorkSheet = Util.ReadFile("/day06/input");

    public static long Solve()
    {
        var parsedWorksheet = CephalopodMathWorkSheet.Select(x => x.Split(" ",StringSplitOptions.RemoveEmptyEntries).ToArray()).ToList();
        long sum = 0;
      
        for (var i = 0; i < parsedWorksheet.First().Length; i++)
        {
            var column = parsedWorksheet.Select(x => x[i]).ToArray();
            if (column.Last() == "+")
                sum += column[..^1].Sum(long.Parse);
            else
                sum += column[..^1].Aggregate((long)1, (acc, val) => acc * long.Parse(val));
        }

        return sum;
    }

    public static long SolveRightToLeft()
    {
        var numbers = CephalopodMathWorkSheet[..^1];
        var equations = CephalopodMathWorkSheet.Last();
        long sum = 0;
        
        while (true)
        {
            var equation = equations[0] + equations[1..].TakeWhile(c => c == ' ').ToStr(); // gets sign with following spaces
            var i = equation == equations ? equation.Length + 1 : equation.Length;
            
            long solution = equation[0] == '+' ? 0 : 1;
            for (var j = 0; j < i - 1; j++)
            {
                if (equation[0] == '+')
                    solution += long.Parse(numbers.Select(x => x[j]).ToStr().Trim()); // concat all chars on that index(column) then trim it after add or multiply
                else 
                    solution *= long.Parse(numbers.Select(x => x[j]).ToStr().Trim());
            }
            sum += solution; // adds to total
            if (equation == equations) break; // reach the end
            equations = equations[i..]; // removes first i characters
            numbers = numbers.Select(x => x[i..]).ToList(); // removes first I
            
        }
        
        return sum;
    }
}