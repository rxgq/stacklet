using tgm.Types;

namespace tgm;

internal class Program
{
    static void Main()
    {
        Scene2D grasslandsScene = new("Grasslands");
        Scene2D rockyHillsScene = new("RockyHills");

        ConsoleEngine.RegisterSprite(new Sprite2D(Vector2.Zero(), grasslandsScene, 'P', ConsoleColor.White));
        ConsoleEngine.RegisterSprite(new Sprite2D(new Vector2(10, 10), grasslandsScene, 'T', ConsoleColor.Red));

        ConsoleEngine.HideCursor = true;
        ConsoleEngine.HasGravity = false;
        ConsoleEngine.ActiveScene = grasslandsScene;

        ConsoleEngine.Start();
    }
}
