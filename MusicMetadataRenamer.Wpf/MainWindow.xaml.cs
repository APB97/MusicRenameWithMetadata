using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Windows;

namespace MusicMetadataRenamer.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            properties.ViewModel.IoC = Ioc.Default;
            directories.ViewModel.IoC = Ioc.Default;
            skipFile.ViewModel.IoC = Ioc.Default;
            actions.ViewModel.PropertiesViewModel = properties.ViewModel;
            actions.ViewModel.DirectoriesViewModel = directories.ViewModel;
            actions.ViewModel.SkipFileViewModel = skipFile.ViewModel;
        }
    }
}