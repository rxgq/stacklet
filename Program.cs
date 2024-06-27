using assembly.src;

namespace assembly;

internal class Program
{
    static void Main()
    {
        var code = File.ReadAllLines("C:\\Users\\adunderdale\\source\\repos\\assembly\\src\\code.txt");

        Lexer lexer = new(code);
        var instructions = lexer.Tokenize();

        foreach (var ins in instructions)
            Console.WriteLine(ins.ToString());

        Executer program = new(instructions);
        program.Execute();
    }
}
