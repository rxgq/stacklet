using System.Security.Cryptography.X509Certificates;
using tgm.Types;

namespace tgm;

public abstract class ConsoleEngine
{
    public static List<Sprite2D> Sprites = new();

    public static bool HideCursor { get; set; }

    public static void Start()
    {
        if (HideCursor)
            Console.CursorVisible = false;

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
