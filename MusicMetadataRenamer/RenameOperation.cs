using Console;
using FileMetadata.Dynamic;
using Rename.Helpers;
using Rename.Helpers.Interfaces;
using StringProcessor;

namespace MusicMetadataRenamer
{
    public class RenameOperation
    {
        public void ExecuteRenameOperation(IConsole console, IDirectorySet directorySelector, IPropertyList propertySelector, IStringProcessor processor)
        {
            new RenameFiles(console).Execute(directorySelector, propertySelector, processor, new MetadataRename(console));
        }
    }
}