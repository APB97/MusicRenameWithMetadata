using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MusicMetadataRenamer.Wpf.Model
{
    public class Id3PropertyModel : ObservableObject
    {
        private string propertyName;
        private bool included;

        public string PropertyName
        {
            get => propertyName;
            set 
            {
                if (propertyName != value)
                {
                    propertyName = value;
                    OnPropertyChanged(nameof(PropertyName));
                }
            }
        }

        public bool Included
        {
            get => included;
            set
            {
                if (included != value)
                {
                    included = value;
                    OnPropertyChanged(nameof(Included));
                }
            }
        }
    }
}
