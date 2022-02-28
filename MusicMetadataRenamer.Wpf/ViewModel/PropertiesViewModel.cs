using FileMetadata.Mp3;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MusicMetadataRenamer.Wpf.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class PropertiesViewModel : ObservableObject
    {
        private ObservableCollection<Id3PropertyModel> properties;

        private Id3PropertyModel selected;

        public ObservableCollection<Id3PropertyModel> Properties
        {
            get => properties;
            set
            {
                if (properties != value)
                {
                    properties = value;
                    OnPropertyChanged(nameof(Properties));
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
                }
            }
        }

        public IRelayCommand MoveUpCommand { get; }
        public IRelayCommand MoveDownCommand { get; }

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
                Properties.Add(new Id3PropertyModel { PropertyName = value.ToString(), Included = false });
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
            (Properties[current], Properties[current - 1]) = (Properties[current - 1], Properties[current]);
        }

        private void MoveDown()
        {
            var current = Properties.IndexOf(selected);
            (Properties[current], Properties[current + 1]) = (Properties[current + 1], Properties[current]);
        }
    }
}
