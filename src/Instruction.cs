namespace assembly.src;

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