using Microsoft.Toolkit.Mvvm.DependencyInjection;
using MusicMetadataRenamer.Wpf.ViewModel;
using System.Windows;

namespace MusicMetadataRenamer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (properties.DataContext as PropertiesViewModel).IoC = Ioc.Default;
            (directories.DataContext as DirectoriesViewModel).IoC = Ioc.Default;
        }
    }
}