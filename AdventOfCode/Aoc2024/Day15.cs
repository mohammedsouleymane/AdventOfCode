namespace Aoc2024;

public static class Day15
{
    private static readonly List<string> Input = Util.ReadFile("/day15/input");
    private static char[,] _grid = Input.TakeWhile(line => line.Length > 0).ToCharMatrix();
    private static readonly List<string> Instructions = Input.SkipWhile(line => line.Length > 0).Skip(1).ToList();
    
    private static (int i, int j) Move((int i, int j) current, (int i, int j) direction)
    {
        (int i , int j) next = current.Add(direction);
        if (_grid.GetELementAt(next) == '#')
            return current;
        if (_grid.GetELementAt(next) != '.')
            Move(next, direction);
        if (_grid.GetELementAt(next) != '.') return current;
        _grid[next.i, next.j] = _grid.GetELementAt(current);
        _grid[current.i, current.j] = '.';
        return next;
    }
    
    private static bool CanMove((int i, int j) left, (int i, int j) right, (int i, int j) direction)
    {
        (int i, int j) nl = left.Add(direction);
        (int i, int j) nr = right.Add(direction);
        if (_grid.GetELementAt(nl) == '#' || _grid.GetELementAt(nr) == '#') return false;
        if (_grid.GetELementAt(nl) == '[' && _grid.GetELementAt(nr) == ']')
            return CanMove(nl, nr, direction);
        if (_grid.GetELementAt(nl) == ']' && _grid.GetELementAt(nr) == '.')
            return CanMove(nl.Subtract((0,1)), nl,direction ) ;
        if (_grid.GetELementAt(nl) == '.' && _grid.GetELementAt(nr) == '[')
            return CanMove(nr,nr.Add((0,1)),direction ) ;
        if (_grid.GetELementAt(nl) == ']' && _grid.GetELementAt(nr) == '[')
            return  CanMove(nl.Subtract((0, 1)), nl, direction) && 
                    CanMove(nr, nr.Add((0, 1)),direction ) ;
        return _grid.GetELementAt(nl) == '.' &&
               _grid.GetELementAt(nr) == '.';
    }

    private static void MoveBox((int i, int j) left, (int i, int j) right, (int i, int j) direction)
    {
        (int i, int j) nl = left.Add(direction);
        (int i, int j) nr = right.Add(direction);
        if (_grid.GetELementAt(nl) == '#' || _grid.GetELementAt(nr) == '#') return;
        if (_grid.GetELementAt(nl) == '[' && _grid.GetELementAt(nr) == ']')
            MoveBox(nl, nr,direction);
        if (_grid.GetELementAt(nl) == ']' && _grid.GetELementAt(nr) == '.')
            MoveBox(nl.Subtract((0,1)), nl,direction ) ;
        if (_grid.GetELementAt(nl) == '.' && _grid.GetELementAt(nr) == '[')
            MoveBox(nr,nr.Add((0,1)),direction ) ;
        if (_grid.GetELementAt(nl) == ']' && _grid.GetELementAt(nr) == '[')
        {
            MoveBox(nl.Subtract((0, 1)), nl, direction);
            MoveBox(nr, nr.Add((0,1)), direction);
        }
        if (_grid.GetELementAt(nl) != '.') return;
        _grid[nl.i, nl.j] = '[';
        _grid[nr.i, nr.j] = ']';
        _grid[left.i, left.j] = '.';
        _grid[right.i, right.j] = '.';
    }
    private static (int i, int j) MoveVertical((int i, int j) current, (int i, int j) direction)
    {
        (int i , int j) next = current.Add(direction);
        if (_grid.GetELementAt(next) == '#')
            return current;
        if (_grid.GetELementAt(next) == '[' && CanMove(next,next.Add((0,1)), direction))
            MoveBox(next, next.Add((0,1)), direction );
        else if(_grid.GetELementAt(next) == ']' &&  CanMove(next.Subtract((0,1)),next, direction))
            MoveBox(next.Subtract((0,1)),next, direction);
        if (_grid.GetELementAt(next) != '.') return current;
        _grid[next.i, next.j] = _grid.GetELementAt(current);
        _grid[current.i, current.j] = '.';
        return next;
    }


    private static char[,] Expand()
    {
        _grid = Input.TakeWhile(line => line.Length > 0).ToCharMatrix();
         List<string> n = [];
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            var str = "";
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                switch (_grid[i, j])
                {
                    case 'O':
                        str += "[]";
                        break;
                    case '@':
                        str += "@.";
                        break;
                    case '#':
                        str += "##";
                        break;
                    case '.':
                        str += "..";
                        break;
                }
            }
            n.Add(str);
        }
        return n.ToCharMatrix();
    }
    public static int SumOfBoxes(bool two= false)
    {
        if(two) _grid  = Expand();
        var start = _grid.Find('@');
        foreach (var instruction in Instructions.ToStr())
        {
            start = instruction switch
            {
                '^' => two ? MoveVertical(start, (-1,0)) : Move(start, (-1, 0)),
                'v' => two ? MoveVertical(start,(1, 0)) : Move(start, (1, 0)) ,
                '<' => Move(start, (0, -1)),
                '>' => Move(start, (0, 1)),
                _ => start
            };
        }
     

        var sum = 0;
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j] is '[' or 'O')
                    sum += 100 * Math.Abs(i) + j;
            }
        }
        return sum;
    }
}