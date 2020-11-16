using SimpleComputer.Exceptions;
using System;
using System.Collections.Generic;

namespace SimpleComputer.Processors
{
    public abstract class Processor : IProcessor
    {
        public List<string> Output { get; } = new List<string>();
        public int[] Memory { get; set; }
        public Instruction[] Program { get; set; }
        public int ProgramCounter { get; set; }

        private Dictionary<string, Type> _instTypes;

        public void Run()
        {
            Init();

            // When this var is not null, the program only halts when encountering an instruction of the same type
            Type haltInst = null;
            int skip = 0;

            bool run;
            do
            {
                // Skip printing and input, when skip is bigger than 1 or haltInst is not same as instruction
                if (skip-- <= 0 && (haltInst == null || haltInst == Program[ProgramCounter].GetType()))
                {
                    Display();

                    // Parse the input to an instruction type or set skip value
                    string input = Console.ReadLine().ToUpper();

                    if (_instTypes.ContainsKey(input))
                        haltInst = (haltInst == _instTypes[input]) ? null : _instTypes[input];
                    else
                        int.TryParse(input, out skip);
                }

                run = Clock();
            }
            while (run);

            Display();
        }

        private void Display()
        {
            Console.WindowHeight = Math.Max(Memory.Length + Program.Length + Output.Count + 12, Console.WindowHeight);
            Console.Clear();

            Console.WriteLine("Code:");
            PrintProgram();

            Console.WriteLine("\nMemory:");
            PrintMemory();

            Console.WriteLine("\nOutput:");
            PrintOutput();

            Console.Write("\nInput: ");
        }

        protected virtual void Init()
        {
            _instTypes = this.GetInstructionMap();

            for (int i = 0; i < Program.Length; i++)
            {
                Program[i].Init(this, i);
            }
        }

        /// <summary>
        /// Executes a clock cycle by executing the current instruction.
        /// </summary>
        /// <returns>Returns a boolean indicating whether the program should continue or not.</returns>
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

        protected abstract void PrintProgram();

        protected abstract void PrintMemory();

        protected abstract void PrintOutput();
    }
}
