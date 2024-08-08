internal class Token {

    public string Value { get; set; }

    public TokenType Type { get; set; }

    public List<string> Args { get; set; }

    public Token(string value, TokenType type, List<string>? args = null) {
        Value = value;
        Type = type;
        Args = args ?? new List<string>();
    }

    public override string ToString()
    {
        string argsFormatted = Args.Count > 0 ? $"[{string.Join(", ", Args)}]" : string.Empty;
        return $"Token: {Type} {Value} {argsFormatted}\n";
    }
}

public enum TokenType {
    PUSH,
    POP,

    ADD,
    SUB,
    MUL,
    DIV,
    MOD,

    OUT,

    EOF,
    BAD,
    SPACE,
}