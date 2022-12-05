using System.Linq;

namespace AdventOfCode2022;

public static class Day03
{
	private static readonly List<string> Rucksacks = Util.ReadFile("/day03/input");
	public static int SumOfDuplicates => CalSum(DuplicatedItems());
	public static int SumPrioritiesOfBadgeItems => CalSum(BadgeItems());

	public static IEnumerable<char> DuplicatedItems()
	{
		return Rucksacks.Select(x => x[0..Mid(x)].Intersect(x[Mid(x)..]).First());
	}

	public static List<char> BadgeItems()
	{
		var badgeItems = new List<char>();
		for (var i = 0; i < Rucksacks.Count; i+=3)
		{
			var group = Rucksacks.Skip(i).Take(3).ToArray();
			var badgeItem = group[0].Intersect(group[1]).Intersect(group[2]).First();
			badgeItems.Add(badgeItem);
		}
		return badgeItems;
	}

	private static int Mid (string x) { return x.Length/2; }

	private static int CalSum(IEnumerable<char> ls)
	{
		return ls.Sum(x => char.IsLower(x) ? x - 96 : x - 38);
	}
}