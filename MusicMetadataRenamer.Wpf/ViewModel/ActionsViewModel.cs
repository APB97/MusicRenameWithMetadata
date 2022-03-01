using Console;
using JsonStructures;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using MusicMetadataRenamer.Wpf.Model;
using Newtonsoft.Json;
using Rename.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class ActionsViewModel
    {
        public IRelayCommand ExecuteCommand { get; }
        public IRelayCommand SaveActionsCommand { get; }
        public IRelayCommand LoadActionsCommand { get; }

        public DirectoriesViewModel DirectoriesViewModel { get; set; }
        public PropertiesViewModel PropertiesViewModel { get; set; }

        public ActionsViewModel()
        {
            ExecuteCommand = new RelayCommand(Rename);
            SaveActionsCommand = new RelayCommand(SaveActions);
            LoadActionsCommand = new RelayCommand(LoadActions);
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
            var dialog = new SaveFileDialog() { DefaultExt = ".json", Filter = "JSON files|*.json" };
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

        private void LoadActions()
        {
            var dialog = new OpenFileDialog() { DefaultExt = ".json", Filter = "JSON files|*.json" };
            if (dialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(dialog.FileName);
                var definitons = JsonConvert.DeserializeObject<ActionDefinitions>(json);
                HashSet<string> setOfProperties = DeterminePropertySet(definitons);
                ApplyProperties(setOfProperties);
                HashSet<string> setOfDirectories = DetermineDirectorySet(definitons);
                ApplyDirectories(setOfDirectories);
            }
        }

        private void ApplyDirectories(HashSet<string> setOfDirectories)
        {
            var list = setOfDirectories.ToList();
            if (DirectoriesViewModel != null)
            {
                DirectoriesViewModel.Directories.Clear();
                foreach (var item in list)
                {
                    DirectoriesViewModel.Directories.Add(new DirectoryModel { Path = item });
                }
            }
        }

        private HashSet<string> DetermineDirectorySet(ActionDefinitions definitons)
        {
            var directoryActions = definitons.Actions.Where(action => action.ActionClass == nameof(DirectorySelector));
            var set = new HashSet<string>();

            foreach (var item in directoryActions)
            {
                if (item.ActionName == nameof(DirectorySelector.Add))
                {
                    foreach (var dir in item.ActionParameters)
                    {
                        set.Add(dir);
                    }
                }
                else if (item.ActionName == nameof(DirectorySelector.Remove))
                {
                    foreach (var dir in item.ActionParameters)
                    {
                        set.Remove(dir);
                    }
                }
            }

            return set;
        }

        private void ApplyProperties(HashSet<string> setOfProperties)
        {
            var listOFProperties = setOfProperties.ToList();
            if (PropertiesViewModel != null)
            {
                int currentIndex = 0;
                var properties = PropertiesViewModel.Properties.ToList();
                foreach (var property in properties)
                {
                    property.Included = setOfProperties.Contains(property.PropertyName);
                    if (property.Included)
                    {
                        int indexOf = listOFProperties.IndexOf(property.PropertyName);
                        (PropertiesViewModel.Properties[indexOf], PropertiesViewModel.Properties[currentIndex]) =
                            (PropertiesViewModel.Properties[currentIndex], PropertiesViewModel.Properties[indexOf]);
                        currentIndex++;
                    }
                }
            }
        }

        private static HashSet<string> DeterminePropertySet(ActionDefinitions definitons)
        {
            var propertyActions = definitons.Actions.Where(action => action.ActionClass == nameof(PropertySelector)).ToArray();

            var setOfProperties = new HashSet<string>();
            foreach (var item in propertyActions)
            {
                if (item.ActionName == nameof(PropertySelector.Add))
                {
                    foreach (var param in item.ActionParameters)
                    {
                        setOfProperties.Add(param);
                    }
                }
                else if (item.ActionName == nameof(PropertySelector.Remove))
                {
                    foreach (var param in item.ActionParameters)
                    {
                        setOfProperties.Remove(param);
                    }
                }
            }

            return setOfProperties;
        }
    }
}
