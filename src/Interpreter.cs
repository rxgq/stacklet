internal class Interpreter {
    private List<Token> Tokens { get; set; }

    private int Current { get; set; }

    private Stack<int> Program { get; set; } = new();

    private Dictionary<string, int> Defs { get; set; } = new();

    public Interpreter(List<Token> tokens) {
        Tokens = tokens;
        BuildDefs();
    }

    public void Interpret() {
        for (; Tokens[Current].Type != TokenType.EOF; Current++) {
            Execute();
        }
    }

    private void Execute() {
        switch (Tokens[Current].Type) {
            case TokenType.PUSH: onPush(); break;
            case TokenType.POP: onPop(); break;
            case TokenType.OUT: onOut(); break;
            case TokenType.ADD: OnOp(); break;
            case TokenType.SUB: OnOp(); break;
            case TokenType.MUL: OnOp(); break;
            case TokenType.DIV: OnOp(); break;
            case TokenType.MOD: OnOp(); break;
            case TokenType.JUMP: OnJump(); break;
        }
    }

    private void onPush() {
        Program.Push(int.Parse(Tokens[Current].Args[0]));
    }

    private void onPop() {
        if (Program.Count < 1) return;
        Program.Pop();
    }

    private void onOut() {
        if (Program.Count < 1) return;
        Console.Write(Program.Peek() + "\n");
    }

    private void OnOp() {
        if (Program.Count < 2) return;

        var b = Program.Pop();
        var a = Program.Pop();

        switch (Tokens[Current].Type) {
            case TokenType.ADD: Program.Push(a + b); break;
            case TokenType.SUB: Program.Push(a - b); break;
            case TokenType.MUL: Program.Push(a * b); break;
            case TokenType.DIV: Program.Push(a / b); break;
            case TokenType.MOD: Program.Push(a % b); break;
        }
    }

    private void OnJump() {
        Thread.Sleep(500);
        var def = Defs.TryGetValue(Tokens[Current].Args[0], out int idx) ? idx : -1;
        if (def != -1) Current = idx;
    }

    private void BuildDefs() {
        for (int i = 0; i < Tokens.Count; i++) {
            if (Tokens[i].Type == TokenType.DEF)
                Defs[Tokens[i].Args[0]] = i;
        }

    }

    public void Print() {
        Console.Write("\n======= PROGRAM =======");
        foreach (var def in Defs)
            Console.Write($"\nDEF: {def.Key} => index: {def.Value}");

        Console.WriteLine("\n");
    }
}