namespace assembly;


internal class Executer
{
    private Dictionary<string, int> Registers = new()
    {
        // 32-bit accumulator
        { "eax", 0 },

        // 32-bit base
        { "ebx", 0 },

        // 32-bit counter
        { "ecx", 0 },
        
        // 32-bit data
        { "edx", 0 },
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
            InstructionType.COMMENT => null,

            _ => throw new Exception("INSTRUCTION NOT REGISTERED")
        };

        return null;
    }

    private object ExecuteAdd() 
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        if (IsRegister(reg2))
            Registers[reg1] += Registers[reg2];

        else
            Registers[reg1] += Convert.ToInt32(reg2);
        return null;
    }

    private object ExecuteSub()
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        if (IsRegister(reg2))
            Registers[reg1] -= Registers[reg2];

        else 
            Registers[reg1] -= Convert.ToInt32(reg2);


        return null;
    }

    private object ExecuteMov()
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        if (IsRegister(reg2))
            Registers[reg1] = Registers[reg2];

        else
            Registers[reg1] += Convert.ToInt32(reg2);

        return null;
    }

    private object ExecutePrt() 
    { 
        var instruction = Instruction();    
        var reg1 = instruction.Parameters[0];

        Console.Write($"{reg1.ToUpper()}: {Registers[reg1]}\n");

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

    private bool IsRegister(string key)
        => Registers.ContainsKey(key);

    private Instruction Instruction()
        => Instructions[Current]; 
    
    private bool IsEOFToken() 
        => Instructions[Current].Type == InstructionType.EOF;
}
