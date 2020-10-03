using System.Collections.Generic;
using Console;
using FileMetadata.Dynamic;
using Rename.Helpers;
using StringProcessor;

namespace MusicMetadataRenamer
{
    public class RenameOperation
    {
        public void ExecuteRenameOperation(IConsole console, IEnumerable<string> directories, IEnumerable<string> properties, IStringProcessor processor)
        {
            new RenameFiles(console).Execute(directories, properties, processor, new MetadataRename(console));
        }
    }
}