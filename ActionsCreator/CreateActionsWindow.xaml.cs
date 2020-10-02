using System.Collections;
using System.Windows.Controls;
using CommandClassInterface;
using Console;
using Rename.Helpers;

namespace ActionsCreator
{
    public partial class CreateActionsWindow
    {
        private IList _actionClass;
        
        public CreateActionsWindow()
        {
            InitializeComponent();
            ConsoleWrapper consoleWrapper = new ConsoleWrapper();
            ComboBoxActionClass.Items.Add(consoleWrapper);
            ComboBoxActionClass.Items.Add(new PropertySelector(consoleWrapper));
            ComboBoxActionClass.Items.Add(new DirectorySelector(consoleWrapper));
            ComboBoxActionClass.Items.Add(new SkipFile(consoleWrapper));
        }

        private void ComboBoxActionClass_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBlockDescription.Text = string.Empty;
            ComboBoxActionName.Items.Clear();
            _actionClass = e.AddedItems;
            foreach (object addedItem in _actionClass)
            {
                if (!(addedItem is ICommandClass commandClass)) continue;
                foreach (string command in commandClass.CommandsForJson)
                {
                    ComboBoxActionName.Items.Add(command);
                }
            }
        }

        private void ComboBoxActionName_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (object actionClassObject in _actionClass)
            {
                if (!(actionClassObject is ICommandClass commandClass)) continue;
                foreach (object addedItem in e.AddedItems)
                {
                    string command = addedItem as string;
                    TextBlockDescription.Text = commandClass.GetHelpFor(command);
                }
            }
        }
    }
}