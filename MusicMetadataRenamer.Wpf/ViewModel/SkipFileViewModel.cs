using Console;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using MusicMetadataRenamer.Wpf.Model;
using Rename.Helpers;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class SkipFileViewModel : ObservableObject
    {
        private Ioc ioC;
        private IConsole console;
        private SkipFile skipFile;
        private FileModel fileModel = new FileModel { Path = "skip.txt" };

        public Ioc IoC
        {
            get => ioC;
            set
            {
                ioC = value;
                console = value.GetRequiredService<IConsole>();
                skipFile = value.GetService<SkipFile>();
                skipFile.Select(new[] { fileModel.Path });
            }
        }

        public FileModel FileModel
        {
            get => fileModel;
            set
            {
                if (fileModel != value)
                {
                    fileModel = value;
                    skipFile?.Select(new []{ value.Path});
                    OnPropertyChanged(nameof(FileModel));
                    OnPropertyChanged(nameof(Path));
                }
            }
        }

        public string Path => fileModel.Path;

        public IRelayCommand SelectCommand { get; }

        public SkipFileViewModel()
        {
            SelectCommand = new RelayCommand(SelectFile);
        }

        private void SelectFile()
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Text files|*.txt" };
            if (dialog.ShowDialog() == true)
            {
                fileModel = new FileModel { Path = dialog.FileName };
                console?.WriteLine($"Selected skip file: {fileModel.Path}");
            }
        }
    }
}
