using SimpleComputer.Processors;
using System;
using System.Collections.Generic;
using System.IO;
using static SimpleComputer.Utils;

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
                // Retrieve a list of files in working dir and program folder.
                List<string> files = new List<string>(Directory.GetFiles(@".\", "*.txt"));
                files.AddRange(Directory.GetFiles(@".\Programs\", "*.txt"));

                // List of file names without extension and print them to console.
                List<string> fileNames = new List<string>();
                files.ForEach(f =>
                {
                    string name = Path.GetFileNameWithoutExtension(f);
                    fileNames.Add(name);
                    Console.WriteLine(name);
                });

                // Get program name with autocomplete enabled.
                ReadLine.AutoCompletionHandler = new ProgramAutoCompletionHandler(fileNames.ToArray());
                string input = ReadLine.Read("\nProgram path: ");

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

                ReadLine.AutoCompletionHandler = default;
            }
            else
            {
                throw new ArgumentException("Wrong number of program arguments given.");
            }

            Console.WriteLine($"Reading file '{Path.GetRelativePath(Directory.GetCurrentDirectory(), path)}'...");
            Console.Title = $"SimpleComputer - {Path.GetFileNameWithoutExtension(path)}";
            LoadProgram(File.ReadAllText(path), processor);

            Console.WriteLine("Running program...");
            processor.Run();
            Console.WriteLine("Program exited. Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Returns the full filename with extension with only the path+name given
        /// </summary>
        /// <param name="name">The path+name without extension</param>
        /// <returns>Full Filepath</returns>
        private static string GetFileByName(string name)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{name}.*");

            if (files.Length >= 1)
                return files[0];
            return "";
        }
    }
}
