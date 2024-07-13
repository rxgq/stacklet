namespace assembly;

internal abstract class Program
{
    private static void Main()
    {
        const string path = @"C:\Users\adunderdale\Projects\personal\asm-compiler\example\code.txt";
        var source = File.ReadAllLines(path);
        
        var lexer = new Lexer(source);
        var tokens = lexer.Tokenize();
        
       foreach (var t in tokens) 
           Console.WriteLine(t.ToString());

       var compiler = new Compiler(tokens);
       compiler.Convert();

    }
}
