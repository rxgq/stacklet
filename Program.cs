namespace terminal
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            int largestLineIndex = 20;

            int lineIndex = 1;

            while (true)
            {
                Console.Clear();

                for (int i = 1; i < largestLineIndex; i++)
                {             
                    Console.SetCursorPosition(i.ToString().Length == 1 ? 1 : 0, i >= 30 ? 29 : i - 1);
                    Display.GreyText(i.ToString());

                    if (lineIndex == i) 
                    {
                        Console.SetCursorPosition(3, i >= 30 ? 29 : i);
                        Display.WhiteText(" >");
                    }
                        
                }
               
                Console.SetCursorPosition(5, lineIndex - 1 >= 30 ? 29 : lineIndex - 1);
                Console.ForegroundColor = Display.CommandColour;
                string? commandInput = Console.ReadLine();

                lineIndex++;

                if (lineIndex >= largestLineIndex)
                    largestLineIndex++;

                if (largestLineIndex > 30)
                    largestLineIndex++;

                if (string.IsNullOrWhiteSpace(commandInput))
                    continue;

                var commandArgs = commandInput.Split(' ');
                var command = commandArgs[0];
            }
        }
    }
}
