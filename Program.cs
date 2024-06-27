namespace assembly;

internal class Program
{
    static void Main()
    {
        var code = File.ReadAllLines("C:\\Users\\adunderdale\\Source\\Repos\\assembly\\code.txt");

        Lexer lexer = new(code);
        var instructions = lexer.Tokenize();

        Executer program = new(instructions);
        program.Execute();
    }
}
