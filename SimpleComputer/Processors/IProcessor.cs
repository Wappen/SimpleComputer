using System.Collections.Generic;

namespace SimpleComputer.Processors
{
    public interface IProcessor
    {
        List<string> Output { get; }
        int[] Memory { get; set; }
        Instruction[] Program { get; set; }
        int ProgramCounter { get; set; }

        void Run();
    }
}