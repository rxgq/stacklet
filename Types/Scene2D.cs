namespace tgm.Types;

public class Scene2D
{
    public List<Sprite2D> Sprites = new();

    public List<Shape2D> Shapes = new();

    public string? Name { get; set; }

    public Scene2D(string name) 
    {
        Name = name;
    }
}

