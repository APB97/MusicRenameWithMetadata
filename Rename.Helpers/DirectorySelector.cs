using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console;
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

        protected override Dictionary<string, string> HelpDictionary { get; } = new Dictionary<string, string>(
            new []
            {
                new KeyValuePair<string, string>(nameof(Add), "Add directories to processing list. Usage: Add <dir1> [<dir2> [...]]"),
                new KeyValuePair<string, string>(nameof(Clear), "Clear list of directories to process. Usage: Clear"),
                new KeyValuePair<string, string>(nameof(Complete), "Complete directory selection step. Usage: Complete"), 
                new KeyValuePair<string, string>(nameof(ClearScreen), "Clear current console's screen. Usage: ClearScreen"),
                new KeyValuePair<string, string>(nameof(Help), "Display list of Commands and their usage or chosen commands' help. Usage: Help [<cmd1>] [<cmd2>] [...]"),
                new KeyValuePair<string, string>(nameof(List), "Display list of directories to process. Usage: List"), 
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
                System.Console.WriteLine($"{nameof(DirectorySelector)} - Type \'Help\' for help:");
                
                string line = System.Console.ReadLine();
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