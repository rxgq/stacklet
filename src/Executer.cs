namespace assembly.src;


internal class Executer
{
    private readonly Dictionary<string, int> Registers = new()
    {
        { "eax", 0 }, { "ebx", 0 }, { "ecx", 0 }, { "edx", 0 },
    };

    public List<Instruction> Instructions { get; set; }

    public int Current { get; set; } = 0;

    public string Param1 => Instruction().Parameters[0];
    public string Param2 => Instruction().Parameters[1];

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
            InstructionType.MUL => ExecuteMul(),
            InstructionType.DIV => ExecuteDiv(),
            InstructionType.INC => ExecuteInc(),
            InstructionType.DEC => ExecuteDec(),

            InstructionType.AND => ExecuteAnd(),
            InstructionType.OR => ExecuteOr(),
            InstructionType.XOR => ExecuteXor(),

            InstructionType.MOV => ExecuteMov(),
            InstructionType.PRT => ExecutePrt(),

            InstructionType.COMMENT => null,

            _ => throw new Exception("INSTRUCTION NOT REGISTERED")
        };
    }

    private object ExecuteAdd()
    {
        // if object to add is register, add contents...
        if (IsRegister(Param2))
            Registers[Param1] += Registers[Param2];

        // ...otherwise convert to int and add value directly
        else
            Registers[Param1] += Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteSub()
    {
        // if object to subtract is register, subtract contents...
        if (IsRegister(Param2))
            Registers[Param1] -= Registers[Param2];

        // ...otherwise convert to int and subtract value directly
        else
            Registers[Param1] -= Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteMul()
    {
        // if object to multiply is register, multiply contents...
        if (IsRegister(Param2))
            Registers[Param1] *= Registers[Param2];

        // ...otherwise convert to int and multiply value directly
        else
            Registers[Param1] *= Convert.ToInt32(Param2);

        return new object();
    }
    private object ExecuteDiv()
    {
        // if object to subtract is register, divide contents...
        if (IsRegister(Param2))
            Registers[Param1] /= Registers[Param2];

        // ...otherwise convert to int and divide value directly
        else
            Registers[Param1] /= Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteInc()
    {
        // increment the register value
        Registers[Param1]++;

        return new object();
    }

    private object ExecuteDec()
    {
        // decrement the register value
        Registers[Param1]--;

        return new object();
    }

    private object ExecuteMov()
    {
        // if object to add is register, set contents...
        if (IsRegister(Param2))
            Registers[Param1] = Registers[Param2];

        // ...otherwise convert to int and set value directly
        else
            Registers[Param1] = Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecutePrt()
    {
        // output the register value
        Console.Write($"{Param1.ToLower()}: {Registers[Param1]}\n");

        return new object();
    }

    private object ExecuteAnd()
    {
        // apply logical and
        Registers[Param1] &= Registers[Param2];

        return new object();
    }


    private object ExecuteOr()
    {
        // apply logical or
        Registers[Param1] |= Registers[Param2];

        return new object();
    }

    private object ExecuteXor()
    {
        // apply logical xor
        Registers[Param1] ^= Registers[Param2];

        return new object();
    }

    private bool IsRegister(string key)
        => Registers.ContainsKey(key);

    private Instruction Instruction()
        => Instructions[Current];

    private bool IsEOFInstruction()
        => Instructions[Current].Type == InstructionType.EOF;
}