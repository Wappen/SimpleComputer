using System;

namespace SimpleComputer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class InstructionAttribute : Attribute
    {
        public string Name { get; }
        public Type InstructionType { get; }

        public InstructionAttribute(Type instructionType)
        {
            InstructionType = instructionType;
            Name = instructionType.GetName();
        }
    }
}
