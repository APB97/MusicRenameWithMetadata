using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Console;
using FileMetadata.Dynamic;
using Microsoft.WindowsAPICodePack.Dialogs;
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
            CommonOpenFileDialog openDialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                AddToMostRecentlyUsedList = true,
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            };
            var result = openDialog.ShowDialog(Window.GetWindow(this));
            if (result == CommonFileDialogResult.Ok)
            {
                string directory = openDialog.FileName;
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

    internal class DummyConsole : IConsole
    {
        public void WriteLine(string text)
        {
            
        }

        public void Clear()
        {
            
        }

        public void WriteLine(object value)
        {
            
        }
    }
}