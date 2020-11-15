using SimpleComputer.Exceptions;
using SimpleComputer.Processors;
using System;
using System.Collections.Generic;

namespace SimpleComputer
{
    static class Utils
    {
        public const string PartSeperator = "#&";

        /// <summary>
        /// Loads the raw program text into the given processor.
        /// </summary>
        /// <param name="text">The program code without any changes.</param>
        /// <param name="processor">The processor into which the program shall be loaded.</param>
        public static void LoadProgram(string text, IProcessor processor)
        {
            string[] parts = text.Split(PartSeperator);

            if (parts.Length != 2)
                throw new ProgramException($"Memory part not seperated from program part. Make sure there is only one '{PartSeperator}' seperator existent in your program.");

            string[] proLines = CleanUp(parts[0].Split('\n'));
            string[] memLines = CleanUp(parts[1].Split('\n'));
            Console.WriteLine("Parsing program part...");
            Instruction[] program = ParseProgram(proLines, processor);
            Console.WriteLine("Parsing memory part...");
            int[] memory = ParseMemory(memLines);

            processor.Program = program;
            processor.Memory = memory;
        }

        /// <summary>
        /// Prepares and cleans the given program lines for parsing.
        /// </summary>
        /// <param name="lines">The raw program/memory lines splitted on new line</param>
        /// <returns>Returns program/memory lines without comments and unnecessary white spaces.</returns>
        private static string[] CleanUp(string[] lines)
        {
            List<string> cleanLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();

                if (String.IsNullOrEmpty(lines[i])
                    || String.IsNullOrWhiteSpace(lines[i])
                    || lines[i].StartsWith('#'))
                    continue;

                cleanLines.Add(lines[i]);
            }

            return cleanLines.ToArray();
        }

        /// <summary>
        /// Parses the given program lines to an array of instructions.
        /// </summary>
        /// <param name="lines">Prepared program code lines.</param>
        /// <param name="processor">Processor for which the program shall be parsed.</param>
        /// <returns>Returns a list of instructions ready to be executed.</returns>
        private static Instruction[] ParseProgram(string[] lines, IProcessor processor)
        {
            Dictionary<string, Type> instTypes = processor.GetInstructionMap();
            var instructions = new Instruction[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    string[] parts = lines[i].Split(' ');
                    Type instType = instTypes[parts[0]];

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

        /// <summary>
        /// Parses the given memory lines to an array of memory cells with values.
        /// </summary>
        /// <param name="lines">The memory values and cells.</param>
        /// <returns>Returns an array of memory values.</returns>
        private static int[] ParseMemory(string[] lines)
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

        /// <summary>
        /// Returns a map of instruction types the processor accepts.
        /// </summary>
        /// <param name="processor">The specific processor.</param>
        /// <returns>Returns a Dictionary in which the keys are the instruction names and the values are the instruction types.</returns>
        public static Dictionary<string, Type> GetInstructionMap(this IProcessor processor)
        {
            var instTypes = new Dictionary<string, Type>();

            object[] attributes = processor.GetType().GetCustomAttributes(true);

            foreach (object attribute in attributes)
            {
                InstructionAttribute instAttribute = attribute as InstructionAttribute;

                if (instAttribute != null)
                    instTypes.Add(instAttribute.Name, instAttribute.InstructionType);
            }

            return instTypes;
        }

        public static string GetName(this Type type)
        {
            object[] attributes = type.GetCustomAttributes(true);

            foreach (object attribute in attributes)
            {
                NameAttribute nameAttribute = attribute as NameAttribute;

                if (nameAttribute != null)
                    return nameAttribute.Name;
            }

            return "";
        }
    }
}
