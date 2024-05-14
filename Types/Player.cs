namespace tgm.Types;

public class Player : Sprite2D
{
    public Player(Vector2 position, Scene2D scene, char character, ConsoleColor color) : base(position, scene, character, color)
    {

    }

    public void Move(ConsoleKeyInfo keyPressed) 
    {
        Vector2 previousPosition = new(Position.X, Position.Y);

        if (keyPressed.Key == ConsoleKey.UpArrow)
            Position.Y += -1;

        if (keyPressed.Key == ConsoleKey.DownArrow)
            Position.Y += 1;

        if (keyPressed.Key == ConsoleKey.LeftArrow)
            Position.X += -1;

        if (keyPressed.Key == ConsoleKey.RightArrow)
            Position.X += 1;

        Console.SetCursorPosition(previousPosition.X, previousPosition.Y);
        Console.Write(" ");

        if (IsCollision(Position, this)) 
        {
            Position = previousPosition;
            return;
        }

        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(Character);
    }

    public static bool IsCollision(Vector2 position, Sprite2D sprite)
    {
        if (position.X < 0 || position.X >= Console.WindowWidth ||
            position.Y < 0 || position.Y >= Console.WindowHeight)
            return true;

        foreach (var registeredSprite in ConsoleEngine.Sprites)
        {
            if (position.Equals(sprite.Position) && registeredSprite != sprite)
                return true;
        }

        return false;
    }

}
