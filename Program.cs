namespace tgm;

internal class Program
{
    static void Main()
    {
        string path = "C:\\Users\\adunderdale\\langtest.txt";
        var lines = File.ReadAllLines(path);

        foreach (var line in lines) 
        {
            Lexer.Tokens.Clear(); // remove

            Lexer.Tokenize(line);
            Console.WriteLine("\n");

            foreach (var c in Lexer.Tokens) 
            {
                Console.Write(c.Value);
            }
        }   

    }
}
