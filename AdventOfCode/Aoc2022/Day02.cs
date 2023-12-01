using System.Text;

namespace Aoc2022;

public static class Day02
{
	private static readonly List<char> Opponent = new() {'A','B','C'};
	private static readonly List<char> Me = new() { 'X', 'Y', 'Z' };
	private static readonly List<string> Rounds = Util.ReadFile("/day02/input");
	private static readonly Dictionary<char, char> Wins = new()
	{
		{'X', 'C'},
		{'Y', 'A'},
		{'Z', 'B'}
	};
	private static readonly Dictionary<char, char> WinsOpp = new()
	{
		{'A', 'Z'},
		{'B', 'X'},
		{'C', 'Y'}
	};
	public static int PartOne()
	{
		var score = 0;
		foreach (var round in Rounds)
		{
			var opp = round[0];
			var me = round[^1];
			score += Me.IndexOf(me) + 1;
			if (Wins[me] == opp)
				score += 6;
			else if (Opponent.IndexOf(opp) == Me.IndexOf(me))
				score += 3;
		}
		return score;
	}

	public static int PartTwo()
	{
		var score = 0;
		foreach (var round in Rounds)
		{
			 var opp = round[0];
			 var me = round[^1];
			 score += me switch
			 {
				 'Y' => 3 + Opponent.IndexOf(opp) + 1,
				 'Z' => 6 + Me.IndexOf(Wins.FirstOrDefault(x => x.Value == opp).Key) + 1,
				 _ => Me.IndexOf(WinsOpp[opp]) + 1
			 };
		}
		return score;
	}

}