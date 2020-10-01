using System.Threading.Tasks;
using Console;
using FileMetadata.Dynamic;
using Rename.Helpers.Interfaces;
using StringProcessor;

namespace Rename.Helpers
{
    public class RenameFiles
    {
        private readonly IConsole _console;

        public RenameFiles(IConsole console)
        {
            _console = console;
        }

        public void Execute(IDirectorySet directorySelector, IPropertyList propertySelector, IStringProcessor wordProcessor, MetadataRename metadataRename)
        {
            Parallel.ForEach(directorySelector.Directories, dirName =>
            {
                var items = Shell.GetFolderItems(dirName);
                var propertiesMap = Metadata.GetProperties(items, propertySelector.Properties);
                
                metadataRename.RenameMultiple(propertiesMap, wordProcessor);
                
                _console.WriteLine($"Renaming in '{dirName}' complete.");
            });
            _console.WriteLine("Renaming finished.");
        }
    }
}