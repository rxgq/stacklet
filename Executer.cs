namespace assembly;


internal class Executer
{

    public List<Instruction> Instructions { get; set; }

    public int Current { get; set; } = 0;

    public Executer(List<Instruction> instructions)
    {
        Instructions = instructions;
    }

    public object? Execute() 
    {
        object? result = null;
        while (!IsEOFToken()) 
        {
            result = ExecuteToken();
            Current++;
        }

        return result;
    }

    private object ExecuteToken() 
    {
        return Instruction() switch
        {
            InstructionType.ADD => ExecuteAdd(), 
            InstructionType.SUB => ExecuteSub(),
            InstructionType.MOV => ExecuteMov(),

            _ => throw new Exception("INSTRUCTION NOT REGISTERED")
        };

        return null;
    }

    private object ExecuteAdd() 
    {


        return null;
    }

    private object ExecuteSub()
    {


        return null;
    }

    private object ExecuteMov()
    {


        return null;
    }

    private InstructionType Instruction()
        => Instructions[Current].Type; 
    
    private bool IsEOFToken() 
        => Instructions[Current].Type == InstructionType.EOF;
}
