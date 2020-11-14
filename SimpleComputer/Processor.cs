using SimpleComputer.Exceptions;
using System;
using System.Collections.Generic;

namespace SimpleComputer
{
    abstract class Processor : IProcessor
    {
        public abstract Dictionary<string, Type> Instructions { get; }

        public int[] Memory { get; set; }
        public Instruction[] Program { get; set; }
        public int ProgramCounter { get; set; }

        public void Run()
        {
            Init();

            // When not null, the program only halts when encountering an instruction of the same type
            Type haltInst = null;
            int skip = 0;

            bool run;
            do
            {
                // Skip halting and printing, when skip var is bigger than 1 or haltInst is not same as instruction
                if (skip-- <= 0 && (haltInst == null || haltInst == Program[ProgramCounter].GetType()))
                {
                    Console.Clear();
                    PrintProgram();
                    Console.WriteLine();
                    PrintMemory();

                    // Parse the input to either skip given number of lines or to set instType
                    string input = Console.ReadLine().ToUpper();
                    if (Instructions.ContainsKey(input))
                        haltInst = (haltInst == Instructions[input]) ? null : Instructions[input];
                    else
                        int.TryParse(input, out skip);
                }

                run = Clock();
            }
            while (run);
        }

        protected virtual void Init()
        {
            Console.Clear();
            Console.WindowHeight = Math.Max(Memory.Length + Program.Length + 10, Console.WindowHeight);

            for (int i = 0; i < Program.Length; i++)
            {
                Program[i].Init(this, i);
            }
        }

        protected virtual bool Clock()
        {
            try
            {
                bool result = Program[ProgramCounter].Execute();

                if (ProgramCounter >= Program.Length)
                    return false;

                return result;
            }
            catch (Exception)
            {
                throw new InstructionException($"Error while executing instruction '{Program[ProgramCounter].GetType().GetName()}' on line {ProgramCounter}.");
            }
        }

        public abstract void PrintProgram();

        public abstract void PrintMemory();
    }
}
