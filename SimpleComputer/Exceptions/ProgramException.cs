using System;

namespace SimpleComputer.Exceptions
{
    /// <summary>
    /// Thrown when a program is in invalid format, e.g.: Program and Memory sections not seperated.
    /// </summary>
    class ProgramException : Exception
    {
        public ProgramException(string message) : base(message)
        {
        }
    }
}
