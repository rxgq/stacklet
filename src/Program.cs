internal abstract class Program
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
                {"div", "arithmetic"},
                {"mod", "arithmetic"},
                {"neg", "arithmetic"},
                {"abs", "arithmetic"},
                {"push", "stack"},
                {"drop", "stack"},
                {"dupe", "stack"},
                {"swap", "stack"},
                {"size", "stack"},
                {"spin", "stack"},
                {"free", "stack"},
            };

            foreach (var test in tests) {
                var path = $"tests/{test.Value}/{test.Key}_test.txt";

                var source = File.ReadAllLines(path);
                Console.Write($"{test.Key}: ");
    
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
