namespace Utilities;

public static class Util
{
	public static T Pop<T>(this List<T> lst)
	{
		var first = lst.First();
		lst.RemoveAt(0);
		return first;
	}
	public static string ReplaceAt(this string str, int index, int length, string replace)
	{
		return str.Remove(index, Math.Min(length, str.Length - index))
			.Insert(index, replace);
	}
	public static List<string> ReadFile(string path)
	{
		path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent + "/input"+path;
		return File.ReadAllLines(path).ToList();
	}

	public static string ToStr<T>(this IEnumerable<T> list, string separator = "")
	{
		return string.Join(separator, list);
	}
	public static T[] GetColumn<T>( this T[,] matrix, int columnNumber)
	{
		return Enumerable.Range(0, matrix.GetLength(0))
			.Select(x => matrix[x, columnNumber])
			.ToArray();
	}

	public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
	{
		return Enumerable.Range(0, matrix.GetLength(1))
			.Select(x => matrix[rowNumber, x]).ToArray();
	}

	public static int[,] ToMatrix(this IEnumerable<string> list)
	{
		var matrix = new int[list.First().Length, list.Count()];
		for (var i = 0; i < matrix.GetLength(0); i++)
		{
			for (var j = 0; j < matrix.GetLength(1); j++)
			{
				matrix[i, j] = int.Parse(list.ToArray()[i][j].ToString());
			}
		}

		return matrix;
	}

	public static T GetELementAt<T>(this T[,] matrix, (int x, int y) coordinate )
	{
		return matrix[coordinate.x, coordinate.y];
	}
	public static char[,] ToCharMatrix(this IEnumerable<string> list)
	{
		var matrix = new char[list.First().Length, list.Count()];
		for (var i = 0; i < matrix.GetLength(0); i++)
		{
			for (var j = 0; j < matrix.GetLength(1); j++)
			{
				matrix[i, j] = list.ToArray()[i][j];
			}
		}
		return matrix;
	}
    public static void Print<T>(this T[,] matrix )
    {
        for(var i = 0; i < matrix.GetLength(0); i++)
        {
            for(var j = 0; j < matrix.GetLength(1); j++)
            {
	            Console.Write($"{matrix[i, j]} ");
            }

            Console.WriteLine();
        }
        
    }
    public static bool Compare<T>(this T[,] firstArray, T[,] secondArray)
    {
	    return firstArray.Rank == secondArray.Rank &&
	           Enumerable.Range(0, firstArray.Rank).All(dimension => firstArray.GetLength(dimension) == secondArray.GetLength(dimension)) &&
	           firstArray.Cast<T>().SequenceEqual(secondArray.Cast<T>());
    }

	public static List<List<T>> SplitToChucks<T>(this List<T> source, T by)
	{
		var chunks = new List<List<T>>();
		var chunk = new List<T>();
		foreach (var t in source)
		{
			if (t.Equals(by))
			{
				chunks.Add(chunk);
				chunk = new List<T>();
			}
			else
			{
				chunk.Add(t);
			}
		}
		chunks.Add(chunk);
		return chunks;
	}
	
	public static bool DigitsOnly(string s)
	{
		foreach (var c in s)
		{
			if (c is < '0' or > '9')
				return false;
		}
		return true;
	}
}