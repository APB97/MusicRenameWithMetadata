using Console;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Rename.Helpers;

namespace MusicMetadataRenamer.Wpf.Model
{
    public class Id3PropertyModel : ObservableObject
    {
        private PropertySelector selector;
        private IConsole console;
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
                        selector?.Add(propertyName);
                        console?.WriteLine($"Property {propertyName} included.");
                    }
                    else
                    {
                        selector?.Remove(new[] { propertyName });
                        console?.WriteLine($"Property {propertyName} excluded.");
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
                console = value.GetRequiredService<IConsole>();
                selector = value.GetService<PropertySelector>();
            }
        }
    }
}
