### Installation
Clone latest version of Stacklet from the repository and run it in your terminal.

### Syntax
Stacklet commands are typically written as `command arg1`, with each command operating on a stack. Commands and arguments are separated by spaces.




### Arithmetic

**ADD**
- **Description**: Adds the top two elements of the stack.
- **Syntax**: `add`
- **Example**:
  ```
  push 3
  push 5
  add    // Stack now contains 8
  ```

**SUB**
- **Description**: Subtracts the top stack element from the second-to-top element.
- **Syntax**: `SUB`
- **Example**:
  ```
  push 10
  push 4
  sub    // Stack now contains 6
  ```
