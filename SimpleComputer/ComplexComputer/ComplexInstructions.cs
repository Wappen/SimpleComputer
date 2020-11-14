﻿using System;

namespace SimpleComputer.ComplexComputer
{
    [Instruction("IN")]
    class InInstruction : Instruction
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

    [Instruction("OUT")]
    class OutInstruction : Instruction
    {
        public OutInstruction(int parameter) : base(parameter) { }

        public override bool Execute()
        {
            Console.WriteLine($"O: {processor.Memory[Parameter]}");
            processor.ProgramCounter++;
            return true;
        }
    }

    [Instruction("SEC")]
    class SectionInstruction : Instruction
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

    [Instruction("JS")]
    class JumpSectionInstruction : Instruction
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