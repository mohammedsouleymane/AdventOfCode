namespace Aoc2025;

public static class Day08
{
    private static readonly string[] JunctionBoxes = Util.ReadFile("/day08/input").ToArray();

    public static long Circuits(bool two = false)
    {
        PriorityQueue<(int i, int j), double> pairs = new();
        List<HashSet<int>> circuits = [];
        // all possible pairs with they distance
        for (var i = 0; i < JunctionBoxes.Length; i++)
        {
            var box = JunctionBoxes[i].Split(",").Select(int.Parse).ToArray();
            for (var j = i+1; j < JunctionBoxes.Length; j++)
            {
                var box2 = JunctionBoxes[j].Split(",").Select(int.Parse).ToArray();
                var distance = box.Zip(box2).Sum(x => Math.Pow(x.First - x.Second, 2));
                pairs.Enqueue( (i,j), distance);
            }
        }


        var count = 0;
        while (pairs.Count > 0)
        {
            count++;
            var (i,j) = pairs.Dequeue();
            
            var possibleCircuits = circuits.Where(x => x.Intersect([i,j]).Any()).ToList();
            if (possibleCircuits.Count == 2) // when count is 2 this means that the pairs are in 2 different circuits
            {
                circuits.RemoveAll(c => possibleCircuits.Contains(c)); // we remove the 2 circuits
                circuits.Add(possibleCircuits[0].Union(possibleCircuits[1]).ToHashSet()); // merge them into one circuit
            }
            else if (possibleCircuits.Count == 1) // one of box is already in a circuit so we add to the circuit
            {
                possibleCircuits.First().Add(i);
                possibleCircuits.First().Add(j);
            }
            else // otherwise create a new one
                circuits.Add([i,j]); 
            
            if(two && circuits.First().Count == JunctionBoxes.Length)
                return long.Parse(JunctionBoxes[i].Split(",")[0]) * long.Parse(JunctionBoxes[j].Split(",")[0]);
            if (!two && count == 1000) // change to 10 for test case
                return circuits.OrderByDescending(x => x.Count).Take(3).Aggregate(1, (acc, val) => acc * val.Count);
        }

        return -1;
    }
}