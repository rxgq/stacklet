namespace lang;

internal class Space
{
    private readonly static int CarbonMolecules = 1;
    public readonly static List<Carbon> Carbons = new();

    private static bool isPaused = false;

    static void Main()
    {
        BuildCarbon();

        Thread keyInputThread = new(ListenForKeyPress);
        keyInputThread.Start();

        while (true)
        {
            if (!isPaused)
            {
                foreach (var carbon in Carbons)
                    carbon.Move();

                Thread.Sleep(10);
            }

            if (!isPaused)
                Console.Clear();
        }
    }

    static void BuildCarbon()
    {
        Random r = new();

        for (int i = 0; i < CarbonMolecules; i++)
        {
            var x = r.Next(0, Console.WindowWidth);
            var y = r.Next(0, Console.WindowHeight);

            int velocityX = r.Next(-2, 3);
            int velocityY = r.Next(-2, 3);

            Carbon carbon = new(x, y, velocityX, velocityY);
            Carbons.Add(carbon);
        }
    }

    static void ListenForKeyPress()
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Spacebar)
                isPaused = !isPaused;
        }
    }
}
