using System.Windows;
using System.Windows.Controls;
using MusicMetadataRenamer.Wpf.Helpers;

namespace MusicMetadataRenamer.Wpf
{
    public partial class DirectoryEntry : UserControl
    {
        public string DirectoryName { get; set; }

        public DirectoryEntry()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ButtonExclude_OnClick(object sender, RoutedEventArgs e)
        {
            Parent.DetachChild(this);
        }
    }
}