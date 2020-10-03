using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Console;
using FileMetadata.Dynamic;
using StringProcessor;
using StringProcessor.DefaultNone;

namespace Rename.Helpers
{
    public class RenameFiles
    {
        private readonly IConsole _console;
        private readonly IStringProcessor _wordProcessor;
        private readonly MetadataRename _metadataRename;

        public RenameFiles(IConsole console, IStringProcessor wordProcessor, MetadataRename metadataRename)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
            _wordProcessor = wordProcessor ?? new DefaultNoProcessor();
            _metadataRename = metadataRename ?? throw new ArgumentNullException(nameof(metadataRename));
        }

        public void RenameMultiple(IEnumerable<string> directoriesToProcess, IEnumerable<string> propertiesToUse,
            SearchOption allDirectoriesOrBaseOnly = SearchOption.AllDirectories)
        {
            Parallel.ForEach(directoriesToProcess, dirName =>
            {
                string[] files = Directory.GetFiles(dirName, "*.*", allDirectoriesOrBaseOnly);
                _metadataRename.RenameMultiple(files, _wordProcessor, propertiesToUse);
                _console.WriteLine($"Renaming in '{dirName}' complete.");
            });
            _console.WriteLine("Renaming finished.");
        }
    }
}