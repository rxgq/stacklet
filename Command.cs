using terminal;

internal class Command
{
    public string Syntax { get; set; }

    public string Summary { get; set; }

    public Action Execute { get; set; }

    public static void Register() 
    {
        Terminal.Commands["c"] = new Command
        {
            Syntax = "c",
            Summary = "Clears the terminal",
            Execute = () =>
            {
                Console.Clear();
                Terminal.MaxLineIndex = 2;
                Terminal.LineIndex = 1;
            }
        };
    }
}
