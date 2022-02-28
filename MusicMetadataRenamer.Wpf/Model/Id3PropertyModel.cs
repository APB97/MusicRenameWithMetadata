using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Rename.Helpers;

namespace MusicMetadataRenamer.Wpf.Model
{
    public class Id3PropertyModel : ObservableObject
    {
        private PropertySelector _selector;

        public Id3PropertyModel()
        {
        }

        private string propertyName;
        private bool included;
        private Ioc ioC;

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
                    if (included)
                    {
                        _selector?.Add(propertyName);
                    }
                    else
                    {
                        _selector?.Remove(new[] { propertyName });
                    }
                }
            }
        }

        public Ioc IoC
        {
            get => ioC;
            set
            {
                ioC = value;
                _selector = value.GetService<PropertySelector>();
            }
        }
    }
}
