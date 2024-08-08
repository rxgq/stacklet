internal abstract class Program {
    public static void Main() {
        const string path = @"example/code.txt";

        var source = File.ReadAllLines(path);
        
        var lexer = new Lexer(source);
        var tokens = lexer.Tokenize();

        lexer.Print();

        var program = new Interpreter(tokens);
        program.Interpret();
    }
}