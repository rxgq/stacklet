namespace tgm.Types;

public class Player : Sprite2D
{
    public Player(Vector2 position, Scene2D scene, char character, ConsoleColor color) : base(position, scene, character, color)
    {

    }

    public void Move(ConsoleKeyInfo keyPressed) 
    {
        Vector2 previousPosition = new(CurrentPosition.X, CurrentPosition.Y);

        if (keyPressed.Key == ConsoleKey.UpArrow)
            CurrentPosition.Y += -1;

        if (keyPressed.Key == ConsoleKey.DownArrow)
            CurrentPosition.Y += 1;

        if (keyPressed.Key == ConsoleKey.LeftArrow)
            CurrentPosition.X += -1;

        if (keyPressed.Key == ConsoleKey.RightArrow)
            CurrentPosition.X += 1;

        Console.SetCursorPosition(previousPosition.X, previousPosition.Y);
        Console.Write(" ");

        Console.SetCursorPosition(CurrentPosition.X, CurrentPosition.Y);
        Console.Write(Character);
    }
}
