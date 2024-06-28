### Documentation
    
    // adds reg1 with reg2 and stores the result in reg1
    ADD, // add <reg1> <reg2>

    // subtracts reg2 from reg1 and stores in reg1
    SUB, // sub <reg1> <reg2>

    // multiplies reg2 with reg1 and stores in reg1
    MUL, // mul <reg1> <reg2>

    // divides reg1 by reg2 and stores in reg1
    DIV, // div <reg1> <reg2>

    // increments the value of reg1
    INC, // inc <reg1>

    // decrements the value of reg1
    DEC, // dec <reg1>

    // performs logical and on reg1 and reg2
    AND, // and <reg1> <reg2>

    // performs logical or on reg1 and reg2
    OR, // or <reg1> <reg2>

    // performs logical xor on reg1 and reg2
    XOR, // xor <reg1> <reg2>

    // performs logical not on reg1
    NOT, // not <reg1>

    // performs logical nand on reg1 and reg2
    NAND, // nand <reg1> <reg2>

    // performs logical nor on reg1 and reg2
    NOR,  // nor <reg1> <reg2>

    // performs logical xnor on reg1 and reg2
    XNOR, // xnor <reg1> <reg2>

    // performs negation on reg1
    NEG, // neg <reg1>

    // peforms a left shift on reg1 for amount
    SHL, // shl <reg1> <amount>

    // performs a right shift on reg2 for amount
    SHR, // shr <reg1> <amount>

    // does nothing, next instruction
    NOP, // nop

    // checks reg1 against reg2, sets zero flag to result
    CMP, // cmp <reg1> <reg2>

    // copies content of reg2 to reg1
    MOV, // mov <reg1> <reg2>

    // outputs the integer value of reg1
    PRT, // prt <reg1>

    // outputs the binary value of reg1
    OUT, // out <reg1>

    // defines a process 
    PROC, // <name>:

    // jumps to a process
    JMP, // jmp <name>

    // jumps to a process if zero flag is not zero
    JNZ, // jnz <name>

    // jumps to a process if zero flag is zero
    JZ, // jz <name>

    // jumps to a process if sign flag is positive
    JNS, // jns <name>

    // jumps to a process if sign flag is negative
    JS, // js <name>

    // exits the process
    RET, // ret

    // "sleeps" the program for sec
    WT, // wt <sec>

    // ignores everything after the ';'
    COMMENT, // ; this is a comment

    // inserted at the end of every program
    EOF,

    // will cause the program to throw an exception
    BAD,
