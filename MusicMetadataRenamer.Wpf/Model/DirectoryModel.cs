using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MusicMetadataRenamer.Wpf.Model
{
    public class DirectoryModel : ObservableObject
    {
        private string path;

        public string Path
        {
            get => path; set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged(nameof(Path));
                }
            }
        }
    }
}
