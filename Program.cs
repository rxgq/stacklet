using tgm.Types;
using tgm.Types.Custom;

namespace tgm;

internal class Program
{
    static void Main()
    {
        Scene2D Level1 = new("Grasslands");

        ConsoleEngine.GenerateVoronoiNoise(
            variation: 20, points: 70, Level1,          
            new List<ConsoleColor>() { ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.Red, ConsoleColor.DarkRed }, 
            new List<char>() { 'x', 'o', '.', '#', '@', '<', '>' }
        );

        ConsoleEngine.RegisterSprite(new Player(Vector2.Random(), Level1, '@', ConsoleColor.Cyan));

        ConsoleEngine.HideCursor = true;
        ConsoleEngine.HasGravity = true;
        ConsoleEngine.SetScene(Level1);

        ConsoleEngine.Start();
    }
}
