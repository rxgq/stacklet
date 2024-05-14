using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using tgm.Types;

namespace tgm
{
    public abstract class ConsoleEngine
    {
        private static Thread? gameLoopThread;

        public static List<Sprite2D> Sprites = new();
        public static bool HideCursor { get; set; }
        public static bool HasGravity { get; set; }
        public static Scene2D? ActiveScene { get; set; }

        public static void Start()
        {
            if (HideCursor)
                Console.CursorVisible = false;

            if (ActiveScene is null)
                throw new NullReferenceException("An active scene is required to run the engine.");

            if (gameLoopThread != null && gameLoopThread.IsAlive)
                throw new InvalidOperationException("The game loop is already running.");

            gameLoopThread = new Thread(GameLoop);
            gameLoopThread.Start();
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
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            foreach (var registeredSprite in sprite.Scene.Sprites)
            {
                if (registeredSprite.Position.X == sprite.Position.X &&
                    registeredSprite.Position.Y == sprite.Position.Y && registeredSprite != sprite)
                {
                    throw new Exception("A sprite at this vector position has already been registered");
                }
            }

            Sprites.Add(sprite);
        }

        public static void UnregisterSprite(Sprite2D sprite)
        {
            Sprites.Remove(sprite);
        }
    }
}
