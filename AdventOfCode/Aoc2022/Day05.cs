namespace Aoc2022;

public static class Day05
{
    private static readonly List<string> StacksAndMoves = Util.ReadFile("/day05/input");
    private static List<string>? _moves;
    public static string TopCratesWithMover9000 => Rearrange().Select(x => x.First()).ToStr();
    public static string TopCratesWithMover9001 => Rearrange(true).Select(x => x.First()).ToStr();

    private static IEnumerable<Stack<char>> Rearrange(bool mover9001 = false)
    {
        var stacks = GetStacks();
        foreach (var move in _moves!.Select(x=> x.Trim().Split(" ")))
        {
            var take = int.Parse(move[1]);
            var from = int.Parse(move[3]) - 1;
            var to = int.Parse(move[5]) - 1;
            var stack = stacks[from].Take(take).ToArray();
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

    private static Stack<char>[] GetStacks()
    {
        var rows = StacksAndMoves.TakeWhile(x => x != "").ToList();
        _moves = StacksAndMoves.Skip(rows.Count + 1).ToList();
        return  rows.Select((_, i) => GetStack(rows, i * 4)).ToArray();
    }

    private static Stack<char> GetStack(IEnumerable<string> rows, int index)
    {
        var stack = new Stack<char>();
        foreach (var row in rows.SkipLast(1).Reverse())
        {
            var crate = row[index+1];
            if(crate != ' ')
                stack.Push(crate);
        }
        return stack;
    }
}