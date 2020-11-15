using System;
using System.Collections.Generic;

namespace SimpleComputer.Processors
{
    [Instruction(typeof(InInstruction))]
    [Instruction(typeof(OutInstruction))]
    [Instruction(typeof(SectionInstruction))]
    [Instruction(typeof(JumpSectionInstruction))]
    public class ComplexProcessor : SimpleProcessor
    {
        public Dictionary<int, int> Sections { get; set; } = new Dictionary<int, int>();

        [Name("IN")]
        public class InInstruction : Instruction
        {
            public InInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                Console.Write("I: ");
                string input = Console.ReadLine();

                int val;
                if (int.TryParse(input, out val))
                {
                    processor.Memory[Parameter] = val;
                }

                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("OUT")]
        public class OutInstruction : Instruction
        {
            public OutInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                processor.Output.Add($"O: {processor.Memory[Parameter]}");
                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("SEC")]
        public class SectionInstruction : Instruction
        {
            public SectionInstruction(int parameter) : base(parameter) { }

            public override void Init(IProcessor processor, int line)
            {
                if (processor is ComplexProcessor)
                {
                    var complexProcessor = (ComplexProcessor)processor;
                    complexProcessor.Sections.Add(Parameter, line);
                }

                base.Init(processor, line);
            }

            public override bool Execute()
            {
                processor.ProgramCounter++;
                return true;
            }
        }

        [Name("JS")]
        public class JumpSectionInstruction : Instruction
        {
            public JumpSectionInstruction(int parameter) : base(parameter) { }

            public override bool Execute()
            {
                if (processor is ComplexProcessor)
                {
                    var complexProcessor = (ComplexProcessor)processor;
                    processor.ProgramCounter = complexProcessor.Sections[Parameter];
                }
                else
                {
                    processor.ProgramCounter++;
                }
                return true;
            }
        }
    }
}
