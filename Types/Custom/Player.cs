namespace tgm.Types.Custom;

public class Player : Sprite2D
{
    private bool isJumping = false;
    private int jumpTimer = 0;
    private int jumpDuration = 10;

    public Player(Vector2 position, Scene2D scene, char character, ConsoleColor color)
        : base(position, scene, character, color)
    {

    }

    public void Move(ConsoleKeyInfo keyPressed)
    {
        Vector2 previousPosition = new(Position.X, Position.Y);

        if (keyPressed.Key == ConsoleKey.UpArrow && !ConsoleEngine.HasGravity)
            Position.Y -= 1;

        if (keyPressed.Key == ConsoleKey.DownArrow)
            Position.Y += 1;

        if (keyPressed.Key == ConsoleKey.LeftArrow)
            Position.X -= 1;

        if (keyPressed.Key == ConsoleKey.RightArrow)
            Position.X += 1;

        if (!isJumping && keyPressed.Key == ConsoleKey.Spacebar)
            StartJump();

        if (isJumping)
            HandleJump();

        Console.SetCursorPosition(previousPosition.X, previousPosition.Y);
        Console.Write(" ");

        Vector2 nextPosition = new(Position.X, Position.Y);

        if (IsCollision(nextPosition, this))
        {
            Position = previousPosition;
        }
        else
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(Character);
        }
    }


    private void StartJump()
    {
        isJumping = true;
        jumpTimer = 0;
    }

    private void HandleJump()
    {
        if (jumpTimer < jumpDuration)
        {
            Position.Y -= 1;
            jumpTimer++;
        }
        else if (Position.Y < Console.WindowHeight - 1) 
            Position.Y += 1;

        else
            isJumping = false;
    }

    public static bool IsCollision(Vector2 position, Sprite2D sprite)
    {
        if (position.X < 0 || position.X >= Console.WindowWidth ||
            position.Y < 0 || position.Y >= Console.WindowHeight)
            return true;

        foreach (var registeredSprite in ConsoleEngine.Sprites)
        {
            if (registeredSprite.Position.X == sprite.Position.X &&
                registeredSprite.Position.Y == sprite.Position.Y && registeredSprite != sprite)
                return true;
        }

        foreach (var registeredShape in ConsoleEngine.ActiveScene.Shapes)
        {
            if (registeredShape.Position.X == sprite.Position.X &&
                registeredShape.Position.Y == sprite.Position.Y)
                return true;
        }

        return false;
    }
    public bool IsGrounded() => Position.Y == Console.WindowHeight - 1;

    public void ApplyGravity()
    {
        var downArrowKeyPress = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);

        if (!IsGrounded())
            Move(downArrowKeyPress);
    }
}

