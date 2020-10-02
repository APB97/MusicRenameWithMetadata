using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console;
using Rename.Helpers.Interfaces;

namespace Rename.Helpers
{
    public class PropertySelector : SelectorBase, IPropertyList
    {
        public PropertySelector(IConsole consoleWrapper) : base(consoleWrapper)
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
            nameof(HelpCommands),
            nameof(HelpProperties),
            nameof(List),
            nameof(Remove)
        });

        public List<string> Properties { get; } = new List<string>();

        public void StartInteractive()
        {
            while (true)
            {
                System.Console.WriteLine(Rename_Helpers_Commands.Type_Help_for_help, nameof(PropertySelector));

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

        public void Add(params string[] properties)
        {
            foreach (string property in properties)
            {
                if (Properties.Contains(property))
                {
                    ConsoleWrapper.WriteLine(string.Format(Rename_Helpers_Commands.PropertySelector__0__is_already_on_the_list, property));
                }
                else
                {
                    Properties.Add(property);
                }
            }

            ConsoleWrapper.WriteLine(Rename_Helpers_Commands.PropertySelector_Properties_added);
        }

        public override void Clear()
        {
            Properties.Clear();
            ConsoleWrapper.WriteLine(Rename_Helpers_Commands.PropertySelector_Property_list_cleared);
        }

        public void HelpCommands()
        {
            HelpInternal(Commands);
        }

        public void HelpProperties()
        {
            System.Console.WriteLine(Rename_Helpers_Commands.PropertySelector_Available_Properties);
        }

        public void List()
        {
            ConsoleWrapper.WriteLine(Rename_Helpers_Commands.PropertySelector_Selected_Properties);
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
            ConsoleWrapper.WriteLine(Rename_Helpers_Commands.PropertySelector_Properties_removed);
        }

        public override string ToString()
        {
            return nameof(PropertySelector);
        }
    }
}