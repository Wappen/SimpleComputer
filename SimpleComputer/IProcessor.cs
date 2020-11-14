using System;
using System.Collections.Generic;

namespace SimpleComputer
{
    interface IProcessor
    {
        Dictionary<string, Type> Instructions { get; }
        int[] Memory { get; set; }
        Instruction[] Program { get; set; }
        int ProgramCounter { get; set; }
        void Run();
    }
}
