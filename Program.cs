namespace terminal;

internal class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.White;

        while (true)
        {
            Console.Clear();
            int activeIndex = 0;

            for (int i = 1; i < 20; i++)
            {
                Console.SetCursorPosition(0, i - 1);
                Console.Write(i);

                if (activeIndex == i)
                    Console.Write(" > ");

                string? commandInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(commandInput))
                    continue;

                var commandArgs = commandInput.Split(' ');
                var command = commandArgs[0];

                Console.WriteLine(command);
            }
        }
    }
}
