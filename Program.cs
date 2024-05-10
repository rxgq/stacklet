namespace terminal;

internal class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        Console.ForegroundColor = ConsoleColor.White;

        int lineIndex = 1;

        while (true)
        {
            Console.Clear();

            for (int i = 1; i < 20; i++)
            {
                Console.SetCursorPosition(i.ToString().Length == 1 ? 1 : 0, i - 1);
                Display.GreyText(i.ToString());

                if (lineIndex == i)
                    Display.WhiteText(" >  ");
            }

            Console.SetCursorPosition(5, lineIndex - 1);
            string? commandInput = Console.ReadLine();
            lineIndex++;

            if (string.IsNullOrWhiteSpace(commandInput))
                continue;

            var commandArgs = commandInput.Split(' ');
            var command = commandArgs[0];

            Console.WriteLine(command);
        }
    }
}
