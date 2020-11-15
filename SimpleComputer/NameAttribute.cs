using System;

namespace SimpleComputer
{
    [AttributeUsage(AttributeTargets.Class)]
    class NameAttribute : Attribute
    {
        public string Name { get; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
