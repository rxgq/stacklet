namespace assembly;

public class Lexer
{
    private List<Token> Tokens { get; set; } = new();
    private string[] Source { get; set; }
    private int Position { get; set; }
    private string Current => Source[Position];

    public Lexer(string[] source)
    {
        Source = source;
    }

    public List<Token> Tokenize()
    {
        while (!IsEof())
        {
            var line = RemoveComments(Current.Trim());

            if (!string.IsNullOrEmpty(line))
                Tokens.Add(NextToken(line));

            Position++;
        }

        return Tokens;
    }

    private static Token NextToken(string line)
    {
        if (line.EndsWith(":"))
        {
            var labelName = line[..^1].Trim();
            return new Token(TokenType.PROC, new List<string> { labelName });
        }
        
        var command = line.Split(" ")[0].ToLower();

        return command switch
        {
            "add"   => new Token(TokenType.ADD,   Parameters(line)),
            "sub"   => new Token(TokenType.SUB,   Parameters(line)),
            "mul"   => new Token(TokenType.MUL,   Parameters(line)),
            "div"   => new Token(TokenType.DIV,   Parameters(line)),
            "move"  => new Token(TokenType.MOVE,  Parameters(line)),
            "print" => new Token(TokenType.PRINT, Parameters(line)),
            "goto"  => new Token(TokenType.GOTO,  Parameters(line)),
            _       => new Token(TokenType.BAD,   Parameters(line))
        };
    }

    private static string RemoveComments(string line)
    {
        var commentIndex = line.IndexOf("//", StringComparison.Ordinal);
        if (commentIndex != -1)
            line = line[..commentIndex].Trim();
        
        return line;
    }
    
    private static List<string> Parameters(string line)
    {
        var spaceIndex = line.IndexOf(' ');

        if (spaceIndex == -1)
            return new List<string>();

        var paramString = line[(spaceIndex + 1)..].Trim();
        var parameters = new List<string>();

        while (!string.IsNullOrWhiteSpace(paramString))
        {
            if (paramString[0] == '\"')
            {
                var endIndex = paramString.IndexOf('\"', 1);
                if (endIndex == -1)
                {
                    parameters.Add(paramString[1..]);
                    break;
                }
                parameters.Add(paramString.Substring(1, endIndex - 1));
                paramString = endIndex + 1 < paramString.Length ? paramString[(endIndex + 1)..].Trim() : string.Empty;
            }
            else
            {
                var endIndex = paramString.IndexOf(' ');
                if (endIndex == -1)
                {
                    parameters.Add(paramString);
                    break;
                }
                parameters.Add(paramString[..endIndex]);
                paramString = endIndex + 1 < paramString.Length ? paramString[(endIndex + 1)..].Trim() : string.Empty;
            }
        }

        return parameters;
    }

    private bool IsEof()
        => Position >= Source.Length;
}