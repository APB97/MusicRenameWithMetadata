using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Console;
using FileMetadata.Dynamic;
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

        public void Execute(IEnumerable<string> directories, IEnumerable<string> properties, IStringProcessor wordProcessor, MetadataRename metadataRename)
        {
            Parallel.ForEach(directories, dirName =>
            {
                metadataRename.RenameMultiple(Directory.GetFiles(dirName, "*.*", SearchOption.AllDirectories), wordProcessor, properties);
                _console.WriteLine($"Renaming in '{dirName}' complete.");
            });
            _console.WriteLine("Renaming finished.");
        }
    }
}