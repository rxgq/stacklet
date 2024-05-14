using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using tgm.Types;
using tgm.Types.Custom;

namespace tgm;

public abstract class ConsoleEngine
{
    private static Thread? GameLoop;

    private static bool ExitRequested = false;
    private static bool ShapesRendered = false;

    public static List<Sprite2D> Sprites = new();

    public static List<Shape2D> Shapes = new();

    public static bool HideCursor { get; set; }
    public static bool HasGravity { get; set; }
    public static Scene2D ActiveScene { get; private set; }


    public static void Start()
    {
        if (HideCursor)
            Console.CursorVisible = false;

        if (ActiveScene is null)
            throw new NullReferenceException("An active scene is required to run the engine.");

        GameLoop = new Thread(Run);
        GameLoop.Start();
    }

    public static void Finish() => ExitRequested = true;

    private static void Run()
    {
        try
        {
            while (!ExitRequested)
            {
                if (HasGravity)
                {
                    if (ActiveScene.Sprites.FirstOrDefault(e => e.GetType() == typeof(Player)) is Player player)
                    {
                        if (!player.IsGrounded())
                        {
                            player.ApplyGravity();
                        }
                    }
                }

                Render();

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    Player? player = ActiveScene.Sprites.FirstOrDefault(e => e.GetType() == typeof(Player)) as Player;

                    if (player is not null)
                        player!.Move(key);

                    if (key.Key == ConsoleKey.Escape)
                        ExitRequested = true;
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

        if (ShapesRendered)
            return;

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

        ShapesRendered = true;
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
        //if (!IsPositionAvailable(shape.Position, shape.Scene, shape))
            //throw new Exception("A sprite at this vector position has already been registered");

        Shapes.Add(shape);
    }

    public static void UnregisterShape(Shape2D shape)
    {
        Shapes.Remove(shape);
    }

    public static void RegisterManyShapes(Vector2 startPosition, Vector2 endPosition, Scene2D scene)
    {
        int dx = Math.Abs(endPosition.X - startPosition.X);
        int dy = Math.Abs(endPosition.Y - startPosition.Y);
        int sx = startPosition.X < endPosition.X ? 1 : -1;
        int sy = startPosition.Y < endPosition.Y ? 1 : -1;
        int err = dx - dy;

        int x = startPosition.X;
        int y = startPosition.Y;

        while (true)
        {
            RegisterShape(new Block(new Vector2(x, y), scene, '0', ConsoleColor.DarkGray));

            if (x == endPosition.X && y == endPosition.Y)
                break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y += sy;
            }
        }
    }

    public static void GenerateVoronoiNoise(int variation, int points, Scene2D scene, List<ConsoleColor> colors, List<char> characters)
    {
        Random r = new();
        List<Vector2> vectors = new();

        for (int i = 0; i < points; i++)
        {
            int x = r.Next(Console.WindowWidth);
            int y = r.Next(Console.WindowHeight);

            vectors.Add(new(x, y));
        }

        for (int y = 0; y < Console.WindowHeight; y++)
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                int minimumDistance = int.MaxValue;

                foreach (var vector in vectors)
                {
                    int distance = (int)Math.Pow(vector.X - x, 2) + (int)Math.Pow(vector.Y - y, 2);

                    if (distance < minimumDistance)
                        minimumDistance = distance;
                }

                if (minimumDistance > variation)
                    RegisterShape(new Shape2D(new Vector2(x, y), scene, characters[r.Next(0, characters.Count)], colors[r.Next(0, colors.Count)]));
            }
        }
    }

    public static void SetScene(Scene2D scene) 
    {
        Console.Clear();
        ActiveScene = scene;
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
