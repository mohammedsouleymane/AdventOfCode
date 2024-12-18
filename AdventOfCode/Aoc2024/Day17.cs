namespace Aoc2024;

public static class Day17
{
    private static readonly List<string[]> Input = Util.ReadFile("/day17/input").Chunk(3).ToList();
    private static readonly Dictionary<char,int> Registers = Input.First().Select(reg => reg[9..].Split(": ")).ToDictionary(x=> x[0][0], x=> int.Parse(x[1]));
    private static readonly int[] Instructions = Input.Last().Last().Split(": ")[1].Split(",").Select(int.Parse).ToArray();
    private static readonly List<int> Output = [];

    private static int ComboOperand(int operand) => operand < 4 ? operand : Registers[(char)(65 + operand % 4)];
    private static int _pointer;
    private static void RunInstruction(int instruction, int operand )
    {
        switch (instruction)
        {
            case 0:
                Registers['A'] >>= ComboOperand(operand);
                break;
            case 1:
                Registers['B'] ^= operand;
                break;
            case 2:
                Registers['B'] = ComboOperand(operand) % 8;
                break;
            case 3:
                if (Registers['A'] != 0)
                {
                    RunInstruction(Instructions[operand], Instructions[operand+1]);
                    _pointer = operand;
                }
                break;
            case 4:
                Registers['B'] ^= Registers['C'];
                break;
            case 5:
                Output.Add(ComboOperand(operand) % 8);
                break;
            case 6:
                Registers['B'] = Registers['A'] >> ComboOperand(operand);
                break;
            case 7:
                Registers['C'] = Registers['A'] >> ComboOperand(operand);
                break;
                    
        }
    }

    public static string RunProgram()
    {
        while (_pointer < Instructions.Length)
        {
            RunInstruction(Instructions[_pointer], Instructions[_pointer + 1]);
            _pointer += 2;
        }
        return Output.ToStr(",");
    }
    
    public static int InitialA(int pointer = 0)
    {
        var i = 0;
        while (RunProgram() != Instructions.ToStr(","))
        {
            Output.Clear();
            Registers['A'] = i;
            Registers['B'] = 0;
            Registers['C'] = 0;
            i++;
        }
        return i;
    }
}

/*Program: 2,4 1,1 7,5 1,5 4,3 5,5 0,3 3,0

B = A % 8 
B = B ^ 1 
C = A >> B
B = B ^ 5
B = B ^ C
    out(B % 8)
A = A >> 3
if A != 0 jump 0*/ // reverse engineer this for part two

