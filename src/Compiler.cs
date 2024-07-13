namespace assembly;

public class Compiler
{
    private List<Token> Tokens { get; set; }
    
    public Compiler(List<Token> tokens)
    {
        Tokens = tokens;
    }

    public void Convert()
    {
        using var writer = new StreamWriter("C:\\Users\\adunderdale\\Projects\\personal\\asm-compiler\\src\\output.asm");
        
        foreach (var token in Tokens)
        {
            
        }
    }
}