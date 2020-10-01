using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console;
using FileMetadata.Dynamic;
using Rename.Helpers.Interfaces;

namespace Rename.Helpers
{
    public class PropertySelector : SelectorBase, IPropertyList
    {
        public PropertySelector(IConsole consoleWrapper) : base(consoleWrapper) { }
        
        protected override HashSet<string> Commands { get; } = new HashSet<string>(
        new []
        {
            nameof(Add),
            nameof(Clear),
            nameof(Complete),
            nameof(ClearScreen),
            nameof(Help),
            nameof(HelpCommands),
            nameof(HelpProperties),
            nameof(List),
            nameof(Remove)
        });

        protected override Dictionary<string, string> HelpDictionary { get; } = new Dictionary<string, string>(
            new []
            {
                new KeyValuePair<string, string>(nameof(Add), "Add properties to list. Usage: Add <p1> [<p2>] [...]"),
                new KeyValuePair<string, string>(nameof(Clear), "Clear properties list. Usage: Clear"),
                new KeyValuePair<string, string>(nameof(Complete), "Complete property selection step. Usage: Complete"),
                new KeyValuePair<string, string>(nameof(ClearScreen), "Clear current console's screen. Usage: ClearScreen"),
                new KeyValuePair<string, string>(nameof(Help), "Display list of commands with their help and available properties. Usage: Help [<cmd1>] [<cmd2>] [...]"),
                new KeyValuePair<string, string>(nameof(HelpCommands), "Display list of commands. Usage: HelpCommands"),
                new KeyValuePair<string, string>(nameof(HelpProperties), "Display list of available properties. Usage: HelpProperties"),
                new KeyValuePair<string, string>(nameof(List), "Display list of selected properties. Usage: List"), 
                new KeyValuePair<string, string>(nameof(Remove), "Remove properties from the list. Usage: Remove <p1> [<p2>] [...]"), 
            });

        public List<string> Properties { get; } = new List<string>();

        public virtual void StartInteractive()
        {
            while (true)
            {
                System.Console.WriteLine($"{nameof(PropertySelector)} - Type \'Help\' for help:");

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

        public virtual void Add(params string[] properties)
        {
            foreach (string property in properties)
            {
                if (Properties.Contains(property))
                {
                    ConsoleWrapper.WriteLine($"{property} is already on the list so it wasn't added.");
                }
                else
                {
                    Properties.Add(property);
                }
            }

            ConsoleWrapper.WriteLine("Properties added to list.");
        }

        public override void Clear()
        {
            Properties.Clear();
            ConsoleWrapper.WriteLine("Property list cleared.");
        }

        public override void Help(string[] forCommands)
        {
            base.Help(forCommands);
            HelpProperties();
        }

        public virtual void HelpCommands()
        {
            HelpInternal(Commands);
        }

        public virtual void HelpProperties()
        {
            System.Console.WriteLine("Available Properties:");
            foreach (FieldInfo field in typeof(PropertyNames).GetFields())
            {
                System.Console.WriteLine(field.GetValue(null));
            }
        }

        public virtual void List()
        {
            ConsoleWrapper.WriteLine("Selected Properties:");
            foreach (string property in Properties)
            {
                ConsoleWrapper.WriteLine(property);
            }
        }

        public void Remove(string[] properties)
        {
            foreach (string property in properties)
            {
                Properties.Remove(property);
            }
            ConsoleWrapper.WriteLine("Properties removed from the list.");
        }
    }
}