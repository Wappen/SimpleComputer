namespace SimpleComputer.SimpleComputer
{
    [Name("+")]
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

    [Name("-")]
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

    [Name("0")]
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

    [Name("J")]
    class JumpInstruction : Instruction
    {
        public JumpInstruction(int parameter) : base(parameter) { }

        public override bool Execute()
        {
            processor.ProgramCounter = Parameter;
            return true;
        }
    }

    [Name("X")]
    class ExitInstruction : Instruction
    {
        public ExitInstruction() : base(0) { }

        public override bool Execute()
        {
            return false;
        }
    }
}
