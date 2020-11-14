using SimpleComputer.SimpleComputer;
using System;
using System.Collections.Generic;

namespace SimpleComputer.ComplexComputer
{
    class ComplexProcessor : Processor
    {
        public Dictionary<int, int> Sections { get; set; } = new Dictionary<int, int>();
        public override Dictionary<string, Type> Instructions => new Dictionary<string, Type>
        {
            { typeof(IncInstruction).GetName(), typeof(IncInstruction) },
            { typeof(DecInstruction).GetName(), typeof(DecInstruction) },
            { typeof(TestInstruction).GetName(), typeof(TestInstruction) },
            { typeof(JumpInstruction).GetName(), typeof(JumpInstruction) },
            { typeof(ExitInstruction).GetName(), typeof(ExitInstruction) },
            { typeof(InInstruction).GetName(), typeof(InInstruction) },
            { typeof(OutInstruction).GetName(), typeof(OutInstruction) },
            { typeof(SectionInstruction).GetName(), typeof(SectionInstruction) },
            { typeof(JumpSectionInstruction).GetName(), typeof(JumpSectionInstruction) }
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
