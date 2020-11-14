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

        protected override void PrintProgram()
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

        protected override void PrintMemory()
        {
            for (int i = 0; i < Memory.Length; i++)
            {
                Console.Write($"{i}");
                Console.CursorLeft = 5;
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
