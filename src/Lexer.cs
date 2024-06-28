namespace assembly.src;

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