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
        private readonly ISilenceAbleConsole _silenceAbleConsole;
        private readonly IStringProcessor _wordProcessor;
        private readonly MetadataRename _metadataRename;

        public RenameFiles(ISilenceAbleConsole silenceAbleConsole, IStringProcessor wordProcessor, MetadataRename metadataRename)
        {
            _silenceAbleConsole = silenceAbleConsole ?? throw new ArgumentNullException(nameof(silenceAbleConsole));
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
                _silenceAbleConsole.WriteLine($"Renaming in '{dirName}' complete.");
            });
            _silenceAbleConsole.WriteLine("Renaming finished.");
        }
    }
}