namespace tgm.Types;

public class Shape2D
{
    public Vector2 Position { get; set; }

    public char Character { get; set; }

    public Scene2D Scene { get; set; }

    public ConsoleColor Color { get; set; }

    public Shape2D(Vector2 position, Scene2D scene, char character, ConsoleColor color)
    {
        Position = position;
        Character = character;
        Scene = scene;
        Color = color;

        scene.Shapes.Add(this);
    }

    public void DestroySelf() => ConsoleEngine.Shapes.Remove(this);
}
