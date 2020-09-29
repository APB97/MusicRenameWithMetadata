using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rename.Helpers;
using Rename.Helpers.Interfaces;

namespace MusicMetadataRenamer
{
    public class DirectorySelector : SelectorBase, IDirectorySet
    {
        public DirectorySelector(ConsoleWrapper consoleWrapper) : base(consoleWrapper) { }
        
        protected override HashSet<string> Commands { get; } = new HashSet<string>(
        new []
        {
            nameof(Add),
            nameof(Clear),
            nameof(Complete),
            nameof(ClearScreen),
            nameof(Help),
            nameof(List)
        });

        public override void Clear()
        {
            Directories.Clear();
            ConsoleWrapper.WriteLine("Directory list cleared.");
        }

        public HashSet<string> Directories { get; } = new HashSet<string>();

        public void StartInteractive()
        {
            while (true)
            {
                Console.WriteLine($"{nameof(DirectorySelector)} - Type \'Help\' for help:");
                
                string line = Console.ReadLine();
                string[] inputs = line?.Split(' ');
                string command = inputs?[0];

                if (string.IsNullOrWhiteSpace(command) || !Commands.Contains(command))
                    continue;

                MethodInfo methodInfo = GetType().GetMethod(command);
                object callResult = methodInfo?.Invoke(this,
                    methodInfo.GetParameters().Length == 1
                        ? new object[] {inputs.Skip(1).ToArray()}
                        : new object[] { });
                
                if (callResult is bool shouldComplete && shouldComplete) return;
            }
        }

        public virtual void Add(params string[] dirs)
        {
            foreach (string dir in dirs)
            {
                Directories.Add(dir);
            }

            ConsoleWrapper.WriteLine("Directories added to list.");
        }

        public virtual void List()
        {
            foreach (string directory in Directories)
            {
                ConsoleWrapper.WriteLine(directory);
            }
        }
    }
}