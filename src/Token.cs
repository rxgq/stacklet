namespace assembly;

public enum TokenType
{
    ADD,
    SUB,
    DIV,
    MUL,
    
    MOVE,
    
    PRINT,
    
    PROC,
    GOTO,
    
    BAD
}

public class Token
{
    private TokenType Type { get; set; }
    private List<string> Parameters { get; set; }

    public Token(TokenType type, List<string> parameters)
    {
        Type = type;
        Parameters = parameters;
    }

    public override string ToString()
        => $"Type: {Type.ToString(), -8} || {string.Join(" | ", Parameters)}";
}