using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MusicMetadataRenamer
{
    public class DirectorySelector : SelectorBase
    {
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
            Console.WriteLine("Directory list cleared.");
        }

        public HashSet<string> Directories { get; } = new HashSet<string>();

        public IEnumerable<string> StartInteractive()
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
                
                if (callResult is bool shouldComplete && shouldComplete)
                    return Directories;
            }
        }

        public virtual void Add(params string[] dirs)
        {
            foreach (string dir in dirs)
            {
                Directories.Add(dir);
            }

            Console.WriteLine("Directories added to list.");
        }

        public virtual void List()
        {
            foreach (string directory in Directories)
            {
                Console.WriteLine(directory);
            }
        }
    }
}