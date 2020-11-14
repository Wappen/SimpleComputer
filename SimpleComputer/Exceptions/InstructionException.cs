using System;

namespace SimpleComputer.Exceptions
{
    class InstructionException : Exception
    {
        public InstructionException(string message) : base(message)
        {
        }
    }
}
