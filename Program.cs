using System;

namespace terminal
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

            int maxLineIndex = 2;
            int lineIndex = 1;

            while (true)
            {
                for (int i = 1; i < maxLineIndex; i++)
                {
                    if (i != 1)
                        Console.WriteLine();
                    
                    Console.SetCursorPosition(0, i - 1);
                    Display.ColouredText((i).ToString(), Display.LineNumberColour);

                    if (lineIndex == i)
                    {
                        Console.SetCursorPosition(4, i - 1);
                        Display.ColouredText("~ ", Display.PointerColour);
                    }
                }

                string? commandInput = Display.ColouredInput(Display.CommandColour);

                Console.SetCursorPosition(4, lineIndex - 1);
                Display.ColouredText(string.IsNullOrWhiteSpace(commandInput) ? " " : ">", Display.PreviousPointerColour);

                lineIndex++;

                if (maxLineIndex >= Console.WindowHeight) 
                {
                    Console.Clear();
                    maxLineIndex = 2;
                    lineIndex = 1;
                }

                if (lineIndex >= maxLineIndex)
                    maxLineIndex++;

                if (string.IsNullOrWhiteSpace(commandInput))
                    continue;

                var commandArgs = commandInput.Split(' ');
                var command = commandArgs[0];
            }
        }
    }
}