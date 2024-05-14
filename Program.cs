using tgm.Types;

namespace tgm;

internal class Program
{
    static void Main()
    {
        Scene2D grasslandsScene = new("Grasslands");

        ConsoleEngine.RegisterSprite(new Sprite2D(Vector2.Random(), grasslandsScene, 'P', ConsoleColor.White));
        ConsoleEngine.RegisterSprite(new Sprite2D(Vector2.Random(), grasslandsScene, 'T', ConsoleColor.Red));

        ConsoleEngine.HideCursor = true;
        ConsoleEngine.HasGravity = false;
        ConsoleEngine.ActiveScene = grasslandsScene;

        ConsoleEngine.Start();
    }
}
