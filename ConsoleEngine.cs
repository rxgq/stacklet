using System.Security.Cryptography.X509Certificates;
using tgm.Types;
using static System.Formats.Asn1.AsnWriter;

namespace tgm;

public abstract class ConsoleEngine
{
    public static List<Sprite2D> Sprites = new();

    public static bool HideCursor { get; set; }

    public static bool HasGravity { get; set; }

    public static Scene2D? ActiveScene { get; set; }

    public static void Start()
    {
        if (HideCursor)
            Console.CursorVisible = false;

        if (ActiveScene is null) 
            throw new NullReferenceException("An active scene is required to run the engine.");

        try 
        { 
            while (true) 
            {
                Render();
            }
        }
        catch (Exception) 
        { 
        
        }
    }

    public static void Render() 
    {
        foreach (var sprite in Sprites)
        {
            Console.SetCursorPosition(sprite.BasePosition.X, sprite.BasePosition.Y);
            Console.Write(sprite.Character);
        }
    }

    public static void RegisterSprite(Sprite2D sprite) 
    {
        Sprites.Add(sprite);
    }

    public static void UnregisterSprite(Sprite2D sprite) 
    {
        Sprites.Remove(sprite);
    }
}
