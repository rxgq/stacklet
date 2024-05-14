namespace tgm.Types;

public class Vector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x; Y = y;
    }

    public static Vector2 Zero() => new(0, 0);

    public static Vector2 Random()
    {
        Random r = new();

        int x = r.Next(0, Console.WindowWidth);
        int y = r.Next(0, Console.WindowHeight);

        return new Vector2(x, y);
    }

    public double DistanceTo(Vector2 other)
    {
        int dx = X - other.X;
        int dy = Y - other.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }
}
