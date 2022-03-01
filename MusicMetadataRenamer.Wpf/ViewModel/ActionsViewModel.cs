using Console;
using JsonStructures;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using Rename.Helpers;
using System.IO;
using System.Linq;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class ActionsViewModel
    {
        public IRelayCommand ExecuteCommand { get; }
        public IRelayCommand SaveActionsCommand { get; }

        public ActionsViewModel()
        {
            ExecuteCommand = new RelayCommand(Rename);
            SaveActionsCommand = new RelayCommand(SaveActions);
        }

        private void Rename()
        {
            var renameService = Ioc.Default.GetRequiredService<RenameFiles>();
            var directoriesService = Ioc.Default.GetRequiredService<DirectorySelector>();
            var propertiesService = Ioc.Default.GetRequiredService<PropertySelector>();
            renameService.RenameMultiple(directoriesService.Directories, propertiesService.Properties);
        }

        private void SaveActions()
        {
            ActionDefinitions definitons = PrepareDefinitions();
            var dialog = new SaveFileDialog() { DefaultExt = ".json", Filter = "JSON files(.json)|*.json" };
            if (dialog.ShowDialog() == true)
            {
                var json = JsonConvert.SerializeObject(definitons);
                File.WriteAllText(dialog.FileName, json);
            }
        }

        private static ActionDefinitions PrepareDefinitions()
        {
            var propertySelector = Ioc.Default.GetRequiredService<PropertySelector>();
            var directorySelector = Ioc.Default.GetRequiredService<DirectorySelector>();
            var console = Ioc.Default.GetRequiredService<ISilenceAbleConsole>();
            var skipFile = Ioc.Default.GetRequiredService<SkipFile>();

            var definitons = new ActionDefinitions()
            {
                Actions = new[]
                {
                    new ActionDefinition
                    {
                        ActionClass = propertySelector.ToString(),
                        ActionName = "Add",
                        ActionParameters = propertySelector.Properties.ToArray()
                    },
                    new ActionDefinition
                    {
                        ActionClass = directorySelector.ToString(),
                        ActionName = "Add",
                        ActionParameters = directorySelector.Directories.ToArray()
                    },
                    new ActionDefinition
                    {
                        ActionClass = console.ToString(),
                        ActionName = console.Silent ? "BeSilent" : "DontBeSilent"
                    },
                    new ActionDefinition
                    {
                        ActionClass = skipFile.ToString(),
                        ActionName = "Select",
                        ActionParameters = new [] {skipFile.SelectedPath }
                    }
                }
            };
            return definitons;
        }
    }
}
