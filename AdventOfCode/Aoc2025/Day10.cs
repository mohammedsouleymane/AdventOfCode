using Microsoft.Z3;

namespace Aoc2025;

public static class Day10
{
    private static readonly List<string> Machines = Util.ReadFile("/day10/input");
    private static int Calculate(string diagram, string[] buttons) // part one bfs
    {
        Queue<(string, int, int)> queue = new();
        for (var i = 0; i < buttons.Length; i++)
        {
            queue.Enqueue((diagram, i, 1));// initializing all button
        }
        
        while (queue.Count != 0)
        {
            var (indicators, btn, press) = queue.Dequeue();
            var dig = indicators.ToCharArray();
            foreach (var toggle in buttons[btn][1..^1].Split(",").Select(int.Parse))
            {
                dig[toggle] = dig[toggle] == '#' ? '.' : '#';
            }
            if (dig.All(light => light == '.')) return press; // all off then done

            for (var i = 0; i < buttons.Length; i++)
            {
                if(btn == i) continue; // to stop double press
                queue.Enqueue((dig.ToStr(), i, press+1)); // current diagram, button index, press count
            }
        }
        return 0;
    }
    
    private static int CalcutateJoltage(string joltages, string[] buttons) // using Z3
    {
        var goalJoltages = joltages.Split(",").Select(int.Parse).ToArray();
        var btns = buttons.Select(x => x[1..^1].Split(",").Select(int.Parse)).ToList();
        using var ctx = new Context();
        var variables = btns
            .Select(btn => ctx.MkIntConst(btn.ToStr(",")))
            .ToList(); // creating all variables

        var opt = ctx.MkOptimize(); // creating an optimizer
        for (var i = 0; i < goalJoltages.Length; i++)
        {
            List<IntExpr> vars = []; 
            for (var j = 0; j < btns.Count; j++)
            {
                if(btns[j].Contains(i)) // check if index is in button basically all buttons that have the index
                    vars.Add(variables[j]);// of the current joltage
            }
            var sum = ctx.MkAdd(vars); // sum of all variables
            var constraint = ctx.MkEq(sum, ctx.MkInt(goalJoltages[i])); // check if they equal the goal
            opt.Add(constraint);//add as a constraint
        }
        
        var total = ctx.MkAdd(variables); 
        opt.MkMinimize(total); // what we want to minimize (sum of all variables)
        foreach (var v in variables)
            opt.Add(ctx.MkGe(v, ctx.MkInt(0))); // made sure all are >= 0
        
        var result = opt.Check();
        var presses = 0;
        if (result != Status.SATISFIABLE) return presses;
        
        var model = opt.Model;
        return variables.Sum(v => ((IntNum)model.Evaluate(v)).Int); // we sum up all the numbers
    }
    public static int MinimalPresses(bool one = true)
    {
        var sum = 0;
        foreach (var machine in Machines)
        {
            var parsed = machine.Split(" ");
            var diagram = parsed[0][1..^1];
            var buttons = parsed[1..^1];
            var joltages = parsed.Last()[1..^1];
            if (one)
                sum += Calculate(diagram, buttons);
            else
                sum += CalcutateJoltage(joltages, buttons);
        }

        return sum;
    }
}