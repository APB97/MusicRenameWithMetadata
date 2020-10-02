using System.Windows;

namespace ActionsCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CreateActionsWindow _actionsCreation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            if (_actionsCreation == null || !_actionsCreation.IsLoaded)
                _actionsCreation = new CreateActionsWindow();
            _actionsCreation.Show();
        }
    }
}