using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FileMetadata.Dynamic;
using Microsoft.Win32;
using StringProcessor.DefaultNone;

namespace MusicMetadataRenamer.Wpf
{
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            ComboBoxProperties.Items.Add(nameof(FileMetadata.Mp3.Mp3InfoReader.Album));
            ComboBoxProperties.Items.Add(nameof(FileMetadata.Mp3.Mp3InfoReader.Artists));
            ComboBoxProperties.Items.Add(nameof(FileMetadata.Mp3.Mp3InfoReader.Title));
        }

        private void ButtonAddDirectory_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            open.Title = "Pick a file to use its directory";
            var success = open.ShowDialog();
            if (success == true)
            {
                string directory = new FileInfo(open.FileName).DirectoryName;
                var entry = new DirectoryEntry
                {
                    DirectoryName = directory
                };
                StackDirectories.Children.Add(entry);
            }
        }

        private void ButtonExecute_OnClick(object sender, RoutedEventArgs e)
        {
            var dirs = StackDirectories.Children.Cast<DirectoryEntry>().Select(de => de.DirectoryName).ToArray();
            if (dirs.Length == 0) return;
            var metadataRename = new MetadataRename(new DummyConsole(), TextBoxSeparator.Text);
            metadataRename.RenameMultiple(dirs, new DefaultNoProcessor());
        }

        private void ButtonAddProp_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = ComboBoxProperties.SelectedItem as string;
            StackPanelProperties.Children.Add(new DirectoryEntry { DirectoryName = selected});
        }
    }
}