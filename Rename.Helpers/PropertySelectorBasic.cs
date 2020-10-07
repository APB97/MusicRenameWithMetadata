using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Console;
using FileMetadata.Mp3;

namespace Rename.Helpers
{
    /// <summary>
    /// SelectorBase implementation for selecting properties.
    /// </summary>
    public class PropertySelectorBasic : SelectorBase
    {
        /// <inheritdoc />
        /// Additionally assigns value to CommandsForJson
        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public PropertySelectorBasic(ISilenceAbleConsole silenceAbleConsole) : base(silenceAbleConsole)
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
        /// Add properties to selected list.
        /// </summary>
        /// <param name="properties">Properties to select.</param>
        public virtual void Add(params string[] properties)
        {
            foreach (string property in properties)
            {
                AddProperty(property);
            }
        }

        /// <summary>
        /// Add single property to list.
        /// </summary>
        /// <param name="property">Property to add.</param>
        protected virtual void AddProperty(string property)
        {
            if (!Properties.Contains(property))
                Properties.Add(property);
        }

        /// <summary>
        /// Clears list of selected properties.
        /// </summary>
        public virtual void Clear()
        {
            Properties.Clear();
        }

        /// <summary>
        /// Displays list of all available properties.
        /// </summary>
        public void HelpProperties()
        {
            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Available_Properties);
            const BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static;
            foreach (MethodInfo method in typeof(Mp3InfoReader).GetMethods(bindingFlags))
                SilenceAbleConsole.WriteLine(method.Name);
        }

        /// <summary>
        /// Displays current list of selected properties.
        /// </summary>
        public virtual void List()
        {
            foreach (string property in Properties)
            {
                SilenceAbleConsole.WriteLine(property);
            }
        }

        /// <summary>
        /// Removes given properties from the list.
        /// </summary>
        /// <param name="properties">Properties to remove.</param>
        public virtual void Remove(string[] properties)
        {
            foreach (string property in properties)
            {
                Properties.Remove(property);
            }
        }
    }
}