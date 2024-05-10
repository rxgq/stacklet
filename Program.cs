namespace terminal;

internal class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        int largestLineIndex = 20;

        int lineIndex = 0;

        while (true)
        {
            Console.Clear();
            lineIndex++;

            for (int i = 1; i < largestLineIndex; i++)
            {
                Console.SetCursorPosition(i.ToString().Length == 1 ? 1 : 0, i - 1);
                Display.GreyText(i.ToString());

                if (lineIndex == i)
                    Display.WhiteText(" >");
            }

            Console.SetCursorPosition(5, lineIndex - 1);
            Console.ForegroundColor = Display.CommandColour;
            string? commandInput = Console.ReadLine();

            if (lineIndex >= largestLineIndex)
                largestLineIndex++;

            if (string.IsNullOrWhiteSpace(commandInput))
                continue;

            var commandArgs = commandInput.Split(' ');
            var command = commandArgs[0];
        }
    }
}
