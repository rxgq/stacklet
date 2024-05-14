namespace tgm.Types;

public class Sprite2D
{
    public Vector2 BasePosition { get; set; }

    public Vector2 CurrentPosition { get; set; }

    public char Character { get; set; }

    public Scene2D? Scene { get; set; }

    public Sprite2D(Vector2 position, Scene2D scene, char character)
    {
        BasePosition = position;
        Character = character;
        CurrentPosition = position;
        Scene = scene;

        scene.Sprites.Add(this);
    }

    public void DestroySelf() => ConsoleEngine.Sprites.Remove(this); 
}
