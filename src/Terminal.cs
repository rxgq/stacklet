namespace terminal.src;

internal class Terminal
{
    public static int MaxLineIndex = 2;
    public static int LineIndex = 1;

    public static readonly Dictionary<string, Command> Commands = new();

    public static void Run()
    {
        Command.Register();

        for (int i = 1; i < MaxLineIndex; i++)
        {
            if (i != 1)
                Console.WriteLine();

            Console.SetCursorPosition(0, i - 1);
            Display.ColouredText(i.ToString(), Display.LineNumberColour);

            if (LineIndex == i)
            {
                Console.SetCursorPosition(4, i - 1);
                Display.ColouredText("~ ", Display.PointerColour);
            }
        }

        string? commandInput = Display.ColouredInput(Display.CommandColour);

        Console.SetCursorPosition(4, LineIndex - 1);
        Display.ColouredText(string.IsNullOrWhiteSpace(commandInput) ? " " : ">", Display.PreviousPointerColour);

        LineIndex++;

        if (MaxLineIndex >= Console.WindowHeight)
            Command.ExecuteCommand("c");

        if (LineIndex >= MaxLineIndex)
            MaxLineIndex++;

        if (string.IsNullOrWhiteSpace(commandInput))
            return;

        var commandArgs = commandInput.Split(' ');
        var commandName = commandArgs[0];

        Command.ExecuteCommand(commandName);
    }
}