namespace SimpleComputer.SimpleComputer
{
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
