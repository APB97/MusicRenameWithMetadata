﻿using Console;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using MusicMetadataRenamer.Wpf.Model;
using Rename.Helpers;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class DirectoriesViewModel : ObservableObject
    {
        private IConsole console;

        public Ioc IoC
        {
            get => ioC;
            set
            {
                ioC = value;
                directorySelector = value.GetService<DirectorySelector>();
                console = value.GetRequiredService<IConsole>();
            }
        }

        private DirectorySelector directorySelector;
        private ObservableCollection<DirectoryModel> directories;
        private DirectoryModel selectedDirectory;
        private Ioc ioC;

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
                ExcludeCommand.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand ExcludeCommand { get; }
        public IRelayCommand AddCommand { get; }

        public DirectoriesViewModel(DirectorySelector directorySelector) : this()
        {
            this.directorySelector = directorySelector;
        }

        public DirectoriesViewModel()
        {
            Directories = new ObservableCollection<DirectoryModel>();
            ExcludeCommand = new RelayCommand(ExcludeSelectedDirectory, CanExcludeSelected);
            AddCommand = new RelayCommand(AddDirectory);
        }

        private void AddDirectory()
        {
            OpenFileDialog open = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Title = "Pick a file to use its directory"
            };
            var success = open.ShowDialog();
            if (success == true)
            {
                string directory = new FileInfo(open.FileName).DirectoryName;
                Directories.Add(new DirectoryModel { Path = directory });
                OnPropertyChanged(nameof(Directories));
                directorySelector.Directories.Add(directory);
                console.WriteLine($"Directory included: {directory}");
            }
        }

        private bool CanExcludeSelected()
        {
            return !string.IsNullOrEmpty(selectedDirectory?.Path);
        }

        private void ExcludeSelectedDirectory()
        {
            Directories.Remove(selectedDirectory);
            directorySelector.Directories.Remove(selectedDirectory.Path);
            console.WriteLine($"Directory excluded: {selectedDirectory.Path}");
        }
    }
}
