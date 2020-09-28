using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MusicMetadataRenamer
{
    public class DirectorySelector
    {
        private readonly HashSet<string> _directories = new HashSet<string>();
        
        private static readonly HashSet<string> Commands = new HashSet<string>(
            new []
            {
                nameof(Add),
                nameof(ClearScreen),
                nameof(Help),
                nameof(List)
            });

        public IEnumerable<string> Directories => _directories;
        
        public IEnumerable<string> StartInteractive()
        {
            while (true)
            {
                Console.WriteLine($"{nameof(DirectorySelector)} - Type \'Help\' for help:");
                
                string line = Console.ReadLine();
                string[] inputs = line?.Split(' ');
                string command = inputs?[0];

                if (string.IsNullOrWhiteSpace(line))
                    return Directories;
                
                if (!Commands.Contains(command))
                    continue;

                MethodInfo methodInfo = GetType().GetMethod(command);
                methodInfo?.Invoke(this,
                    methodInfo.GetParameters().Length == 1
                        ? new object[] {inputs.Skip(1).ToArray()}
                        : new object[] { });
            }
        }

        public virtual void Help()
        {
            foreach (string command in Commands)
            {
                Console.WriteLine(command);
            }
        }

        public virtual void Add(params string[] dirs)
        {
            foreach (string dir in dirs)
            {
                _directories.Add(dir);
            }

            Console.WriteLine("Directories added to list.");
        }

        public virtual void List()
        {
            foreach (string directory in _directories)
            {
                Console.WriteLine(directory);
            }
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}