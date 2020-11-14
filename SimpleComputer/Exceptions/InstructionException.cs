using System;

namespace SimpleComputer.Exceptions
{
    /// <summary>
    /// Thrown when an error happenes during execution of an instruction.
    /// </summary>
    class InstructionException : Exception
    {
        public InstructionException(string message) : base(message)
        {
        }
    }
}
