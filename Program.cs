namespace tgm;

internal class Program
{
    static void Main()
    {
        ConsoleEngine engine = new();
        engine.RegisterSprite(new Player(Vector2.Zero(), 'P'));

        engine.Start();
    }
}
