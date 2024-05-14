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

    // physics object

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

                var key = Console.ReadKey();
                Player? player = Sprites.FirstOrDefault(e => e.GetType() == typeof(Player)) as Player;
                player!.Move(key);
            }
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public static void Render() 
    {
        foreach (var sprite in Sprites)
        {
            if (sprite.Scene == ActiveScene)
            {
                Console.ForegroundColor = sprite.Color;

                Console.SetCursorPosition(sprite.Position.X, sprite.Position.Y);
                Console.Write(sprite.Character);

                Console.ResetColor();
            }
        }
    }

    public static void RegisterSprite(Sprite2D sprite) 
    {
        foreach (var registeredSprite in sprite.Scene.Sprites) 
        {
            if (registeredSprite.Position.X == sprite.Position.X &&
                registeredSprite.Position.Y == sprite.Position.Y && registeredSprite != sprite) 
            {
                throw new Exception("A sprite at this vector position has already been registered");
            }
        }

        Sprites.Add(sprite);
    }

    public static void UnregisterSprite(Sprite2D sprite) 
    {
        Sprites.Remove(sprite);
    }
}
