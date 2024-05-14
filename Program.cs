using tgm.Types;

namespace tgm;

internal class Program
{
    static void Main()
    {
        Scene2D scene1 = new("Scene 1");

        ConsoleEngine.RegisterSprite(new Sprite2D(Vector2.Zero(), scene1, 'P'));


        ConsoleEngine.HideCursor = true;
        ConsoleEngine.HasGravity = false;
        ConsoleEngine.ActiveScene = scene1;

        ConsoleEngine.Start();
    }
}
