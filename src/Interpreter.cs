internal class Interpreter {
    private List<Token> Tokens { get; set; }

    private int Current { get; set; }

    private Stack<int> Stack { get; set; } = new();

    public Interpreter(List<Token> tokens) {
        Tokens = tokens;
    }

    public void Interpret() {
        while (Tokens[Current].Type != TokenType.EOF) {
            Execute();
        }
    }

    private void Execute() {
        Current++;
        
    }
}