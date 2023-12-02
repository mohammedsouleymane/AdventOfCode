namespace Aoc2023;

public class Day02
{
    private record Cube(string s)
    {
        private string[] Arr => s.Split(" ");
        public string Color => Arr[1];
        public int Amount => int.Parse(Arr[0]);
    }
    private static readonly List<string> Games = Util.ReadFile("/day02/input");

    private static IEnumerable<Cube> GetCubes(string game) => game.Replace(";", ",").Split(",").Select(x => new Cube(x.Trim())).ToList();
    private static bool PassConfiguration(IEnumerable<Cube> gameCubes)
    {
        var configuration = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };
        return !gameCubes.Any(c => configuration[c.Color] < c.Amount);
    }
    
    public static int One => Games.Select(game => game.Split(":"))
                                  .Where(x => PassConfiguration(GetCubes(x[1])))
                                  .Sum(x=> int.Parse(x[0].Split(" ")[1]));
    public static int Two => Games.Select(game => GetCubes(game.Split(":")[1]).GroupBy(c => c.Color)
                                                                                           .Select(g => g.Max(c => c.Amount))
                                                                                           .Aggregate(1, (acc, val) => acc * val)).Sum();
}