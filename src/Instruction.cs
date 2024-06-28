namespace assembly.src;

enum Inst
{
    ADD,
    SUB,
    MUL,
    DIV,
    INC,
    DEC,
    AND,
    OR,
    XOR,
    NOT,
    NAND,
    NOR,
    XNOR,
    NEG,
    SHL,
    SHR,
    NOP,
    CMP,
    MOV,
    PRT,
    OUT,
    PROC,
    JMP,
    JNZ,
    JZ,
    JNS,
    JS,
    RET,
    WT,
    COMMENT,
    EOF,
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