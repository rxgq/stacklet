using tgm.Types;
using tgm.Types.Custom;

namespace tgm;

internal class Program
{
    static void Main()
    {
        Scene2D grasslandsScene = new("Grasslands");

        ConsoleEngine.RegisterSprite(new Player(Vector2.Zero(), grasslandsScene, 'P', ConsoleColor.White));

        RegisterManyShapes(startPosition: new Vector2(3, 3), endPosition: new Vector2(10, 20), grasslandsScene);

        ConsoleEngine.HideCursor = true;
        ConsoleEngine.HasGravity = false;
        ConsoleEngine.ActiveScene = grasslandsScene;

        ConsoleEngine.Start();
    }

    public static void RegisterManyShapes(Vector2 startPosition, Vector2 endPosition, Scene2D scene)
    {
        int dx = Math.Abs(endPosition.X - startPosition.X);
        int dy = Math.Abs(endPosition.Y - startPosition.Y);
        int sx = startPosition.X < endPosition.X ? 1 : -1;
        int sy = startPosition.Y < endPosition.Y ? 1 : -1;
        int err = dx - dy;

        int x = startPosition.X;
        int y = startPosition.Y;

        while (true)
        {
            ConsoleEngine.RegisterShape(new Block(new Vector2(x, y), scene, '0', ConsoleColor.DarkGray));

            if (x == endPosition.X && y == endPosition.Y)
                break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y += sy;
            }
        }
    }
}
