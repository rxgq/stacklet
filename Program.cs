using tgm.Types;
using tgm.Types.Custom;

namespace tgm;

internal class Program
{
    static void Main()
    {
        Scene2D grasslandsScene = new("Grasslands");
        Scene2D rockyHillsScene = new("Rocky Hills");

        ConsoleEngine.GenerateVoronoiNoise(
            seed: 10, points: 140, grasslandsScene,          
            new List<ConsoleColor>() { ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.Gray }, 
            new List<char>() { 'x', 'o', '.' }
        );

        ConsoleEngine.HideCursor = true;
        ConsoleEngine.HasGravity = false;
        ConsoleEngine.SetScene(grasslandsScene);

        ConsoleEngine.Start();
    }
}
