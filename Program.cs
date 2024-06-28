using assembly.src;

namespace assembly;

internal class Program
{
    static void Main()
    {
        var file = "fib";
        var code = File.ReadAllLines($"C:\\Users\\adunderdale\\source\\repos\\assembly\\src\\examples\\{file}.txt");

        Lexer lexer = new(code);
        var instructions = lexer.Tokenize();

        Executer program = new(instructions);
        program.Execute();
    }
}
