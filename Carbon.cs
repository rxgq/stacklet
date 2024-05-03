using System.Security.Cryptography;

namespace lang;

public class Carbon
{
    public char Symbol = 'C';
    public ConsoleColor Color = ConsoleColor.DarkGray;
    public int X { get; set; }
    public int Y { get; set; }
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }

    public Carbon(int x, int y, int velocityX, int velocityY)
    {
        X = x;
        Y = y;

        VelocityX = velocityX;
        VelocityY = velocityY;
    }

    public void Move()
    {
        int newX = X + VelocityX;
        int newY = Y + VelocityY;

        foreach (var carbon in Space.Carbons) 
        { 
            
        }

        if (newX < 0)
        {
            newX = 0;
            VelocityX *= -1;
        }
        else if (newX >= Console.WindowWidth)
        {
            newX = Console.WindowWidth - 1;
            VelocityX *= -1;
        }

        if (newY < 0)
        {
            newY = 0;
            VelocityY *= -1;
        }
        else if (newY >= Console.WindowHeight)
        {
            newY = Console.WindowHeight - 1;
            VelocityY *= -1;
        }

        if (X >= 0 && X < Console.WindowWidth && Y >= 0 && Y < Console.WindowHeight)
        {
            X = newX;
            Y = newY;

            WriteCarbon();
        }
    }

    public void WriteCarbon()
    {
        Console.SetCursorPosition(X, Y);
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
    }
}