namespace assembly.src;

internal class Executer
{
    private readonly Dictionary<string, int> Registers = new()
    {
        { "eax", 0 }, { "ebx", 0 }, { "ecx", 0 }, { "edx", 0 },
    };

    private readonly Dictionary<string, int> Processes = new();
    private readonly Stack<int> CallStack = new();

    public List<Instruction> Instructions { get; set; }
    public int Pointer { get; set; } = 0;

    public string Param1 => Instruction.Parameters.Count > 0 ? Instruction.Parameters[0] : string.Empty;
    public string Param2 => Instruction.Parameters.Count > 1 ? Instruction.Parameters[1] : string.Empty;
    public Instruction Instruction => Instructions[Pointer];

    public Executer(List<Instruction> instructions)
    {
        Instructions = instructions;
        PreProcessProcs();
    }

    private void PreProcessProcs()
    {
        int instIdx = -1;
        foreach (var inst in Instructions)
        {
            instIdx++;
            if (inst.Type == Inst.PROC)
                Processes[inst.Identifier!] = instIdx;
        }
    }

    public void Execute()
    {
        while (!IsEOFInstruction())
        {
            ExecuteInstruction();
            Pointer++;
        }
    }

    private void ExecuteInstruction()
    {
        if (CallStack.Count == 0 && Instruction.Type == Inst.PROC)
        {
            Pointer--;
            return;
        }

        _ = Instruction.Type switch
        {
            Inst.ADD => ExecuteAdd(),
            Inst.SUB => ExecuteSub(),
            Inst.MUL => ExecuteMul(),
            Inst.DIV => ExecuteDiv(),
            Inst.INC => ExecuteInc(),
            Inst.DEC => ExecuteDec(),

            Inst.AND => ExecuteAnd(),
            Inst.OR => ExecuteOr(),
            Inst.XOR => ExecuteXor(),

            Inst.MOV => ExecuteMov(),
            Inst.PRT => ExecutePrt(),
            Inst.OUT => ExecuteOut(),
            Inst.WT => ExecuteWt(),

            Inst.JMP => ExecuteJmp(),
            Inst.JNZ => ExecuteJnz(),
            Inst.RET => ExecuteRet(),
            Inst.PROC => null, // already pre-processed

            Inst.COMMENT => null,

            _ => throw new Exception("INSTRUCTION NOT REGISTERED")
        };
    }

    private object ExecuteAdd()
    {
        if (IsRegister(Param2))
            Registers[Param1] += Registers[Param2];
        else
            Registers[Param1] += Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteSub()
    {
        if (IsRegister(Param2))
            Registers[Param1] -= Registers[Param2];
        else
            Registers[Param1] -= Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteMul()
    {
        if (IsRegister(Param2))
            Registers[Param1] *= Registers[Param2];
        else
            Registers[Param1] *= Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteDiv()
    {
        if (IsRegister(Param2))
            Registers[Param1] /= Registers[Param2];
        else
            Registers[Param1] /= Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteInc()
    {
        Registers[Param1]++;
        return new object();
    }

    private object ExecuteDec()
    {
        Registers[Param1]--;
        return new object();
    }

    private object ExecuteAnd()
    {
        if (IsRegister(Param2))
            Registers[Param1] &= Registers[Param2];
        else
            Registers[Param1] &= Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecuteOr()
    {
        if (IsRegister(Param2))
            Registers[Param1] |= Registers[Param2];
        else
            Registers[Param1] |= Convert.ToInt32(Param2);
        return new object();
    }

    private object ExecuteXor()
    {
        if (IsRegister(Param2))
            Registers[Param1] ^= Registers[Param2];
        else
            Registers[Param1] ^= Convert.ToInt32(Param2); return new object();
    }

    private object ExecuteMov()
    {
        if (IsRegister(Param2))
            Registers[Param1] = Registers[Param2];
        else
            Registers[Param1] = Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecutePrt()
    {
        if (IsRegister(Param1))
            Console.WriteLine($"{Param1.ToLower()}: {Registers[Param1]}\n");
        else
            Console.WriteLine($" {Param1}");

        return new object();
    }

    private object ExecuteOut()
    {
        Console.WriteLine($"{Param1}: {Convert.ToString(Registers[Param1], 2).PadLeft(32, '0')}");
        return new object();
    }

    private object ExecuteWt()
    {
        Thread.Sleep(Convert.ToInt32(Param1) * 1000);
        return new object();
    }

    private object ExecuteRet()
    {
        if (CallStack.Count > 0)
            Pointer = CallStack.Pop();

        return new object();
    }

    private object ExecuteJmp()
    {
        if (Processes.TryGetValue(Param1, out var index))
        {
            CallStack.Push(Pointer);
            Pointer = index - 1;
        }

        return new object();
    }

    private object ExecuteJnz()
    {
        if (IsRegister(Param2))
        {
            if (Registers[Param2] != 0)
                ExecuteJmp();
        }
        else
        {
            if (Convert.ToInt32(Param2) != 0)
                ExecuteJmp();
        }
            

        return new object();
    }

    private bool IsRegister(string key)
        => Registers.ContainsKey(key);

    private bool IsEOFInstruction()
        => Instructions[Pointer].Type == Inst.EOF;
}