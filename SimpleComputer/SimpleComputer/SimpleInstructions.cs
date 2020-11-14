namespace SimpleComputer.SimpleComputer
{
    [Instruction("+")]
    class IncInstruction : Instruction
    {
        public IncInstruction(int parameter) : base(parameter) { }

        public override bool Execute()
        {
            processor.Memory[Parameter]++;
            processor.ProgramCounter++;
            return true;
        }
    }

    [Instruction("-")]
    class DecInstruction : Instruction
    {
        public DecInstruction(int parameter) : base(parameter) { }

        public override bool Execute()
        {
            processor.Memory[Parameter]--;
            processor.ProgramCounter++;
            return true;
        }
    }

    [Instruction("0")]
    class TestInstruction : Instruction
    {
        public TestInstruction(int parameter) : base(parameter) { }

        public override bool Execute()
        {
            bool isZero = processor.Memory[Parameter] == 0;
            processor.ProgramCounter += isZero ? 2 : 1;
            return true;
        }
    }

    [Instruction("J")]
    class JumpInstruction : Instruction
    {
        public JumpInstruction(int parameter) : base(parameter) { }

        public override bool Execute()
        {
            processor.ProgramCounter = Parameter;
            return true;
        }
    }

    [Instruction("X")]
    class ExitInstruction : Instruction
    {
        public ExitInstruction() : base(0) { }

        public override bool Execute()
        {
            return false;
        }
    }
}
