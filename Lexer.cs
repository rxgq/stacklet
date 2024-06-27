namespace assembly;

enum InstructionType
{
    // adds the two parameters together and stores the result in reg1
    ADD, // <reg1> <reg2>

    // subtracts reg2 from reg1 and stores in reg1
    SUB, // <reg1> <reg2>

    //  copies content of reg1 to reg2
    MOV, // <reg1> <reg2>


    EOF, BAD
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
        => $"Type: {Type, -8} || Ins: {Syntax, -8} || Params: {string.Join(", ", Parameters) ?? ""}";
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
            Instructions.Add(NextToken());
            Current++;
        }

        Instructions.Add(new Instruction(InstructionType.EOF, "NONE", new List<string>()));
        return Instructions;
    }

    private Instruction NextToken() 
    {
        return InstructionToken() switch
        {
            "ADD" => OnAdd(),
            "SUB" => OnSub(),
            "MOV" => OnMov(),

            _ => new Instruction(InstructionType.BAD, "", OnParams()),
        };
    }

    private Instruction OnAdd()
        => new(InstructionType.ADD, "ADD", OnParams());

    private Instruction OnSub()
        => new(InstructionType.SUB, "SUB", OnParams());

    private Instruction OnMov()
        => new(InstructionType.MOV, "MOV", OnParams());

    private List<string> OnParams()
        => Source[Current][3..].Split(',').ToList();

    private string InstructionToken()
        => Source[Current][..3];

    private bool IsEOF()
        => Current > Source.Length - 1;
}