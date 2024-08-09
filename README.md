## Installation
Clone latest version of Stacklet from the repository.
```bash
git clone https://github.com/rxgq/stacklet.git
cd stacklet
```

<br>

Build the project using .NET
```bash
dotnet build
```

## Usage

To run a Stacklet program, use the following:

```bash
dotnet run -- <path_to_code>
```

For example:

```bash
dotnet run -- example/fib.txt
```




## Features

- Basic Stack Manipulation (push, drop, dupe, swap, etc)
- Arithmetic Operations(add, sub, mul, div, etc)
- Control Flow (goto, labels)
- I/O Operations (out, read)
- Conditional Statement Execution

## Example Program

Program that infinitely outputs the fibonacci sequence
```
  push 0
  push 1

  def fib
    wait 1
    out
    dupe
    spin
    add

  goto fib
```

<br>
<br>
<br>

## Documentation

### Arithmetic
<hr>

**ADD**
- **Description**: Adds the top two elements of the stack.
- **Syntax**: `add`

  ```
  push 10
  push 7
  add      // Stack now contains 17
  ```

<br>

**SUB**
- **Description**: Subtracts the top stack element from the second-to-top element.
- **Syntax**: `sub`
  
  ```
  push 20
  push 13
  sub      // Stack now contains 7
  ```

<br>

**MUL**
- **Description**: Multiplies the top two elements of the stack.
- **Syntax**: `mul`
  
  ```
  push 3
  push 21
  mul      // Stack now contains 63
  ```

<br>

**DIV**
- **Description**: Divides the top stack element by the second-to-top element.
- **Syntax**: `div`
  
  ```
  push 24
  push 6
  div      // Stack now contains 6
  ```

<br>

**MOD**
- **Description**: Divides the top stack element by the second-to-top element and returns the remainder.
- **Syntax**: `mod`
  
  ```
  push 11
  push 7
  mod      // Stack now contains 4
  ```

<br>

**NEG**
- **Description**: Reverses the sign of the top stack element.
- **Syntax**: `neg`
  
  ```
  push 5
  neg      // Stack now contains -5
  ```

<br>

**ABS**
- **Description**: Replaces the top element of the stack with its absolute value.
- **Syntax**: `abs`
  
  ```
  push -14
  abs      // Stack now contains 14
  ```


<br>
<br>
<br>

### Stack Operations
<hr>


**PUSH**
- **Description**: Pushes a value to the top of the stack.
- **Syntax**: `push`
  
  ```
  push 10
  out     // Stack now contains 10
  ```

<br>

**DROP**
- **Description**: Drops the value at the top of the stack.
- **Syntax**: `drop`
  
  ```
  push 5
  push 10

  drop

  out     // Stack now contains 5
  ```

<br>

**DUPE**
- **Description**: Duplicates the value at the top of the stack.
- **Syntax**: `dupe`
  
  ```
  push 5
  dupe

  out     // Stack now contains (5, 5)
  ```

<br>

**SWAP**
- **Description**: Swapes the value at the top of the stack with the second-to-top value.
- **Syntax**: `swap`
  
  ```
  push 4
  push 8
  swap

  out     // Stack now contains (8, 4)
  ```

<br>

**SIZE**
- **Description**: Gets the current stack size and pushes to the top of the stack.
- **Syntax**: `size`
  
  ```
  push 1
  push 2
  push 8

  size

  out     // Stack now contains 3
  ```

<br>


**SPIN**
- **Description**: Reverses the order of the stack elements
- **Syntax**: `spin`
  
  ```
  push 1
  push 2
  push 3

  spin

  out     // Stack now contains (3, 2, 1)
  ```

<br>

**FREE**
- **Description**: Clears the stack contents
- **Syntax**: `free`
  
  ```
  push 5
  push 3
  push 17

  free

  push 1
  out      // Stack now contains 1
  ```

<br>

<br>
<br>
<br>

### Misc
<hr>

**DUMP**
- **Description**: Dumps the contents of the stack into the output
- **Syntax**: `dump`
  
  ```
  push 1
  push 4
  push 2
  push 8

  dump          // STACK: 1, 4, 2, 8
  ```

<br>

**GOTO**
- **Description**: Attempts to update the instruction pointer to the location of the 
- **Syntax**: `goto arg1`
  
  ```
  def main
    out "Running main"
  
    goto main      // infinitely calls 'main'
  ```

<br>

**HALT**
- **Description**: Exits the program immediately 
- **Syntax**: `halt`
  
  ```
  push 10
  out 

  halt

  out      // will not get executed
  ```

<br>

**NOP**
- **Description**: No operation, does nothing
- **Syntax**: `nop`
  
  ```
  push 10
  nop      // does nothing

  drop

  push 5
  out
  ```

<br>

**OUT**
- **Description**: Prints the value at the top of the stack to the output or a string
- **Syntax**: `out`
  
  ```
  out "Hello World!"    // Hello World!

  push 10
  out      // 10
  ```

<br>

**READ**
- **Description**: reads an integer input from the user
- **Syntax**: `read`
  
  ```
  read     // input: 1
  out      // 1
  ```

<br>


**WAIT**
- **Description**: Reads in an integer and 'sleeps' the program for that time
- **Syntax**: `wait`
  
  ```
  dpush 10
  wait 1      // waits for 1 second

  out
  ```

<br>