namespace lang;

internal class Space
{
    private readonly static int CARBON_COUNT = 100;
    public readonly static List<Carbon> Carbons = new();

    private static bool IsPaused = false;

    static void Main()
    {
        BuildCarbon();

        new Thread(() => PauseSpace()).Start();

        while (true)
        {
            if (!IsPaused)
            {
                foreach (var carbon in Carbons)
                    carbon.Move();

                Thread.Sleep(10);
            }

            if (!IsPaused)
                Console.Clear();
        }
    }

    static void BuildCarbon()
    {
        Random r = new();

        for (int i = 0; i < CARBON_COUNT; i++)
        {
            var x = r.Next(0, Console.WindowWidth);
            var y = r.Next(0, Console.WindowHeight);

            int velocityX = r.Next(-2, 3);
            int velocityY = r.Next(-2, 3);

            Carbon carbon = new(x, y, velocityX, velocityY);
            Carbons.Add(carbon);
        }
    }

    static void PauseSpace()
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Spacebar)
                IsPaused = !IsPaused;
        }
    }
}
