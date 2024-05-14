namespace tgm;

internal class Program
{
    static void Main()
    {
        ConsoleEngine engine = new();

        Player player = new(Vector2.Zero(), '0');

        engine.Start();
    }
}
