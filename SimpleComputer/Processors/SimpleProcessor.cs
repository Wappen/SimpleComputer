using System;
using System.Collections.Generic;

namespace SimpleComputer.Processors
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

        [Name("+")]
        public class IncInstruction : Instruction
        {
            public IncInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                processor.Memory[Parameter]++;
                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("-")]
        public class DecInstruction : Instruction
        {
            public DecInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                processor.Memory[Parameter]--;
                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("0")]
        public class TestInstruction : Instruction
        {
            public TestInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                bool isZero = processor.Memory[Parameter] == 0;
                processor.ProgramCounter += isZero ? 2 : 1;
                return true;
            }
        }

        [Name("J")]
        public class JumpInstruction : Instruction
        {
            public JumpInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                processor.ProgramCounter = Parameter;
                return true;
            }
        }

        [Name("X")]
        public class ExitInstruction : Instruction
        {
            public ExitInstruction() : base(0) { }

            public override bool Execute()
            {
                return false;
            }
        }
    }
}
