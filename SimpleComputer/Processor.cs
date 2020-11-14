﻿using SimpleComputer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleComputer
{
    abstract class Processor : IProcessor
    {
        public abstract Dictionary<string, Type> Instructions { get; }

        public List<string> Output { get; } = new List<string>();
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
                    Display();
                    
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

            Display();
        }

        private void Display()
        {
            Console.WindowHeight = Math.Max(Memory.Length + Program.Length + Output.Count + 12, Console.WindowHeight);
            Console.Clear();
            Console.WriteLine("Code:");
            PrintProgram();
            Console.WriteLine();
            Console.WriteLine("Memory:");
            PrintMemory();
            Console.WriteLine();
            Console.WriteLine("Output:");
            PrintOutput();
            Console.WriteLine();
            Console.Write("Input: ");
        }

        protected virtual void Init()
        {
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
