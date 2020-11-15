using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleComputer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class InstructionAttribute : Attribute
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
