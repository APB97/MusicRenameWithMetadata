using Console;

namespace Rename.Helpers
{
    /// <inheritdoc />
    public class PropertySelector : PropertySelectorBasic
    {
        private readonly ISilenceAbleConsole _commandResultsConsole;

        /// <inheritdoc />
        public PropertySelector(ISilenceAbleConsole silenceAbleConsole, ISilenceAbleConsole commandResultsConsole) : base(silenceAbleConsole)
        {
            _commandResultsConsole = commandResultsConsole;
        }

        /// <inheritdoc />
        public override void Add(params string[] properties)
        {
            base.Add(properties);
            _commandResultsConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Properties_added);
        }

        /// <inheritdoc />
        protected override void AddProperty(string property)
        {
            if (Properties.Contains(property))
                _commandResultsConsole.WriteLine(string.Format(Rename_Helpers_Commands.PropertySelector__0__is_already_on_the_list, property));
            base.AddProperty(property);
        }

        /// <inheritdoc />
        public override void Clear()
        {
            base.Clear();
            _commandResultsConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Property_list_cleared);
        }

        /// <inheritdoc />
        public override void List()
        {
            _commandResultsConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Selected_Properties);
            base.List();
        }

        /// <inheritdoc />
        public override void Remove(string[] properties)
        {
            base.Remove(properties);
            _commandResultsConsole.WriteLine(Rename_Helpers_Commands.PropertySelector_Properties_removed);
        }
    }
}