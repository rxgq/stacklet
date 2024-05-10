namespace terminal;

internal class Display
{
    public static ConsoleColor PointerColour = ConsoleColor.White;
    public static ConsoleColor LineNumberColour = ConsoleColor.DarkGray;
    public static ConsoleColor CommandColour = ConsoleColor.DarkMagenta;

    public static void GreyText(string text) 
    {
        Console.ForegroundColor = LineNumberColour;
        Console.WriteLine(text);    

        Console.ResetColor();
    }

    public static void WhiteText(string text)
    {
        Console.ForegroundColor = PointerColour;
        Console.Write(text);

        Console.ResetColor();
    }
}
