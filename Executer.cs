namespace assembly;


internal class Executer
{
    private Dictionary<string, int> Registers = new()
    {
        // 32-bit accumulator
        { "EAX", 0 },

        // 32-bit base
        { "EBX", 0 },

        // 32-bit counter
        { "ECX", 0 },
        
        // 32-bit data
        { "EDX", 0 },
    };

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
        _ = Instruction().Type switch
        {
            InstructionType.ADD => ExecuteAdd(), 
            InstructionType.SUB => ExecuteSub(),
            InstructionType.MOV => ExecuteMov(),
            InstructionType.PRT => ExecutePrt(),
            InstructionType.INC => ExecuteInc(),
            InstructionType.DEC => ExecuteDec(),

            _ => throw new Exception("INSTRUCTION NOT REGISTERED")
        };

        return null;
    }

    private object ExecuteAdd() 
    {
        var instruction = Instruction();

        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        Registers[reg1] = Registers[reg2];
        return null;
    }

    private object ExecuteSub()
    {


        return null;
    }

    private object ExecuteMov()
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        Registers[reg2] = Registers[reg1];
        return null;
    }

    private object ExecutePrt() 
    { 
        var instruction = Instruction();    
        var reg1 = instruction.Parameters[0];

        Console.Write(Registers[reg1]);
        return null;
    }

    private object ExecuteInc() 
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        Registers[reg1]++;

        return null;
    }

    private object ExecuteDec() 
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        Registers[reg1]--;

        return null;
    }

    private Instruction Instruction()
        => Instructions[Current]; 
    
    private bool IsEOFToken() 
        => Instructions[Current].Type == InstructionType.EOF;
}
