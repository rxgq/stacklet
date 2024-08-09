internal abstract class Program
{
    public static void Main(string[] args) {
        if (args.Length == 0) {
            Console.WriteLine("Usage: dotnet run -- <path_to_code>");
            return;
        }

        var path = args[0];
        if (!File.Exists(path)) {
            Console.WriteLine($"File not found: {path}");
            return;
        }

        var source = File.ReadAllLines(path);

        var lexer = new Lexer(source);
        var tokens = lexer.Tokenize();

        lexer.Print();

        var program = new Interpreter(tokens);
        program.Interpret();

        //program.Print();
    }
}
