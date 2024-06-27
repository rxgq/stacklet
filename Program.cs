namespace assembly;

internal class Program
{
    static void Main()
    {
        var code = File.ReadAllLines("C:\\Users\\adunderdale\\Source\\Repos\\assembly\\code.txt");

        Lexer lexer = new(code);
        var tokens = lexer.Tokenize();

        foreach (var token in tokens)
            Console.WriteLine(token.ToString());
    }
}
