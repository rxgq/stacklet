namespace tgm;

internal class Lexer
{
    public static List<Token> Tokens { get; set; } = new();

    public static int Index { get; set; }

    public enum TokenType
    {
        BadToken,
        EndOfFile,

        Plus,
        Subtract,
        Multiply,
        Divide,
        Assignment,

        WhiteSpace,
        VariableDeclaration,
    }

    public static void Tokenize(string line)
    {
        Index = 0;
        int length = line.Length;

        while (Index < length)
        {
            char c = line[Index];

            if (c == '+')
                Tokens.Add(new Token(TokenType.Plus, c.ToString(), Index, Index));

            else if (c == '-')
                Tokens.Add(new Token(TokenType.Subtract, c.ToString(), Index, Index));

            else if (c == '*')
                Tokens.Add(new Token(TokenType.Multiply, c.ToString(), Index, Index));

            else if (c == '/')
                Tokens.Add(new Token(TokenType.Divide, c.ToString(), Index, Index));

            else if (c == ' ')
                Tokens.Add(new Token(TokenType.WhiteSpace, " ", Index, Index));

            else if (c == 'v')
            {
                if (Index + 2 < length && line.Substring(Index, 3) == "var")
                {
                    Tokens.Add(new Token(TokenType.VariableDeclaration, "var", Index, Index + 2));
                    Index += 2;
                }
            }
            else 
                Tokens.Add(new Token(TokenType.BadToken, "#", Index, Index));

            Index++;           
        }
    }

}

