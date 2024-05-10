namespace terminal;

internal class Program
{
    static void Main()
    {
        Console.CursorVisible = false;

        while (true) 
        {
            try 
            { 
                Terminal.Run();
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }
    }
}
