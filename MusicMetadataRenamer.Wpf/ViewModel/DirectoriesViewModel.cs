using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using MusicMetadataRenamer.Wpf.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class DirectoriesViewModel : ObservableObject
    {
        private ObservableCollection<DirectoryModel> directories;
        private DirectoryModel selectedDirectory;

        public ObservableCollection<DirectoryModel> Directories
        {
            get => directories;
            set
            {
                if (directories != value)
                {
                    directories = value;
                    OnPropertyChanged(nameof(Directories));
                }
            }
        }

        public DirectoryModel SelectedDirectory
        {
            get => selectedDirectory;
            set
            {
                selectedDirectory = value;
                // Notify changed
                ExcludeCommand.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand ExcludeCommand { get; }
        public IRelayCommand AddCommand { get; }

        public DirectoriesViewModel()
        {
            Directories = new ObservableCollection<DirectoryModel>();
            ExcludeCommand = new RelayCommand(ExcludeSelectedDirectory, CanExcludeSelected);
            AddCommand = new RelayCommand(AddDirectory);
        }

        private void AddDirectory()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            open.Title = "Pick a file to use its directory";
            var success = open.ShowDialog();
            if (success == true)
            {
                string directory = new FileInfo(open.FileName).DirectoryName;
                Directories.Add(new DirectoryModel { Path = directory });
                OnPropertyChanged(nameof(Directories));
            }
        }

        private bool CanExcludeSelected()
        {
            return !string.IsNullOrEmpty(selectedDirectory?.Path);
        }

        private void ExcludeSelectedDirectory()
        {
            Directories.Remove(selectedDirectory);
        }
    }
}
