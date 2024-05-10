using System;

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
                    if (i != 1)
                        Console.WriteLine();
                    
                    Console.SetCursorPosition(0, i >= Console.WindowHeight ? Console.WindowHeight - 1 : i - 1);
                    Console.Write((i).ToString());

                    if (lineIndex == i)
                    {
                        Console.SetCursorPosition(4, i >= Console.WindowHeight ? Console.WindowHeight - 1 : i - 1);
                        Console.Write("~ ");
                    }
                }

                string? commandInput = Console.ReadLine();

                lineIndex++;

                if (lineIndex >= largestLineIndex) 
                {
                    largestLineIndex++;
                }

                if (string.IsNullOrWhiteSpace(commandInput))
                    continue;

                var commandArgs = commandInput.Split(' ');
                var command = commandArgs[0];
            }
        }
    }
}