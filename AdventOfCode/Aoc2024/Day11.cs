namespace Aoc2024;

public abstract class Day11
{
        private static readonly List<long> Stones = Util.ReadFile("/day11/input").First().Split(" ").Select(long.Parse).ToList();
        private static readonly Dictionary<(long, long), long> Dict = new(); //memoization
        private static long ApplyRules(long stone, int blinks)
        {
                long total = 0;
                if (blinks == 0) return 1;
                if (Dict.TryGetValue((stone,blinks), out var value))
                        return value;
                if (stone == 0)
                      total += ApplyRules(1,blinks-1);
                else if (stone.ToString().Length % 2 == 0)
                {
                        var str = stone.ToString();
                        var middle = str.Length / 2;
                        var left = long.Parse(str[..middle]);
                        var right = long.Parse(str[middle..]);
                        total += ApplyRules(left, blinks - 1) + ApplyRules(right, blinks - 1);
                        
                }
                else
                        total += ApplyRules(stone * 2024,  blinks- 1);
                if(!Dict.ContainsKey((stone,blinks)) && total > 1)
                        Dict.Add((stone,blinks), total);
                return total;
        }
        
        
        public static long AmountOfStonesAfter (int blinks = 25)
        {
                long sum = 0;
                sum += Stones.ToList().Sum(stone => ApplyRules(stone, blinks));
                return sum;
        }
}