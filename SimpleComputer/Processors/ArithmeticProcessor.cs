using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleComputer.Processors
{
    [Instruction(typeof(LoadInstruction))]
    [Instruction(typeof(StoreInstruction))]
    [Instruction(typeof(AddInstruction))]
    [Instruction(typeof(SubInstruction))]
    [Instruction(typeof(MulInstruction))]
    [Instruction(typeof(DivInstruction))]
    class ArithmeticProcessor : ComplexProcessor
    {
        public int Accumulator { get; set; }

        [Name("LOAD")]
        public class LoadInstruction : Instruction
        {
            public LoadInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                if (processor is ArithmeticProcessor)
                {
                    var arProcessor = (ArithmeticProcessor)processor;

                    arProcessor.Accumulator = arProcessor.Memory[Parameter];
                }

                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("STORE")]
        public class StoreInstruction : Instruction
        {
            public StoreInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                if (processor is ArithmeticProcessor)
                {
                    var arProcessor = (ArithmeticProcessor)processor;

                    arProcessor.Memory[Parameter] = arProcessor.Accumulator;
                }

                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("ADD")]
        public class AddInstruction : Instruction
        {
            public AddInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                if (processor is ArithmeticProcessor)
                {
                    var arProcessor = (ArithmeticProcessor)processor;

                    arProcessor.Accumulator += arProcessor.Memory[Parameter];
                }

                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("SUB")]
        public class SubInstruction : Instruction
        {
            public SubInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                if (processor is ArithmeticProcessor)
                {
                    var arProcessor = (ArithmeticProcessor)processor;

                    arProcessor.Accumulator -= arProcessor.Memory[Parameter];
                }

                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("MUL")]
        public class MulInstruction : Instruction
        {
            public MulInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                if (processor is ArithmeticProcessor)
                {
                    var arProcessor = (ArithmeticProcessor)processor;

                    arProcessor.Accumulator *= arProcessor.Memory[Parameter];
                }

                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("DIV")]
        public class DivInstruction : Instruction
        {
            public DivInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                if (processor is ArithmeticProcessor)
                {
                    var arProcessor = (ArithmeticProcessor)processor;

                    arProcessor.Accumulator /= arProcessor.Memory[Parameter];
                }

                processor.ProgramCounter++;
                return true;
            }
        }

        protected override void PrintMemory()
        {
            base.PrintMemory();
            Console.WriteLine($"ACC: {Accumulator}");
        }
    }
}
