using System;

namespace SimpleComputer.Exceptions
{
    class ProgramException : Exception
    {
        public ProgramException(string message) : base(message)
        {
        }
    }
}
