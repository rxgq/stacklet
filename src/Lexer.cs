internal class Lexer {
    private string[] Source { get; set; }
    private List<Token> Tokens { get; set; } = new();
    private int Current { get; set; } = 0;

    private string This { 
        get => Source[Current]; 
        set { Source[Current] = value; } 
    } 

    public Lexer(string[] source) {
        Source = source;
    }

    public List<Token> Tokenize() {
        for (; Current < Source.Length; Current++)
            Tokens.Add(NextToken());

        Tokens.Add(new Token("", TokenType.EOF));
        return Tokens;
    }

    private Token NextToken() {
        This = This.TrimStart();
        string command = This.Split(' ')[0].ToLower();

        return command switch {
            "push" => new(command, TokenType.PUSH, Args()),
            "drop" => new(command, TokenType.DROP),
            "dupe" => new(command, TokenType.DUPE),
            "swap" => new(command, TokenType.SWAP),
            "free" => new(command, TokenType.FREE),
            "rev" => new(command, TokenType.REV),

            "add"  => new(command, TokenType.ADD),
            "sub"  => new(command, TokenType.SUB),
            "mul"  => new(command, TokenType.MUL),
            "div"  => new(command, TokenType.DIV),
            "mod"  => new(command, TokenType.MOD),
            "neg"  => new(command, TokenType.NEG),
            "abs"  => new(command, TokenType.ABS),

            "def"  => new(command, TokenType.DEF, Args()),
            "goto" => new(command, TokenType.GOTO, Args()),
            "out"  => new(command, TokenType.OUT),
            "nop"  => new(command, TokenType.NOP),
            "read"  => new(command, TokenType.READ),

            ""     => new("", TokenType.SPACE),
            _      => new("", TokenType.BAD),
        };
    }

    private List<string> Args() {
        var tokens = Source[Current].Split(' ');

        var args = new List<string>();
        if (tokens.Length > 1) args.Add(tokens[1]);
        if (tokens.Length > 2) args.Add(tokens[2]);

        return args;
    }

    public void Print() {
        Console.WriteLine("\n======= TOKENS =======");
        foreach (var token in Tokens) {
            Console.Write(token.ToString());
        }

        Console.Write($"\nTOKEN COUNT: {Tokens.Count}\n\n");
    }
}