namespace tgm;

internal class Player
{
    public Vector2 BasePosition { get; set; }

    public Vector2 CurrentPosition { get; set; }

    public char Character { get; set; }

    public Player(Vector2 position, char character) 
    { 
        BasePosition = position;
        Character = character;
        CurrentPosition = position;
    }
}
