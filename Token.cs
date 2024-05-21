using static tgm.Lexer;

namespace tgm;

internal class Token
{
    public TokenType Type { get; set; }

    public dynamic Value { get; set; }

    public int StartPosition { get; set; }

    public int EndPosition { get; set; }

    public Token(TokenType type, dynamic value, int startPosition, int endPosition) 
    { 
        Type  = type;
        Value = value;
        StartPosition = startPosition;
        EndPosition = endPosition;
    }
}

