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
            SilenceAbleConsole silenceAbleConsole = new SilenceAbleConsole();
            ComboBoxActionClass.Items.Add(silenceAbleConsole);
            ComboBoxActionClass.Items.Add(new PropertySelector(silenceAbleConsole));
            ComboBoxActionClass.Items.Add(new DirectorySelector(silenceAbleConsole));
            ComboBoxActionClass.Items.Add(new SkipFile(silenceAbleConsole));
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