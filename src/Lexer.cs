using System.Xml.Linq;

namespace assembly.src;

enum Inst
{
    // adds the two parameters together and stores the result in reg1
    ADD, // add <reg1> <reg2>

    // subtracts reg2 from reg1 and stores in reg1
    SUB, // sub <reg1> <reg2>

    // multiplies reg2 with reg1 and stores in reg1
    MUL, // mul <reg1> <reg2>

    // divides reg1 by reg2 and stores in reg1
    DIV, // div <reg1> <reg2>

    // increments the value of reg1
    INC, // inc <reg1>

    // decrements the value of reg1
    DEC, // dec <reg1>

    // performs logical and on reg1 and reg2
    AND, // and <reg1> <reg2>

    // performs logical or on reg1 and reg2
    OR, // or <reg1> <reg2>

    // performs logical xor on reg1 and reg2
    XOR, // xor <reg1> <reg2>

    // performs logical not on reg1
    NOT, // not <reg1>

    // performs negation on reg1
    NEG, // neg <reg1>

    // peforms a left shift on reg1
    SHL, // shl <reg1>

    // performs a right shift on reg2
    SHR, // shr <reg1>

    // does nothing, next instruction
    NOP, // nop

    // checks reg1 against reg2, sets zero flag to result
    CMP, // cmp <reg1> <reg2>

    // copies content of reg2 to reg1
    MOV, // mov <reg1> <reg2>

    // outputs the integer value of reg1
    PRT, // prt <reg1>

    // outputs the binary value of reg1
    OUT, // out <reg1>

    // defines a process 
    PROC, // <name>:

    // jumps to a process
    JMP, // jmp <name>

    // jumps if not zero
    JNZ, // jnz <name>

    // exits the process
    RET, // ret

    // "sleeps" the program
    WT, // wt <sec>

    // ignores everything after the ';'
    COMMENT, // ; this is a comment

    // inserted at the end of every program
    EOF,

    // will cause the program to throw an exception
    BAD,
}

internal class Instruction
{
    public List<string> Parameters { get; set; }
    public string Syntax { get; set; }
    public Inst Type { get; set; }
    public string? Identifier { get; set; }

    public Instruction(Inst type, string syntax, List<string> parameters, string? identifier = null)
    {
        Type = type;
        Syntax = syntax;
        Parameters = parameters;
        Identifier = identifier;
    }

    public override string ToString()
        => $"type: {Type,-12} || inst: {Syntax,-12} || params: {string.Join(", ", Parameters) ?? "",-12} || ident: {Identifier}";
}

internal class Lexer
{
    public List<Instruction> Instructions { get; set; } = new();

    public string[] Source { get; set; }

    public int Current { get; set; } = 0;

    
    public string Instruction => InstructionToken().ToLower();

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

        Instructions.Add(new Instruction(Inst.EOF, "NONE", new List<string>()));
        return Instructions;
    }

    private Instruction NextToken()
    {
        if (IsComment())
            return new Instruction(Inst.COMMENT, "", OnParams());

        if (IsProc())
            return new Instruction(Inst.PROC, "PROC", OnParams(), ProcIdentifier());

        return Instruction switch
        {
            "add" => OnAdd(),
            "sub" => OnSub(),
            "mul" => OnMul(),
            "div" => OnDiv(),
            "inc" => OnInc(),
            "dec" => OnDec(),

            "and" => OnAnd(),
            "or"  =>  OnOr(),
            "xor" => OnXor(),
            "cmp" => OnCmp(),
            "not" => OnNot(),
            "neg" => OnNeg(),
            "shl" => OnShl(),
            "shr" => OnShr(),
            "nop" => OnNop(),

            "mov" => OnMov(),
            "prt" => OnPrt(),
            "out" => OnOut(),
            "wt"  =>  OnWt(),

            "proc" => OnProc(),
            "jmp" => OnJmp(),
            "jnz" => OnJnz(),
            "ret" => OnRet(),

            _ => new Instruction(Inst.BAD, "", OnParams()),
        };
    }

    private Instruction OnAdd()
        => new(Inst.ADD, "ADD", OnParams());

    private Instruction OnSub()
        => new(Inst.SUB, "SUB", OnParams());

    private Instruction OnMul()
        => new(Inst.MUL, "MUL", OnParams());

    private Instruction OnDiv()
        => new(Inst.DIV, "DIV", OnParams());

    private Instruction OnInc()
        => new(Inst.INC, "INC", OnParams());

    private Instruction OnDec()
        => new(Inst.DEC, "DEC", OnParams());

    private Instruction OnAnd()
        => new(Inst.AND, "AND", OnParams());

    private Instruction OnOr()
        => new(Inst.OR,  "OR",  OnParams());

    private Instruction OnXor()
        => new(Inst.XOR, "XOR", OnParams());
    private Instruction OnCmp()
        => new(Inst.CMP, "CMP", OnParams());

    private Instruction OnNot()
        => new(Inst.NOT, "NOT", OnParams());

    private Instruction OnNeg()
        => new(Inst.NEG, "NEG", OnParams());

    private Instruction OnShl()
        => new(Inst.SHL, "SHL", OnParams());

    private Instruction OnShr()
        => new(Inst.SHR, "SHR", OnParams());

    private Instruction OnNop()
        => new(Inst.NOP, "NOP", OnParams());

    private Instruction OnMov()
        => new(Inst.MOV, "MOV", OnParams());

    private Instruction OnPrt()
        => new(Inst.PRT, "PRT", OnParams());

    private Instruction OnOut()
        => new(Inst.OUT, "OUT", OnParams());

    private Instruction OnWt()
        => new(Inst.WT,  "WT",  OnParams());

    private Instruction OnProc()
        => new(Inst.PROC,"PROC",OnParams()); 
    
    private Instruction OnJmp()
        => new(Inst.JMP, "JMP", OnParams());

    private Instruction OnJnz()
        => new(Inst.JNZ, "JNZ", OnParams());

    private Instruction OnRet()
        => new(Inst.RET, "RET", OnParams());


    private List<string> OnParams()
    {
        string line = Source[Current].Split(';', 2)[0].Trim();

        if (line.Contains(' '))
            return line[(line.IndexOf(' ') + 1)..]
                .Split(',').Select(p => p.Trim().ToLower()).ToList();
        
        return new List<string>();
    }

    private bool IsComment()
        => Source[Current].Replace(" ", "")[0] == ';';

    private string InstructionToken()
    {
        string line = Source[Current].Trim();
        int endIndex = line.IndexOfAny(new char[] { ' ', ';' });

        if (endIndex == -1)
                return line;

        return endIndex == 1 ? line : line[..endIndex];
    }

    private bool IsEmptyLine()
        => Source[Current] == "";

    private bool IsProc() 
        => Source[Current].Split(";")[0].Trim()[^1] == ':';

    private string ProcIdentifier() 
        => Source[Current].Trim().Replace(":", "");

    private bool IsEOF()
        => Current > Source.Length - 1;
}