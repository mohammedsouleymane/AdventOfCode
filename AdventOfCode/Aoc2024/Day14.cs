namespace Aoc2024;

public static class Day14
{
    private static readonly List<string> Inputs = Util.ReadFile("/day14/input");
    
    private static int CountForQuadrant(int sx, int ex, int sy, int ey, List<(int,int)> robots)
    {
        var sum = 0;
        for (var i = sx; i < ex; i++)
        {
            for (var j = sy; j < ey; j++)
            {
                sum += robots.Count(x => x == (i, j));
            }
        }
        return sum;
    }

    public static int ParseRobot(bool two = false)
    {
        const int xas = 101;
        const int yas = 103;
        const int mx = xas / 2;
        const int my = yas / 2;
        
        var seconds = 0;
        while (true)
        {
            List<(int,int)> robots = [];
            foreach (var input in Inputs)
            {
                var inp = input.Split(" ").Select(p=> p.Split("=")[1].Split(",").Select(int.Parse)).Select(l=> (l.First(),l.Last())).ToArray();
                var (vx, vy) = inp.Last();
                var (x, y) = inp.First();
                var (c,r) = ((x + vx * (100+seconds)) % xas, (y + vy * (100+seconds)) % yas );
                if (c < 0)
                    c = xas + c; // makes c positive
                if (r < 0)
                    r = yas + r; // makes r positive
                robots.Add((c,r));
            }
            if(!two)
             return CountForQuadrant(0, mx, 0, my, robots) * CountForQuadrant(mx+1, xas, 0, my, robots) *
                   CountForQuadrant(0, mx, my+1, yas, robots) * CountForQuadrant(mx+1, xas, my+1, yas, robots);

            var check = 0;
            foreach (var robot in robots)
            {
                List<(int, int)> directions =
                    [(0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (-1, 1), (1, -1), (-1, -1)];
                var count = directions.Select(x => x.Add(robot)).Count(robots.Contains);
                if (count >= 4) check++; // checks which robots have atleast 4 neigbors
            }

            if (check > 200) // if 40% has atleast 4 neighbors you have an easteregg
                return seconds + 100;
            seconds++;
            
        }
    }

}