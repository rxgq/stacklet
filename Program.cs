namespace assembly;

internal class Program
{
    static void Main()
    {
        var code = File.ReadAllLines("C:\\Users\\adunderdale\\Source\\Repos\\assembly\\code.txt");

        Lexer lexer = new(code);
        var instructions = lexer.Tokenize();

        foreach (var instruction in instructions)
            Console.WriteLine(instruction.ToString());

        Executer program = new(instructions);
        var result = program.Execute();

        Console.Write(result);
    }
}
