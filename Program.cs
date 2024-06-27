using assembly.src;

namespace assembly;

internal class Program
{
    static void Main()
    {
        var code = File.ReadAllLines("C:\\Users\\adunderdale\\source\\repos\\assembly\\src\\code.txt");

        Lexer lexer = new(code);
        var instructions = lexer.Tokenize();

/*        foreach (var inst in instructions)
            Console.WriteLine(inst.ToString());*/

        Executer program = new(instructions);
        program.Execute();
    }
}
