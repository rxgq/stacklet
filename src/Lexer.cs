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
            "push"   => new(command, TokenType.PUSH, Args()),
            "drop"   => new(command, TokenType.DROP, Args()),
            "dupe"   => new(command, TokenType.DUPE, Args()),
            "swap"   => new(command, TokenType.SWAP, Args()),
            "free"   => new(command, TokenType.FREE, Args()),
            "spin"   => new(command, TokenType.SPIN, Args()),
            "size"   => new(command, TokenType.SIZE, Args()),
            "dump"   => new(command, TokenType.DUMP, Args()),

            "add"    => new(command, TokenType.ADD, Args()),
            "sub"    => new(command, TokenType.SUB, Args()),
            "mul"    => new(command, TokenType.MUL, Args()),
            "div"    => new(command, TokenType.DIV, Args()),
            "mod"    => new(command, TokenType.MOD, Args()),
            "neg"    => new(command, TokenType.NEG, Args()),
            "abs"    => new(command, TokenType.ABS, Args()),

            "def"    => new(command, TokenType.DEF, Args()),
            "goto"   => new(command, TokenType.GOTO, Args()),
            "out"    => new(command, TokenType.OUT, Args()),
            "nop"    => new(command, TokenType.NOP, Args()),
            "read"   => new(command, TokenType.READ, Args()),
            "halt"   => new(command, TokenType.HALT, Args()),

            ""       => new("", TokenType.SPACE),
            _        => new(command, TokenType.BAD),
        };
    }

    private List<string> Args() {
        var tokens = Source[Current].Split(' ');
        var args = new List<string>();

        if (tokens.Length > 1 && tokens[1][0] == '\"') {
            args.Add(ParseString());
            return args;
        }

        if (tokens.Length > 1) args.Add(tokens[1]);
        if (tokens.Length > 2) args.Add(tokens[2]);
        if (tokens.Length > 3) args.Add(tokens[3]);

        return args;
    }

    private string ParseString() {
        int curr = 5;
        while (This[curr] != '\"')
            curr++;

        return This[5..curr];
    }

    public void Print() {
        Console.WriteLine("\n======= TOKENS =======");
        foreach (var token in Tokens) {
            Console.Write(token.ToString());
        }

        Console.Write($"\nTOKEN COUNT: {Tokens.Count}\n\n");
    }
}