namespace assembly.src;

enum InstructionType
{
    // adds the two parameters together and stores the result in reg1
    ADD, // add <reg1> <reg2>

    // subtracts reg2 from reg1 and stores in reg1
    SUB, // sub <reg1> <reg2>

    // multiplies reg2 with reg1 and stores in reg1
    MUL, // mul <reg1> <reg2>

    // divides reg1 by reg2 and stores in reg1
    DIV, // div <reg1> <reg2>

    // copies content of reg2 to reg1
    MOV, // mov <reg1> <reg2>

    // outputs the value of reg1
    PRT, // prt <reg1>

    // increments the value of reg1
    INC, // inc <reg1>

    // decrements the value of reg1
    DEC, // dec <reg1>

    AND,
    OR,
    XOR,

    // inserted at the end of every program
    EOF,

    // ignores everything after the ';'
    COMMENT, // ; this is a comment

    // will cause the program to throw an exception
    BAD,
}

internal class Instruction
{
    public List<string> Parameters { get; set; }

    public string Syntax { get; set; }

    public InstructionType Type { get; set; }

    public Instruction(InstructionType type, string instruction, List<string> parameters)
    {
        Type = type;
        Syntax = instruction;
        Parameters = parameters;
    }

    public override string ToString()
        => $"Type: {Type,-8} || Ins: {Syntax,-8} || Params: {string.Join(", ", Parameters) ?? ""}";
}

internal class Lexer
{
    public List<Instruction> Instructions { get; set; } = new();

    public string[] Source { get; set; }

    public int Current { get; set; } = 0;

    public Lexer(string[] source)
    {
        Source = source;
    }

    public List<Instruction> Tokenize()
    {
        while (!IsEOF())
        {
            if (!IsEmptyLine())
                Instructions.Add(NextToken());

            Current++;
        }

        Instructions.Add(new Instruction(InstructionType.EOF, "NONE", new List<string>()));
        return Instructions;
    }

    private Instruction NextToken()
    {
        if (IsComment())
            return new Instruction(InstructionType.COMMENT, "", OnParams());
        
        var x = InstructionToken().ToLower() switch
        {
            "add" => OnAdd(),
            "sub" => OnSub(),
            "mul" => OnMul(),
            "div" => OnDiv(),
            "mov" => OnMov(),
            "prt" => OnPrt(),
            "inc" => OnInc(),
            "dec" => OnDec(),
            "and" => OnAnd(),
            "or"  =>  OnOr(),
            "xor" => OnXor(),

            _ => new Instruction(InstructionType.BAD, "", OnParams()),
        };

        return x;
    }

    private Instruction OnAdd()
        => new(InstructionType.ADD, "ADD", OnParams());

    private Instruction OnSub()
        => new(InstructionType.SUB, "SUB", OnParams());

    private Instruction OnMul()
        => new(InstructionType.MUL, "MUL", OnParams());

    private Instruction OnDiv()
        => new(InstructionType.DIV, "DIV", OnParams());

    private Instruction OnMov()
        => new(InstructionType.MOV, "MOV", OnParams());

    private Instruction OnPrt()
        => new(InstructionType.PRT, "PRT", OnParams());

    private Instruction OnInc()
        => new(InstructionType.INC, "INC", OnParams());

    private Instruction OnDec()
        => new(InstructionType.DEC, "DEC", OnParams());

    private Instruction OnAnd()
        => new(InstructionType.AND, "AND", OnParams());

    private Instruction OnOr()
        => new(InstructionType.OR, "OR", OnParams());

    private Instruction OnXor()
        => new(InstructionType.XOR, "XOR", OnParams());

    private List<string> OnParams()
    {
        string line = Source[Current].Split(';', 2)[0].Trim();

        if (line.Contains(' '))
            return line[(line.IndexOf(' ') + 1)..].Split(',').Select(p => p.Trim().ToLower()).ToList();
        
        return new List<string>();
    }

    private bool IsComment()
        => Source[Current].Replace(" ", "")[0] == ';';

    private string InstructionToken()
    {
        string line = Source[Current].Trim();
        int endIndex = line.IndexOfAny(new char[] { ' ', ';' });

        return endIndex == 1 ? line : line[..endIndex];
    }

    private bool IsEmptyLine()
        => Source[Current] == "";

    private bool IsEOF()
        => Current > Source.Length - 1;
}