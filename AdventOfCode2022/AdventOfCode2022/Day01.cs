using System.Runtime.InteropServices;

namespace AdventOfCode2022;

public static class Day01
{
	private static IEnumerable<int> Calories()
	{
		var numbers = Util.ReadFile("/day01/input").Select(x => x == "" ? 0 : int.Parse(x)).ToList();
		var calories = new List<int>();


		var total = 0;
		foreach (var number in CollectionsMarshal.AsSpan(numbers))
		{
			total += number;
			if (number != 0 && number != numbers.Last()) continue;
			calories.Add(total);
			total = 0;
		}

		return calories;
	}

	public static int Max => Calories().Max();
	public static int Top3 =>  Calories().OrderByDescending(x=> x).Take(3).Sum();


}