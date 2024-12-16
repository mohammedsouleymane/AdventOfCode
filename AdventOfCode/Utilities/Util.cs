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
	public static (int i, int j) Find<T>(this T[,] matrix, T element)
	{
		for (var i = 0; i < matrix.GetLength(0); i++)
		{
			for (var j = 0; j < matrix.GetLength(1); j++)
			{
				if (matrix[i, j]!.Equals(element))
					return (i, j);
			}
		}
		return (-1, -1);
	}
	public static bool InBounds<T>(this T[,] matrix, (int x, int y) coordinate )
	{
		return matrix.GetLength(0) > coordinate.x && coordinate.x >= 0
		                                          && matrix.GetLength(1) > coordinate.y && coordinate.y >= 0;
	}
	
	
	public static char[,] ToCharMatrix(this IEnumerable<string> list)
	{
		var matrix = new char[ list.Count(), list.First().Length];
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
	            Console.Write($"{matrix[i, j]}");
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

    public static (int,int) Subtract(this (int, int) tuple, (int, int) toSub)
    {
	    return (tuple.Item1 - toSub.Item1, tuple.Item2 - toSub.Item2);
    }
    public static (int,int) Add(this (int, int) tuple, (int, int) toSub)
    {
	    return (tuple.Item1 + toSub.Item1, tuple.Item2 + toSub.Item2);
    }
    public static (double,double) Subtract(this (double, double) tuple, (double, double) toSub)
    {
	    return (tuple.Item1 - toSub.Item1, tuple.Item2 - toSub.Item2);
    }
    public static void Swap<T>(this T[] arr, long i, long j)
    {
	    (arr[i], arr[j]) = (arr[j], arr[i]);
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
	
	public static bool DigitsOnly(this string s) => s.All(char.IsDigit);
	
}