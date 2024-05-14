using System.Security.Cryptography.X509Certificates;

namespace tgm;

public class ConsoleEngine
{
    List<Sprite2D> Sprites = new();

    public ConsoleEngine() 
    { 
        
    }

    public void Start()
    {
        foreach (var sprite in Sprites) 
        { 
            Console.SetCursorPosition(sprite.BasePosition.X, sprite.BasePosition.Y);
            Console.Write(sprite.Character);
        }
    }

    public void RegisterSprite(Sprite2D sprite) 
    {
        Sprites.Add(sprite);
    }
}
