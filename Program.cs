using tgm.Types;
using tgm.Types.Custom;

namespace tgm;

internal class Program
{
    static void Main()
    {
        Scene2D grasslandsScene = new("Grasslands");

        ConsoleEngine.RegisterSprite(new Player(Vector2.Zero(), grasslandsScene, 'P', ConsoleColor.White));
        ConsoleEngine.RegisterShape(new Block(new Vector2(10, 10), grasslandsScene, 'P', ConsoleColor.White));

        ConsoleEngine.HideCursor = true;
        ConsoleEngine.HasGravity = false;
        ConsoleEngine.ActiveScene = grasslandsScene;

        ConsoleEngine.Start();
    }
}
