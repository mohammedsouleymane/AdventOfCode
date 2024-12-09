namespace Aoc2024;

internal class FileBlock(int id)
{
    public int Id { get; set; } = id;
}

public static class Day09
{
    private static readonly string Disk = Util.ReadFile("/day09/input").First();
    public static long Calc()
    {
        var blocks = new List<FileBlock>();
        for (var j = 0; j < Disk.Length; j++)
        {
            var id = j % 2 == 0 ? j / 2 : -1;
            for (var l = 0; l < int.Parse(Disk[j].ToString()); l++)
            {
                blocks.Add(new FileBlock(id));
            }
        }
        
        var replace = blocks.ToArray();
        var i = replace.Length - 1;

        for (var j = 0; j < replace.Length; j++)
        {
            if (replace[j].Id != -1) continue;
            while (replace[i].Id == -1)
            {
                i--;
            }
            if (i < j)
                break;
            replace[j].Id =  replace[i].Id;
            replace[i].Id = -1;
            i--;
        }

        long sum = 0;
        var n = replace[..(i + 1)];
        for (var j = 0; j < n.Length; j++)
        {
            sum += j * n[j].Id;
        }
        return sum;
    }
    
    public static long Calc2()
    {
        var blocks = new List<FileBlock>();
        var blockStartAndOffset = new List<(int, int)>();
        var frees = new List<(int, int)>();
        for (var j = 0; j < Disk.Length; j++)
        {
            var id = j % 2 == 0 ? j / 2 : -1;
            if (j % 2 == 0 && int.Parse(Disk[j].ToString()) > 0)
                blockStartAndOffset.Add((blocks.Count, int.Parse(Disk[j].ToString())));
            else
                frees.Add((blocks.Count, int.Parse(Disk[j].ToString())));
            for (var l = 0; l < int.Parse(Disk[j].ToString()); l++)
            {
                blocks.Add(new FileBlock(id));
            }
        }

        var replace = blocks.ToArray();
        blockStartAndOffset.Reverse();
        foreach (var (x,y) in blockStartAndOffset)
        {
            var possibleFreeSpaces = frees.Select((zz, j) => (zz.Item1, zz.Item2, j)).Where(zz => zz.Item2 >= y).ToArray();
            if (possibleFreeSpaces.Length == 0) continue;
            
            var (s,o,idx) = possibleFreeSpaces.First(); // s: start o:offset idx:index of
            if(s+o > x) continue; // can't place after
            for (var i = 0; i < y; i++)
            {
                replace[s + i].Id = replace[x + i].Id;
                replace[x + i].Id = -1;
            }
            frees[idx] = (s + y, o - y);
        }
        
        long sum = 0;
        for (var i = 0; i < replace.Length; i++)
        {
         if(replace[i].Id == -1) continue;
         sum += i * replace[i].Id;
        }

        return sum;
    }
}