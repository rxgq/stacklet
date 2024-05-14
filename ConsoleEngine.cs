using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using tgm.Types;
using tgm.Types.Custom;

namespace tgm;

public abstract class ConsoleEngine
{
    private static Thread? gameLoop;

    public static List<Sprite2D> Sprites = new();

    public static List<Shape2D> Shapes = new();

    public static bool HideCursor { get; set; }
    public static bool HasGravity { get; set; }
    public static Scene2D ActiveScene { get; set; }

    public static void Start()
    {
        if (HideCursor)
            Console.CursorVisible = false;

        if (ActiveScene is null)
            throw new NullReferenceException("An active scene is required to run the engine.");

        gameLoop = new Thread(GameLoop);
        gameLoop.Start();
    }

    private static void GameLoop()
    {
        try
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                Render();

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    Player? player = Sprites.FirstOrDefault(e => e.GetType() == typeof(Player)) as Player;
                    player!.Move(key);

                    if (key.Key == ConsoleKey.Escape)
                        exitRequested = true;
                }
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

        foreach (var shape in Shapes)
        {
            if (shape.Scene == ActiveScene)
            {
                Console.ForegroundColor = shape.Color;

                Console.SetCursorPosition(shape.Position.X, shape.Position.Y);
                Console.Write(shape.Character);

                Console.ResetColor();
            }
        }
    }

    public static void RegisterSprite(Sprite2D sprite)
    {
        if (!IsPositionAvailable(sprite.Position, sprite.Scene, sprite))
            throw new Exception("A sprite at this vector position has already been registered");

        Sprites.Add(sprite);
    }

    public static void UnregisterSprite(Sprite2D sprite)
    {
        Sprites.Remove(sprite);
    }

    public static void RegisterShape(Shape2D shape)
    {
        if (!IsPositionAvailable(shape.Position, shape.Scene, shape))
            throw new Exception("A sprite at this vector position has already been registered");

        Shapes.Add(shape);
    }

    public static void UnregisterShape(Shape2D shape)
    {
        Shapes.Remove(shape);
    }

    public static bool IsPositionAvailable(Vector2 position, Scene2D scene, dynamic obj) // ew dynamic
    {
        foreach (var registeredSprite in scene.Sprites)
        {
            if (registeredSprite.Position.X == position.X &&
                registeredSprite.Position.Y == position.Y && registeredSprite != obj)
            {
                return false;
            }
        }

        foreach (var registeredShape in scene.Shapes)
        {
            if (registeredShape.Position.X == position.X &&
                registeredShape.Position.Y == position.Y && registeredShape != obj)
            {
                return false;
            }
        }

        return true;
    }
}
