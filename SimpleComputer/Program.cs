using SimpleComputer.ComplexComputer;
using System;
using System.IO;
using System.Threading;
using static SimpleComputer.ProgramUtils;

namespace SimpleComputer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SimpleComputer";

            IProcessor processor = new ComplexProcessor();
            string path;

            if (args.Length == 1)
            {
                path = args[0];
            }
            else if (args.Length == 0)
            {
                Console.Write("Program path: ");
                string input = Console.ReadLine();

                if (File.Exists(input))
                    path = input;
                else if (File.Exists($@".\Programs\{input}"))
                    path = $@".\Programs\{input}";
                else if (File.Exists(GetFileByName(input)))
                    path = GetFileByName(input);
                else if (File.Exists(GetFileByName($@".\Programs\{input}")))
                    path = GetFileByName($@".\Programs\{input}");
                else
                    throw new FileNotFoundException();
            }
            else
            {
                throw new ArgumentException("Wrong number of program arguments given.");
            }

            Console.WriteLine($"Reading file '{Path.GetRelativePath(Directory.GetCurrentDirectory(), path)}'...");
            LoadProgram(File.ReadAllText(path), processor);

            Console.WriteLine("Running program...");
            processor.Run();
            Console.WriteLine("Program exited.");
            Thread.Sleep(1000);
        }

        private static string GetFileByName(string name)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{name}.*");

            if (files.Length >= 1)
                return files[0];
            return "";
        }
    }
}
