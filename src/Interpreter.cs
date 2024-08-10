namespace stacklet;

internal class Interpreter {
    private List<Token> Tokens { get; set; }
    private int Current { get; set; }
    private Stack<int> Program { get; set; } = new();
    private Dictionary<string, int> Defs { get; set; } = new();
    private bool IsHalted { get; set; } = false;

    public Interpreter(List<Token> tokens) {
        Tokens = tokens;
        MapDefs();
    }

    public void Interpret() {
        while (Tokens[Current].Type != TokenType.EOF && !IsHalted) {
            Execute();
            Current++;
        }
    }

    private void Execute() {
        if (IsCondition()) return;

        switch (Tokens[Current].Type) {
            case TokenType.PUSH: OnPush(); break;
            case TokenType.DROP: OnDrop(); break;
            case TokenType.DUPE: OnDupe(); break;
            case TokenType.SWAP: OnSwap(); break;
            case TokenType.FREE: OnFree(); break;
            case TokenType.SPIN: OnSpin(); break;
            case TokenType.SIZE: OnSize(); break;
            case TokenType.DUMP: OnDump(); break;
            case TokenType.ADD: OnOp(); break;
            case TokenType.SUB: OnOp(); break;
            case TokenType.MUL: OnOp(); break;
            case TokenType.DIV: OnOp(); break;
            case TokenType.MOD: OnOp(); break;
            case TokenType.ABS: OnAbs(); break;
            case TokenType.NEG: OnNeg(); break;
            case TokenType.MAX: OnOp(); break;
            case TokenType.MIN: OnOp(); break;
            case TokenType.OUT: OnOut(); break;
            case TokenType.READ: OnRead(); break;
            case TokenType.GOTO: OnGoto(); break;
            case TokenType.HALT: OnHalt(); break;
            case TokenType.WAIT: OnWait(); break;
            case TokenType.BAD: throw new InvalidStackOperation($"Invalid command '{Tokens[Current].Value}'");
        }
    }

    private void OnPush() {
        if (Tokens[Current].Args.Count < 1) throw new InvalidStackOperation("Push expects at least one argument");

        var isInt = int.TryParse(Tokens[Current].Args[0], out int num);
        if (!isInt) throw new InvalidStackOperation("Attempted to push a non-integer value onto the stack");

        Program.Push(num);
    }

    private void OnDrop() {
        if (Program.Count < 1) throw new StackUnderflow("Cannot drop from an empty stack");

        Program.Pop();
    }

    private void OnDupe() {
        if (Program.Count < 1) throw new InvalidStackOperation("Cannot dupe a value from an empty stack");

        var a = Program.Peek();
        Program.Push(a);
    }

    private void OnSwap() {
        if (Program.Count < 2) throw new InvalidStackOperation($"Cannot swap on a stack with less than 2 values");

        var a = Program.Pop();
        var b = Program.Pop();

        Program.Push(a);
        Program.Push(b);
    }

    private void OnFree() {
        Program.Clear();
    }

    private void OnSpin() {
        var rev = new Stack<int>();

        while (Program.Count != 0)
            rev.Push(Program.Pop());

        Program = rev;
    }

    private void OnSize() {
        Program.Push(Program.Count); 
    }

    private void OnOut() {
        if (Program.Count < 1 && Tokens[Current].Args.Count == 0) throw new InvalidStackOperation("Cannot perform out on an empty stack");

        if (Tokens[Current].Args.Count == 1) {
            Console.WriteLine(Tokens[Current].Args[0]);
            return;
        }

        Console.Write(Program.Peek() + "\n");
    }

    private void OnRead() {
        var a = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(a)) 
            return;

        var isInt = int.TryParse(a, out int val);
        if (!isInt) throw new InvalidStackOperation("Attempted to read a non-integer value into the stack");

        Program.Push(val);
    }

    private void OnDump() {
        Console.Write("STACK: ");
        var stackArray = Program.ToArray();
        for (int i = stackArray.Length - 1; i >= 0; i--)
            Console.Write($"{stackArray[i]} ");

        Console.WriteLine();
    }

    private void OnWait() {
        var isInt = int.TryParse(Tokens[Current].Args[0], out int secs);
        if (!isInt) throw new InvalidStackOperation("Attempted to call wait with a non-integer value");

        Thread.Sleep(secs * 1000);
    }

    private void OnOp() {
        if (Program.Count < 2) throw new InvalidStackOperation($"Cannot perform operation on a stack with less than 2 values");

        var b = Program.Pop();
        var a = Program.Pop();

        switch (Tokens[Current].Type) {
            case TokenType.ADD: Program.Push(a + b); break;
            case TokenType.SUB: Program.Push(a - b); break;
            case TokenType.MUL: Program.Push(a * b); break;
            case TokenType.DIV: Program.Push(a / b); break;
            case TokenType.MOD: Program.Push(a % b); break;
            case TokenType.MAX: Program.Push(Math.Max(a, b)); break;
            case TokenType.MIN: Program.Push(Math.Min(a, b)); break;
        }
    }

    private void OnAbs() {
        if (Program.Count < 1) throw new InvalidStackOperation("Cannot perform operation on a stack with less than 1 value");

        var a = Program.Pop();
        Program.Push(Math.Abs(a));
    }

    private void OnNeg() {
        if (Program.Count < 1) throw new InvalidStackOperation("Cannot perform operation on a stack with less than 1 value");

        var a = Program.Pop();
        Program.Push(-a);
    }

    private void OnGoto() {
        var def = Defs.TryGetValue(Tokens[Current].Args[0], out int idx) ? idx : -1;
        if (def == -1) throw new InvalidStackOperation($"Undefined reference to '{Tokens[Current].Args[0]}' in goto statement");

        Current = idx;
    }

    private void OnHalt() {
        IsHalted = true;
    }

    private void MapDefs() {
        for (int i = 0; i < Tokens.Count; i++) {

            if (Tokens[i].Type == TokenType.DEF)
                Defs[Tokens[i].Args[0]] = i;
        }
    }

    private bool IsCondition() {
        // the program always checks for an if statement, if args < 2 then there cannot be a conditional
        // if there are 2 or more, then it *must* be a conditional
        if (Tokens[Current].Args.Count < 2) return false;
        
        if (Program.Count == 0) throw new InvalidStackOperation("Cannot evaluate 'if' or 'ifnt' expression on empty stack");

        int ifIndex = Tokens[Current].Args
            .FindIndex(arg => arg.ToLower() == "if" || arg.ToLower() == "ifnt");
        if (ifIndex == -1) throw new InvalidStackOperation("Expected 'if' or 'ifnt'");

        if (Tokens[Current].Args.Count != ifIndex + 2) throw new InvalidStackOperation("Expected a condition after 'if' or 'ifnt'");

        var ifStmt = Tokens[Current].Args[ifIndex].ToLower();
        if (ifStmt != "if" && ifStmt != "ifnt") throw new InvalidStackOperation("Expected 'if' or 'ifnt'");

        var a = Program.Peek();

        if (ifStmt == "if") return a != int.Parse(Tokens[Current].Args[ifIndex + 1]);
        return a == int.Parse(Tokens[Current].Args[ifIndex + 1]);
    }

    public void Print() {
        Console.Write("\n======= PROGRAM =======");
        foreach (var def in Defs)
            Console.Write($"\nDEF: {def.Key, -8} => index: {def.Value}");

        Console.WriteLine("\n");
    }
}