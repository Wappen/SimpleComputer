using SimpleComputer.SimpleComputer;
using System;
using System.Collections.Generic;

namespace SimpleComputer.ComplexComputer
{
    [Instruction(typeof(InInstruction))]
    [Instruction(typeof(OutInstruction))]
    [Instruction(typeof(SectionInstruction))]
    [Instruction(typeof(JumpSectionInstruction))]
    public class ComplexProcessor : SimpleProcessor
    {
        public Dictionary<int, int> Sections { get; set; } = new Dictionary<int, int>();
    }
}
