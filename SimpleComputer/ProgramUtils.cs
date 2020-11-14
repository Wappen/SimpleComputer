using SimpleComputer.Exceptions;
using System;
using System.Collections.Generic;

namespace SimpleComputer
{
    static class ProgramUtils
    {
        public const string PartSeperator = "#&";

        public static void LoadProgram(string text, IProcessor processor)
        {
            string[] parts = text.Split(PartSeperator);

            if (parts.Length != 2)
                throw new ProgramException($"Memory part not seperated from program part. Make sure there is only one '{PartSeperator}' seperator existent in your program.");

            string[] proLines = PrepLines(parts[0].Split('\n'));
            string[] memLines = PrepLines(parts[1].Split('\n'));
            Console.WriteLine("Parsing program part...");
            Instruction[] program = ParseProgram(proLines, processor.Instructions);
            Console.WriteLine("Parsing memory part...");
            int[] memory = ParseMemory(memLines);

            processor.Program = program;
            processor.Memory = memory;
        }

        private static string[] PrepLines(string[] lines)
        {
            List<string> preppedLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();

                if (String.IsNullOrEmpty(lines[i])
                    || String.IsNullOrWhiteSpace(lines[i])
                    || lines[i].StartsWith('#'))
                    continue;

                preppedLines.Add(lines[i]);
            }

            return preppedLines.ToArray();
        }

        public static Instruction[] ParseProgram(string[] lines, Dictionary<string, Type> instructionTypes)
        {
            var instructions = new Instruction[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    string[] parts = lines[i].Split(' ');
                    Type instType = instructionTypes[parts[0]];

                    if (parts.Length == 2)
                    {
                        int parameter = int.Parse(parts[1]);
                        instructions[i] = (Instruction)Activator.CreateInstance(instType, parameter);
                    }
                    else
                    {
                        instructions[i] = (Instruction)Activator.CreateInstance(instType);
                    }
                }
                catch (KeyNotFoundException)
                {
                    throw new UnknownInstructionException($"Unknown Instruction '{lines[i]}' on line {i}.");
                }
                catch (MissingMethodException)
                {
                    throw new ArgumentOutOfRangeException($"Wrong number of arguments given at '{lines[i]}' on line {i}.");
                }
                catch (FormatException)
                {
                    throw new ArgumentException($"Given argument was not a number at '{lines[i]}' on line {i}.");
                }
            }

            return instructions;
        }

        public static int[] ParseMemory(string[] lines)
        {
            int[] memory = new int[lines.Length];

            for (int i = 0; i < memory.Length; i++)
            {
                try
                {
                    memory[i] = int.Parse(lines[i]);
                }
                catch (FormatException)
                {
                    throw new FormatException($"Memory cell was not a number at '{lines[i]}' on line {i}.");
                }
            }

            return memory;
        }
    }
}
