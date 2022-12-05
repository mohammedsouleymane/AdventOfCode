namespace AdventOfCode2022;

public static class Day05
{
    private static readonly List<string> StacksAndMoves = FileReader.GetData("/day05/input");
    private static List<string>? _moves;

    public static string GetTopCrates(bool mover9001 = false)
    {
        var stacks = Rearrange(mover9001);
        var crates = stacks.Select(x => x.First().Replace("[", "").Replace("]", ""));
        return string.Join("", crates);
    }

    private static Stack<string>[] Rearrange(bool mover9001 = false)
    {
        var stacks = GetStacks();
        foreach (var move in _moves!.Select(x=> x.Trim().Split(" ")))
        {
            var take = int.Parse(move[1]);
            var from = int.Parse(move[3]) - 1;
            var to = int.Parse(move[5]) - 1;
            var stack = stacks![from].Take(take).ToArray();
            if (mover9001)
                stack = stack.Reverse().ToArray();
            
            for (var i = 0; i < take; i++)
            {
                stacks[to].Push(stack[i]);
                stacks[from].Pop();
            }
        }
        return stacks;
    }

    private static Stack<string>[] GetStacks()
    {
        var stacks = new List<Stack<string>>();
        var rows = StacksAndMoves.TakeWhile(x => x != "").ToList();
        var stackCount = int.Parse(rows.Last().Trim().Split(" ").Last());
        _moves = StacksAndMoves.Skip(rows.Count + 1).ToList();
        rows.RemoveAt(stackCount-1);
        for (var i = 0; i < stackCount; i++)
        {
            stacks.Add(GetStack(rows, i*4));
        }
        return  stacks.ToArray();
    }

    private static Stack<string> GetStack(IEnumerable<string> rows, int index)
    {
        var stack = new Stack<string>();
        foreach (var row in rows.Reverse())
        {
            var crate = new string(row.Skip(index).Take(3).ToArray());
            if(crate != "   " && crate != "")
                stack.Push(crate);
        }
        return stack;
    }
}