# SimpleComputer
With this console program you can simulate a very simple processor with a small instruction set.
The project is just a personal learning project for which I got the idea during school.
You can execute code files written in an assembly like language using the program and observe the flow step by step.
## How To Use
When starting the program normally you are prompted with the message "Program path:" into which you should enter the path to a code file.
The program then loads the code file and displays everything in the console window. The program code is shown with a pointer pointing at the line
that is being executed next.

Press <kbd>Enter</kbd> to move one time step forward. You can also type in a number to skip the few next steps.
It is also possible to input the name of an instruction. The program will then execute the code until it encounters an instruction of the same type.

At the bottom you can see the memory with it's values and the programs input/output.
## The Language
The code is written in an assembly like language. Each line begins with the instruction code, followed by a parameter.
The code file also includes a memory section. The memory section defines the size of available memory and also sets default values.

### Addition example
Here is an example of a program that adds cells 0 and 1 with the values 10 and 6 together:
```
J 3
+ 0
- 1
0 1
J 1
X

#&

10
6
```
The program uses 5 different instructions.
- `J <val>` - Sets the program counter to the parameter's value. Basically jumps to the given line.
- `+ <val>` - Increments the memory cell at the given address.
- `- <val>` - Decrements the memory cell at the given address.
- `0 <val>` - Tests if the memory cell at the specified address is zero. If it is it skips the next line.
- `X` - Does not use a parameter; exits the program.

The `#&` part describes the line from which the memory section begins. Each new line in the memory section translates to a new memory cell.

### Multiplication example
Here is an example of a more complex program which you can use to multiply two values with each other.
It includes comments and uses some more instructions which are described below.
```
IN 0
IN 1

# Exit if 1 is Zero
0 1
JS 10
JS 100

# Beginning of loop 1: Dec 0
SEC 10
0 0
JS 11
JS 100
SEC 11
- 0

# Move 1 to 3 while inc 2
SEC 12
- 1
+ 2
+ 3
0 1
JS 12
JS 20

# Beginning of loop 2: Dec 0
SEC 20
0 0
JS 21
JS 100
SEC 21
- 0

# Move 3 to 1 while inc 2
SEC 22
- 3
+ 2
+ 1
0 3
JS 22
JS 10


# Output
SEC 100
OUT 2
X

#&

0
0
0
0
```
The multiplication program uses 4 more instructions.
- `SEC <val>` - Defines a section in the program to which you can jump using the jump section instruction.
- `JS <val>` - Jumps to the section with the given id.
- `IN <val>` - Gets user input and stores it in the memory cell at the given address.
- `OUT <val>` - Outputs the value of the memory cell at the given address.

The `SEC` and `JS` instructions are kinda a replacement for the `J` instruction because they offer more flexibility when programming, as you can
easily add more lines in between without breaking the program.
