using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console;
using Rename.Helpers.Interfaces;

namespace Rename.Helpers
{
    public class DirectorySelector : SelectorBase, IDirectorySet
    {
        public DirectorySelector(IConsole consoleWrapper) : base(consoleWrapper)
        {
            CommandsForJson = new[] {nameof(Add)};
        }

        public override IEnumerable<string> CommandsForJson { get; }

        protected override HashSet<string> Commands { get; } = new HashSet<string>(
        new []
        {
            nameof(Add),
            nameof(Clear),
            nameof(Complete),
            nameof(ClearScreen),
            nameof(Help),
            nameof(List),
            nameof(Remove)
        });

        public override void Clear()
        {
            Directories.Clear();
            ConsoleWrapper.WriteLine(Rename_Helpers_Commands.Messages_Directory_list_cleared);
        }

        public HashSet<string> Directories { get; } = new HashSet<string>();

        public void StartInteractive()
        {
            while (true)
            {
                System.Console.WriteLine(Rename_Helpers_Commands.Type_Help_for_help, nameof(DirectorySelector));
                
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

        public void Add(params string[] dirs)
        {
            foreach (string dir in dirs)
            {
                Directories.Add(dir);
            }

            ConsoleWrapper.WriteLine(Rename_Helpers_Commands.Messages_Directories_added);
        }

        public void List()
        {
            foreach (string directory in Directories)
            {
                ConsoleWrapper.WriteLine(directory);
            }
        }

        public void Remove(params string[] dirs)
        {
            foreach (string directory in dirs)
            {
                Directories.Remove(directory);
            }
            
            ConsoleWrapper.WriteLine(Rename_Helpers_Commands.Messages_Directories_removed);
        }

        public override string ToString()
        {
            return nameof(DirectorySelector);
        }
    }
}