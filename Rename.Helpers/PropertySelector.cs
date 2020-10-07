using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console;
using FileMetadata.Mp3;

namespace Rename.Helpers
{
    /// <summary>
    /// SelectorBase implementation for selecting properties.
    /// </summary>
    public class PropertySelector : SelectorBase
    {
        /// <inheritdoc />
        /// Additionally assigns value to CommandsForJson
        public PropertySelector(ISilenceAbleConsole silenceAbleConsole) : base(silenceAbleConsole)
        {
            CommandsForJson = new[] {nameof(Add)};
        }

        /// <inheritdoc />
        public override IEnumerable<string> CommandsForJson { get; }

        /// <inheritdoc />
        protected override HashSet<string> Commands { get; } = new HashSet<string>(
        new []
        {
            nameof(Add),
            nameof(Clear),
            nameof(Complete),
            nameof(ClearScreen),
            nameof(Help),
            nameof(HelpProperties),
            nameof(List),
            nameof(Remove)
        });

        /// <summary>
        /// List of selected properties.
        /// </summary>
        public List<string> Properties { get; } = new List<string>();

        /// <summary>
        /// Launches Interactive mode for current class.
        /// </summary>
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

        /// <summary>
        /// Add properties to selected list.
        /// </summary>
        /// <param name="properties">Properties to select.</param>
        public void Add(params string[] properties)
        {
            foreach (string property in properties)
            {
                if (Properties.Contains(property))
                {
                    SilenceAbleConsole.WriteLine(string.Format(Rename_Helpers_Commands.PropertySelector__0__is_already_on_the_list, property));
                }
                else
                {
                    Properties.Add(property);
                }
            }

            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Properties_added);
        }

        /// <summary>
        /// Clears list of selected properties.
        /// </summary>
        public void Clear()
        {
            Properties.Clear();
            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Property_list_cleared);
        }

        /// <summary>
        /// Displays list of all available properties.
        /// </summary>
        public void HelpProperties()
        {
            System.Console.WriteLine(Rename_Helpers_Commands.PropertySelector_Available_Properties);
            const BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static;
            foreach (MethodInfo method in typeof(Mp3InfoReader).GetMethods(bindingFlags))
            {
                System.Console.WriteLine(method.Name);
            }
        }

        /// <summary>
        /// Displays current list of selected properties.
        /// </summary>
        public void List()
        {
            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Selected_Properties);
            foreach (string property in Properties)
            {
                SilenceAbleConsole.WriteLine(property);
            }
        }

        /// <summary>
        /// Removes given properties from the list.
        /// </summary>
        /// <param name="properties">Properties to remove.</param>
        public void Remove(string[] properties)
        {
            foreach (string property in properties)
            {
                Properties.Remove(property);
            }
            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Properties_removed);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return nameof(PropertySelector);
        }
    }
}