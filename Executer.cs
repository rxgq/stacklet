namespace assembly;


internal class Executer
{
    private readonly Dictionary<string, int> Registers = new()
    {
        { "eax", 0 }, { "ebx", 0 }, { "ecx", 0 }, { "edx", 0 },
    };

    public List<Instruction> Instructions { get; set; }

    public int Current { get; set; } = 0;

    public Executer(List<Instruction> instructions)
    {
        Instructions = instructions;
    }

    public void Execute() 
    {
        while (!IsEOFInstruction()) 
        {
            ExecuteInstruction();
            Current++;
        }
    }

    private void ExecuteInstruction() 
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
    }

    private object ExecuteAdd() 
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        // if object to add is register, add contents...
        if (IsRegister(reg2))
            Registers[reg1] += Registers[reg2];

        // ...otherwise convert to int and add value directly
        else
            Registers[reg1] += Convert.ToInt32(reg2);

        return new object();
    }

    private object ExecuteSub()
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        // if object to subtract is register, subtract contents...
        if (IsRegister(reg2))
            Registers[reg1] -= Registers[reg2];

        // ...otherwise convert to int and subtract value directly
        else 
            Registers[reg1] -= Convert.ToInt32(reg2);

        return new object();
    }

    private object ExecuteMov()
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];
        var reg2 = instruction.Parameters[1];

        // if object to add is register, set contents...
        if (IsRegister(reg2))
            Registers[reg1] = Registers[reg2];

        // ...otherwise convert to int and set value directly
        else
            Registers[reg1] = Convert.ToInt32(reg2);

        return new object();
    }

    private object ExecutePrt() 
    { 
        var instruction = Instruction();    
        var reg1 = instruction.Parameters[0];

        // output the register value
        Console.Write($"{reg1.ToLower()}: {Registers[reg1]}\n");

        return new object();
    }

    private object ExecuteInc() 
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];

        // increment the register value
        Registers[reg1]++;

        return new object();
    }

    private object ExecuteDec() 
    {
        var instruction = Instruction();
        var reg1 = instruction.Parameters[0];

        // decrement the register value
        Registers[reg1]--;

        return new object();
    }

    private bool IsRegister(string key)
        => Registers.ContainsKey(key);

    private Instruction Instruction()
        => Instructions[Current]; 
    
    private bool IsEOFInstruction() 
        => Instructions[Current].Type == InstructionType.EOF;
}