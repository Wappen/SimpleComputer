using System;
using System.Collections.Generic;

namespace SimpleComputer.SimpleComputer
{
    class SimpleProcessor : Processor
    {
        public override Dictionary<string, Type> Instructions => new Dictionary<string, Type>
        {
            { "+", typeof(IncInstruction) },
            { "-", typeof(DecInstruction) },
            { "0", typeof(TestInstruction) },
            { "J", typeof(JumpInstruction) },
            { "X", typeof(ExitInstruction) }
        };

        public override void PrintProgram()
        {
            for (int i = 0; i < Program.Length; i++)
            {
                Console.Write($"{i}");
                Console.CursorLeft = 3;
                char lc = (ProgramCounter == i) ? '>' : ' ';
                char rc = (ProgramCounter == i) ? '<' : ' ';
                Console.Write($"{lc} | {Program[i]} {rc}");
                Console.WriteLine();
            }
        }

        public override void PrintMemory()
        {
            for (int i = 0; i < Memory.Length; i++)
            {
                Console.Write($"{i}");
                Console.CursorLeft = 5;
                Console.Write($"| {Memory[i]}");
                Console.WriteLine();
            }
        }
    }
}
