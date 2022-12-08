﻿namespace AdventOfCode2022;

public static class Util
{
	public static List<string> ReadFile(string path)
	{
		path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent + "/input"+path;
		return File.ReadAllLines(path).ToList();
	}

	public static string ToStr<T>(this IEnumerable<T> list)
	{
		return string.Join("", list);
	}
}