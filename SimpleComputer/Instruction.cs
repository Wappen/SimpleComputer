using SimpleComputer.Processors;

namespace SimpleComputer
{
    public abstract class Instruction
    {
        public readonly int Parameter;

        protected IProcessor processor;

        public Instruction(int parameter)
        {
            Parameter = parameter;
        }

        public virtual void Init(IProcessor processor, int line)
        {
            this.processor = processor;
        }

        public abstract bool Execute();

        public override string ToString()
        {
            return $"{GetType().GetName()} {Parameter}";
        }
    }
}
