namespace tgm.Types;

public class Sprite2D
{
    public Vector2 Position { get; set; }

    public char Character { get; set; }

    public Scene2D Scene { get; set; }

    public ConsoleColor Color { get; set; }

    public Sprite2D(Vector2 position, Scene2D scene, char character, ConsoleColor color)
    {
        Position = position;
        Character = character;
        Scene = scene;
        Color = color;

        scene.Sprites.Add(this);
    }

    public void DestroySelf() => ConsoleEngine.Sprites.Remove(this); 
}
