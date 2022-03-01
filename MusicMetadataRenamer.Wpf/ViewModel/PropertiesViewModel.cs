using Console;
using FileMetadata.Mp3;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using MusicMetadataRenamer.Wpf.Model;
using Rename.Helpers;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class PropertiesViewModel : ObservableObject
    {
        public Ioc IoC
        {
            get => ioC;
            set
            {
                ioC = value;
                console = value.GetRequiredService<IConsole>();
                propertySelector = value.GetService<PropertySelector>();
                propertySelector.Add(properties.Where(p => p.Included).Select(p => p.PropertyName).ToArray());
                foreach (var item in Properties)
                {
                    item.IoC = ioC;
                }
            }
        }

        private PropertySelector propertySelector;

        private ObservableCollection<Id3PropertyModel> properties;

        private Id3PropertyModel selected;
        private Ioc ioC;
        private IConsole console;

        public ObservableCollection<Id3PropertyModel> Properties
        {
            get => properties;
            set
            {
                if (properties != value)
                {
                    properties = value;
                    OnPropertyChanged(nameof(Properties));
                    MoveUpCommand.NotifyCanExecuteChanged();
                    MoveDownCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public Id3PropertyModel Selected
        {
            get => selected;
            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnPropertyChanged(nameof(Selected));
                    MoveUpCommand.NotifyCanExecuteChanged();
                    MoveDownCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IRelayCommand MoveUpCommand { get; }
        public IRelayCommand MoveDownCommand { get; }

        public PropertiesViewModel(PropertySelector propertySelector) : this()
        {
            this.propertySelector = propertySelector;
        }

        public PropertiesViewModel()
        {
            MoveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            MoveDownCommand = new RelayCommand(MoveDown, CanMoveDown);

            Properties = new ObservableCollection<Id3PropertyModel>();
            var values = typeof(Mp3InfoReader)
                .GetFields()
                .Where(fi => fi.Name.EndsWith("IdSearchPattern", System.StringComparison.InvariantCulture))
                .Select(fi => fi.Name.Substring(0, fi.Name.IndexOf("IdSearchPattern", System.StringComparison.InvariantCulture)));
            foreach (var value in values)
            {
                Properties.Add(new Id3PropertyModel() { PropertyName = value.ToString(), Included = false });
            }

            Id3PropertyModel property = Properties.SingleOrDefault(model => model.PropertyName == "Title");
            if (property != null)
            {
                property.Included = true;
            }
            OnPropertyChanged(nameof(Properties));
        }

        private bool CanMoveDown()
        {
            return selected != null && Properties.IndexOf(selected) < Properties.Count - 1;
        }

        private bool CanMoveUp()
        {
            return Properties.IndexOf(selected) > 0;
        }

        private void MoveUp()
        {
            var current = Properties.IndexOf(selected);
            var currentProp = Properties[current];
            var previousProp = Properties[current - 1];
            (Properties[current], Properties[current - 1]) = (Properties[current - 1], Properties[current]);
            if (currentProp.Included && previousProp.Included)
            {
                int oneIndex = propertySelector.Properties.FindIndex(s => s == currentProp.PropertyName);
                int otherIndex = propertySelector.Properties.FindIndex(s => s == previousProp.PropertyName);
                if (oneIndex < 0)
                {
                    propertySelector.Properties.Add(currentProp.PropertyName);
                    console.WriteLine($"Property included: {currentProp.PropertyName}");
                }
                if (otherIndex < 0)
                {
                    propertySelector.Properties.Add(previousProp.PropertyName);
                    console.WriteLine($"Property included: {currentProp.PropertyName}");
                }
                (propertySelector.Properties[oneIndex], propertySelector.Properties[otherIndex]) = (propertySelector.Properties[otherIndex], propertySelector.Properties[oneIndex]);
            }
        }

        private void MoveDown()
        {
            var current = Properties.IndexOf(selected);
            var currentProp = Properties[current];
            var nextProp = Properties[current + 1];
            (Properties[current], Properties[current + 1]) = (Properties[current + 1], Properties[current]);
            if (currentProp.Included && nextProp.Included)
            {
                int oneIndex = propertySelector.Properties.FindIndex(s => s == currentProp.PropertyName);
                int otherIndex = propertySelector.Properties.FindIndex(s => s == nextProp.PropertyName);
                (propertySelector.Properties[oneIndex], propertySelector.Properties[otherIndex]) = (propertySelector.Properties[otherIndex], propertySelector.Properties[oneIndex]);
            }
        }
    }
}
