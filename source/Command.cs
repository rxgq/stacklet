using terminal.source;

internal class Command
{
    public string? Syntax { get; set; }

    public string? Summary { get; set; }

    public Action? Execute { get; set; }

    public const string CLEAR_COMMAND = "c";

    public static void Register() 
    {
        Terminal.Commands[CLEAR_COMMAND] = new Command
        {
            Syntax = CLEAR_COMMAND,
            Summary = "Clears the terminal",
            Execute = () =>
            {
                Console.Clear();

                Terminal.MaxLineIndex = 2;
                Terminal.LineIndex = 1;
            }
        };
    }

    public static void ExecuteCommand(string commandName) 
    {
        if (Terminal.Commands.TryGetValue(commandName, out var command) && command.Execute is not null)
            command.Execute();
    }
}
