using System.Windows.Controls;

namespace MusicMetadataRenamer.Wpf.View
{
    public partial class ConsoleView : UserControl
    {
        public ConsoleView()
        {
            InitializeComponent();
        }

        private void ScrollToBottom(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.ScrollToEnd();
        }
    }
}
