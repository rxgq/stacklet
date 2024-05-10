namespace terminal;

internal class Display
{
    public static ConsoleColor PointerColour = ConsoleColor.White;
    public static ConsoleColor LineNumberColour = ConsoleColor.DarkGray;
    public static ConsoleColor CommandColour = ConsoleColor.DarkMagenta;

    public static void ColouredText(string text, ConsoleColor Color) 
    {
        Console.ForegroundColor = Color;
        Console.Write(text);    

        Console.ResetColor();
    }
}
