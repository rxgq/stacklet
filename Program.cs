namespace terminal
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

            int largestLineIndex = 2;
            int lineIndex = 1;

            while (true)
            {
                Console.Clear();

                for (int i = 1; i < largestLineIndex; i++)
                {             
                    Console.SetCursorPosition(0, Math.Min(i - 1, 29));
                    Display.ColouredText(i.ToString(), Display.LineNumberColour);

                    if (i - 1 > 29) 
                        Console.WriteLine();

                    if (lineIndex == i) 
                    {
                        Console.SetCursorPosition(3, Math.Min(i - 1, 29));
                        Display.ColouredText(" ~ ", Display.PointerColour);
                    }                
                }
               
                Console.ForegroundColor = Display.CommandColour;
                string? commandInput = Console.ReadLine();

                lineIndex++;

                if (lineIndex >= largestLineIndex)
                    largestLineIndex++;

                if (string.IsNullOrWhiteSpace(commandInput))
                    continue;

                var commandArgs = commandInput.Split(' ');
                var command = commandArgs[0];
            }
        }
    }
}
