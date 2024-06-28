namespace assembly.src;

internal class Executer
{
    private readonly Dictionary<string, int> Memory = new()
    {
        { "rax", 0 }, { "rbx", 0 },{ "rcx", 0 },{ "rdx", 0 },
    };

    private readonly Dictionary<string, int> Flags = new()
    {
        { "zf", 0 },{ "sf", -0 }
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
            Inst.ADD  =>  ExecuteAdd(),
            Inst.SUB  =>  ExecuteSub(),
            Inst.MUL  =>  ExecuteMul(),
            Inst.DIV  =>  ExecuteDiv(),
            Inst.AND  =>  ExecuteAnd(),
            Inst.OR   =>   ExecuteOr(),
            Inst.XOR  =>  ExecuteXor(),
            Inst.NOR  =>  ExecuteNor(),
            Inst.XNOR => ExecuteXnor(),
            Inst.NAND => ExecuteNand(),
            Inst.SHL  =>  ExecuteShl(),
            Inst.SHR  =>  ExecuteShr(),

            Inst.INC  =>  ExecuteInc(),
            Inst.DEC  =>  ExecuteDec(),
            Inst.NOT  =>  ExecuteNot(),
            Inst.NEG  =>  ExecuteNeg(),

            Inst.CMP  =>  ExecuteCmp(),
            Inst.JMP  =>  ExecuteJmp(),
            Inst.JNZ  =>  ExecuteJnz(),
            Inst.JZ   =>   ExecuteJz(),
            Inst.JNS  =>  ExecuteJns(),
            Inst.JS   =>   ExecuteJs(),
            Inst.RET  =>  ExecuteRet(),
            Inst.MOV  =>  ExecuteMov(),
            Inst.NOP  =>          null,

            Inst.PRT  =>  ExecutePrt(),
            Inst.OUT  =>  ExecuteOut(),
            Inst.WT   =>   ExecuteWt(),

            Inst.PROC =>          null,


            Inst.COMMENT => null,
            _ => throw new Exception("INSTRUCTION NOT IN MEMORY")
        };
    }

    private void SetSignFlag() 
    {
        if (Memory[Param1] < 0)
            Flags["sf"] = 1;
        else
            Flags["sf"] = 0;
    }

    private object ExecuteAdd()
    {
        if (IsMemory(Param2))
            Memory[Param1] += Memory[Param2];
        else
            Memory[Param1] += Convert.ToInt32(Param2);

        SetSignFlag();

        return new object();
    }

    private object ExecuteSub()
    {
        if (IsMemory(Param2))
            Memory[Param1] -= Memory[Param2];
        else
            Memory[Param1] -= Convert.ToInt32(Param2);

        SetSignFlag();

        return new object();
    }

    private object ExecuteMul()
    {
        if (IsMemory(Param2))
            Memory[Param1] *= Memory[Param2];
        else
            Memory[Param1] *= Convert.ToInt32(Param2);

        SetSignFlag();

        return new object();
    }

    private object ExecuteDiv()
    {
        if (IsMemory(Param2))
            Memory[Param1] /= Memory[Param2];
        else
            Memory[Param1] /= Convert.ToInt32(Param2);

        SetSignFlag();

        return new object();
    }

    private object ExecuteInc()
    {
        Memory[Param1]++;
        SetSignFlag();
        return new object();
    }

    private object ExecuteDec()
    {
        Memory[Param1]--;
        SetSignFlag();
        return new object();
    }

    private object ExecuteAnd()
    {
        if (IsMemory(Param2))
            Memory[Param1] &= Memory[Param2];
        else
            Memory[Param1] &= Convert.ToInt32(Param2);

        SetSignFlag();

        return new object();
    }

    private object ExecuteOr()
    {
        if (IsMemory(Param2))
            Memory[Param1] |= Memory[Param2];
        else
            Memory[Param1] |= Convert.ToInt32(Param2);

        SetSignFlag();

        return new object();
    }

    private object ExecuteXor()
    {
        if (IsMemory(Param2))
            Memory[Param1] ^= Memory[Param2];
        else
            Memory[Param1] ^= Convert.ToInt32(Param2);

        SetSignFlag();

        return new object();
    }

    private object ExecuteNand()
    {
        if (IsMemory(Param1) && IsMemory(Param2))
            Memory[Param1] = ~(Memory[Param1] & Memory[Param2]);

        SetSignFlag();

        return new object();
    }

    private object ExecuteNor()
    {
        if (IsMemory(Param1) && IsMemory(Param2))
            Memory[Param1] = ~(Memory[Param1] | Memory[Param2]);

        SetSignFlag();

        return new object();
    }

    private object ExecuteXnor()
    {
        if (IsMemory(Param1) && IsMemory(Param2))
            Memory[Param1] = ~(Memory[Param1] ^ Memory[Param2]);

        SetSignFlag();

        return new object();
    }

    private object ExecuteCmp() 
    {
        Flags["zf"] = Memory[Param1] == Memory[Param2] ? 1 : 0;
        return new object();
    }

    private object ExecuteNot() 
    {
        Memory[Param1] = ~Memory[Param1];
        SetSignFlag();

        return new object();
    }

    private object ExecuteNeg()
    {
        Memory[Param1] = -Memory[Param1];
        SetSignFlag();

        return new object();
    }

    private object ExecuteShl()
    {
        Memory[Param1] = Memory[Param1] << Convert.ToInt32(Param2);
        SetSignFlag();
        
        return new object();
    }

    private object ExecuteShr()
    {
        Memory[Param1] = Memory[Param1] >> Convert.ToInt32(Param2);
        SetSignFlag();

        return new object();
    }

    private object ExecuteMov()
    {
        if (IsMemory(Param2))
            Memory[Param1] = Memory[Param2];
        else
            Memory[Param1] = Convert.ToInt32(Param2);

        return new object();
    }

    private object ExecutePrt()
    {
        Console.Write(IsMemory(Param1) 
            ? $"{Param1.ToLower()}: {Memory[Param1]}\n" 
            : $"{Param1}\n");

        return new object();
    }

    private object ExecuteOut()
    {
        Console.WriteLine(IsMemory(Param1)
            ? $"{Param1}: {Convert.ToString(Memory[Param1], 2).PadLeft(32, '0')}"
            : Convert.ToString(int.Parse(Param1), 2).PadLeft(32, '0'));

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
        if (Flags["zf"] != 0)
            ExecuteJmp();
            
        return new object();
    }

    private object ExecuteJz() 
    {
        if (Flags["zf"] == 0)
            ExecuteJmp();

        return new object();
    }

    private object ExecuteJns()
    {
        if (Flags["sf"] == 0)
            ExecuteJmp();

        return new object();
    }

    private object ExecuteJs()
    {
        if (Flags["sf"] != 0)
            ExecuteJmp();

        return new object();
    }

    private bool IsMemory(string key)
        => Memory.ContainsKey(key);

    private bool IsEOFInstruction()
        => Instructions[Pointer].Type == Inst.EOF;
}