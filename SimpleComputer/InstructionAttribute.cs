using System;

namespace SimpleComputer
{
    class InstructionAttribute : Attribute
    {
        public string Name { get; }

        public InstructionAttribute(string name)
        {
            Name = name;
        }
    }

    static class TypeExtensions
    {
        public static string GetName(this Type type)
        {
            object[] attributes = type.GetCustomAttributes(true);

            foreach (object attribute in attributes)
            {
                InstructionAttribute instructionAttribute = attribute as InstructionAttribute;

                if (instructionAttribute != null)
                    return instructionAttribute.Name;
            }

            return "";
        }
    }
}
