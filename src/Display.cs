namespace terminal.src;

internal class Display
{
    public static ConsoleColor PointerColour = ConsoleColor.White;
    public static ConsoleColor PreviousPointerColour = ConsoleColor.Gray;
    public static ConsoleColor LineNumberColour = ConsoleColor.DarkGray;
    public static ConsoleColor CommandColour = ConsoleColor.DarkMagenta;

    public static void ColouredText(string text, ConsoleColor Colour)
    {
        Console.ForegroundColor = Colour;
        Console.Write(text);

        Console.ResetColor();
    }

    public static string? ColouredInput(ConsoleColor Colour)
    {
        Console.ForegroundColor = Colour;
        return Console.ReadLine();
    }
}