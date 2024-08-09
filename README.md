### Installation
Clone latest version of Stacklet from the repository and run it in your terminal.

### Syntax
Stacklet commands are typically written as `command arg1`, with each command operating on a stack. Commands and arguments are separated by spaces.




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
