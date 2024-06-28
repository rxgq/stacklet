using System.Xml.Linq;

namespace assembly.src;

enum Inst
{
    // adds reg1 with reg2 and stores the result in reg1
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

    // performs logical nand on reg1 and reg2
    NAND, // nand <reg1> <reg2>

    // performs logical nor on reg1 and reg2
    NOR,  // nor <reg1> <reg2>

    // performs logical xnor on reg1 and reg2
    XNOR, // xnor <reg1> <reg2>

    // performs negation on reg1
    NEG, // neg <reg1>

    // peforms a left shift on reg1 for amount
    SHL, // shl <reg1> <amount>

    // performs a right shift on reg2 for amount
    SHR, // shr <reg1> <amount>

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

    // jumps to a process if zero flag is not zero
    JNZ, // jnz <name>

    // jumps to a process if zero flag is zero
    JZ, // jz <name>

    // jumps to a process if sign flag is positive
    JNS, // jns <name>

    // jumps to a process if sign flag is negative
    JS, // js <name>

    // exits the process
    RET, // ret

    // "sleeps" the program for sec
    WT, // wt <sec>

    // ignores everything after the ';'
    COMMENT, // ; this is a comment

    // inserted at the end of every program
    EOF,

    // will cause the program to throw an exception
    BAD,
}

internal class Lexer
{
    public List<Instruction> Instructions { get; set; } = new();
    public string[] Source { get; set; }
    public int Current { get; set; } = 0;
    
    public string Instruction => InstructionToken().ToLower();
    public List<string> Parameters => OnParams();

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

        Instructions.Add(new Instruction(Inst.EOF, "", new List<string>()));
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
            "add"  => new(Inst.ADD, "ADD", Parameters),
            "sub"  => new(Inst.SUB, "SUB", Parameters),
            "mul"  => new(Inst.MUL, "MUL", Parameters),
            "div"  => new(Inst.DIV, "DIV", Parameters),
            "and"  => new(Inst.AND, "AND", Parameters),
            "or"   => new(Inst.OR,  "OR",  Parameters),
            "xor"  => new(Inst.XOR, "XOR", Parameters),
            "nand" => new(Inst.NAND,"NAND",Parameters),
            "xnor" => new(Inst.XNOR,"XNOR",Parameters),
            "nor"  => new(Inst.NOR, "NOR", Parameters),
            "shl"  => new(Inst.SHL, "SHL", Parameters),
            "shr"  => new(Inst.SHR, "SHR", Parameters),

            "inc"  => new(Inst.INC, "INC", Parameters),
            "dec"  => new(Inst.DEC, "DEC", Parameters),
            "not"  => new(Inst.NOT, "NOT", Parameters),
            "neg"  => new(Inst.NEG, "NEG", Parameters),

            "mov"  => new(Inst.MOV, "MOV", Parameters),
            "jmp"  => new(Inst.JMP, "JMP", Parameters),
            "jnz"  => new(Inst.JNZ, "JNZ", Parameters),
            "jz"   => new(Inst.JZ,  "JZ",  Parameters),
            "js"   => new(Inst.JS,  "JS",  Parameters),
            "jns"  => new(Inst.JNS, "JNS", Parameters),
            "ret"  => new(Inst.RET, "RET", Parameters),
            "cmp"  => new(Inst.CMP, "CMP", Parameters),
            "nop"  => new(Inst.NOP, "NOP", Parameters),

            "prt"  => new(Inst.PRT, "PRT", Parameters),
            "out"  => new(Inst.OUT, "OUT", Parameters),
            "wt"   => new(Inst.WT,  "WT",  Parameters),

            "proc" => new(Inst.PROC,"PROC",Parameters),

            _      => new(Inst.BAD, "",    Parameters),
        };
    }

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
        => Source[Current].Replace(" ", "").Split(";")[0][..^1];

    private bool IsEOF()
        => Current > Source.Length - 1;
}