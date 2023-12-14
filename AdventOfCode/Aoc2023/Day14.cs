namespace Aoc2023;

public class Day14
{
    private static char[,] Matrix = Util.ReadFile("/day14/input").ToCharMatrix();

    private static void Up(int i, int j)
    {
        if(i == 0) return;
        var s = (i, j);
        for (var k = i ; k >= 0 ; k--)
        {
            if(Matrix[k,j] == '#')
                break;
            if (Matrix[k, j] == '.')
                s = (k, j);
        }

        Matrix[i, j] = '.';
        Matrix[s.i, s.j] = 'O';
    }
    private static void Down(int i, int j)
    {
        if(i == Matrix.GetLength(0) - 1) return;
        var s = (i, j);
        for (var k = i; k < Matrix.GetLength(0) ; k++)
        {
            if(Matrix[k,j] == '#')
                break;
            if (Matrix[k, j] == '.')
                s = (k, j);
        }

        Matrix[i, j] = '.';
        Matrix[s.i, s.j] = 'O';
    }
    private static void Right(int i, int j)
    {
        if(j == Matrix.GetLength(1) - 1) return;
        var s = (i, j);
        for (var k = j; k < Matrix.GetLength(1) ; k++)
        {
            if(Matrix[i,k] == '#')
                break;
            if (Matrix[i, k] == '.')
                s = (i, k);
        }

        Matrix[i, j] = '.';
        Matrix[s.i, s.j] = 'O';
    }
    private static void Left(int i, int j)
    {
        if(j == 0) return;
        var s = (i, j);
        for (var k = j; k >= 0 ; k--)
        {
            if(Matrix[i,k] == '#')
                break;
            if (Matrix[i, k] == '.')
                s = (i, k);
        }

        Matrix[i, j] = '.';
        Matrix[s.i, s.j] = 'O';
    }
    private static int Count(char[,] matrix)
    {
        var n = 0;
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            n += matrix.GetRow(i).Count(x => x == 'O') * (matrix.GetLength(0) - i);
        }
        return n;
    }
    public static int One()
    {
        for (var i = 0; i < Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < Matrix.GetLength(1); j++)
            {
                if(Matrix[i,j] == 'O')
                    Up(i,j);
            }
        }
        return Count(Matrix);
    }

    private static List<(int i , int j)> FindZeros()
    {
        var listZeros = new List<(int, int)>();
        for (var i = 0; i < Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < Matrix.GetLength(1); j++)
            {
                if (Matrix[i, j] == 'O') 
                    listZeros.Add((i,j));
            }
        }
        return listZeros;
    }
    public static int Two()
    {
        var matrices = new List<char[,]>();
        
        Matrix = Util.ReadFile("/day14/input").ToCharMatrix();
        const int cycles = 1000000000;
        for (var i = 0; i < cycles; i++)
        {
            for (var k = 0; k < 4; k++)
            {
                var zeros = FindZeros();
                switch (k)
                {
                    case 0:
                        zeros.ForEach(x => Up(x.i,x.j));
                        break;
                    case 1:
                        zeros.ForEach(x=> Left(x.i, x.j));
                   
                        break;
                    case 2:
                        zeros.ForEach(x=> Down(x.i, x.j));
                        break;
                    case 3:
                        zeros.ForEach(x => Right(x.i, x.j) );
                        break;
                }
            }
            
            var possibleInList = matrices.Select((m, i) => (i: i, m)).Where(x => x.m.Compare(Matrix)).ToList();
            if (possibleInList.Any())
            {
                var index = (cycles - i - 1) % (i - possibleInList.First().i);
                return Count(matrices[index + possibleInList.First().i]);
            }
                
            var copy = new char[Matrix.GetLength(0),Matrix.GetLength(1)];
            Array.Copy(Matrix, copy, copy.Length);
            matrices.Add(copy);
        }
        return Count(Matrix);
    }
}