using System;
using System.Collections.Generic;

namespace SimpleComputer.SimpleComputer
{
    [Instruction(typeof(IncInstruction))]
    [Instruction(typeof(DecInstruction))]
    [Instruction(typeof(TestInstruction))]
    [Instruction(typeof(JumpInstruction))]
    [Instruction(typeof(ExitInstruction))]
    public class SimpleProcessor : Processor
    {
        protected override void PrintProgram()
        {
            for (int i = 0; i < Program.Length; i++)
            {
                Console.Write($"{i}");
                Console.CursorLeft = 4;
                char lc = (ProgramCounter == i) ? '>' : '|';
                char rc = (ProgramCounter == i) ? '<' : ' ';
                Console.Write($"{lc} {Program[i]} {rc}");
                Console.WriteLine();
            }
        }

        protected override void PrintMemory()
        {
            for (int i = 0; i < Memory.Length; i++)
            {
                Console.Write($"{i}");
                Console.CursorLeft = 4;
                Console.Write($"| {Memory[i]}");
                Console.WriteLine();
            }
        }

        protected override void PrintOutput()
        {
            foreach (var line in Output)
            {
                Console.WriteLine(line);
            }
        }
    }
}
