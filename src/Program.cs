internal abstract class Stacklet
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

        var lexer = new Lexer(source);
        var tokens = lexer.Tokenize();

        var program = new Interpreter(tokens);
        program.Interpret();
    }
    
    internal static class StackletTests {
        public static void RunTests() {
            var tests = new Dictionary<string, string>() {
                {"add", "arithmetic"},
                {"sub", "arithmetic"},
                {"mul", "arithmetic"},
                {"div", "arithmetic"}
            };

            foreach (var test in tests) {
                var source = File.ReadAllLines($"tests/{test.Value}/{test.Key}_test.txt");
                Execute(source);
            }
        }

        private static void Execute(string[] source) {
            var lexer = new Lexer(source);
            var tokens = lexer.Tokenize();

            var program = new Interpreter(tokens);
            program.Interpret();
        }
    }
}
