using System;

namespace SimpleComputer.Exceptions
{
    /// <summary>
    /// Thrown when an instruction is unknown.
    /// </summary>
    class UnknownInstructionException : Exception
    {
        public UnknownInstructionException(string message) : base(message)
        {
        }

        public UnknownInstructionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
