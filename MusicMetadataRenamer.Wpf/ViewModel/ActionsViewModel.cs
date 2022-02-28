using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Rename.Helpers;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class ActionsViewModel
    {
        public IRelayCommand ExecuteCommand { get; }

        public ActionsViewModel()
        {
            ExecuteCommand = new RelayCommand(Rename);
        }

        private void Rename()
        {
            var renameService = Ioc.Default.GetRequiredService<RenameFiles>();
            var directoriesService = Ioc.Default.GetRequiredService<DirectorySelector>();
            var propertiesService = Ioc.Default.GetRequiredService<PropertySelector>();
            renameService.RenameMultiple(directoriesService.Directories, propertiesService.Properties);
        }
    }
}
