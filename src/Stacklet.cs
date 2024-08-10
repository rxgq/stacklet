namespace stacklet;

internal abstract partial class Stacklet
{
    public static void Main(string[] args) {
        if (args.Length == 0) {
            Console.WriteLine("Usage: dotnet run -- <path_to_code>");
            return;
        }

        var path = args[0];
        if (path == "test") {
            StackletTests.RunTests();
            return;
        }

        if (!File.Exists(path)) {
            Console.WriteLine($"File not found: {path}");
            return;
        }

        var source = File.ReadAllLines(path);
        Execute(source, inspect: true);
    }

    public static void Execute(string[] source, bool inspect) {
        var lexer = new Lexer(source);
        var tokens = lexer.Tokenize();

        if (inspect) lexer.Print();

        var program = new Interpreter(tokens);
        program.Interpret();

        if (inspect) program.Print();
    }
}