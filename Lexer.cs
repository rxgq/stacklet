namespace assembly;

enum TokenType
{
    // adds the two parameters together and stores the result in reg1
    ADD, // <reg1> <reg2>

    // subtracts reg2 from reg1 and stores in reg1
    SUB, // <reg1> <reg2>

    EOF, BAD
}

internal class Token
{
    public List<string> Parameters { get; set; }

    public string Instruction { get; set; }

    public TokenType Type { get; set; }

    public Token(TokenType type, string instruction, List<string> parameters)
    {
        Type = type;
        Instruction = instruction;
        Parameters = parameters;
    }

    public override string ToString()
        => $"Type: {Type, -8} || Ins: {Instruction, -8} || Params: {string.Join(", ", Parameters) ?? ""}";
}

internal class Lexer
{
    public List<Token> Tokens { get; set; } = new();

    public string[] Source { get; set; }

    public int Current { get; set; } = 0;

    public Lexer(string[] source)
    {
        Source = source;
    }

    public List<Token> Tokenize()
    {
        while (!IsEOF())
        {
            Tokens.Add(NextToken());
            Current++;
        }

        Tokens.Add(new Token(TokenType.EOF, "NONE", new List<string>()));
        return Tokens;
    }

    private Token NextToken() 
    {
        return Source[Current][..3] switch
        {
            "ADD" => OnAdd(),
            "SUB" => OnSub(),
            _ => new Token(TokenType.BAD, "", OnParams()),
        };
    }

    private Token OnAdd()
        => new(TokenType.ADD, "ADD", OnParams());

    private Token OnSub()
        => new(TokenType.SUB, "SUB", OnParams());

    private List<string> OnParams()
        => Source[Current][3..].Split(',').ToList();

    private bool IsEOF()
        => Current > Source.Length - 1;
}