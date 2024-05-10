namespace terminal;

internal class Display
{
    public static void GreyText(string text) 
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(text);    

        Console.ResetColor();
    }

    public static void WhiteText(string text)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(text);

        Console.ResetColor();
    }
}
