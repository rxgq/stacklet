using tgm.Types;

namespace tgm;

internal class Program
{
    static void Main()
    {
        ConsoleEngine.RegisterSprite(new Sprite2D(Vector2.Zero(), 'P'));
        ConsoleEngine.HideCursor = true;

        ConsoleEngine.Start();
    }
}
