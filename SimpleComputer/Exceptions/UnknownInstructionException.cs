using System;

namespace SimpleComputer.Exceptions
{
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
