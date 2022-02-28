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
    /// <summary>
    /// Class for renaming multiple files in multiple directories.
    /// </summary>
    public class RenameFiles
    {
        private readonly IConsole _silenceAbleConsole;
        private readonly IStringProcessor _wordProcessor;
        private readonly MetadataRename _metadataRename;

        /// <summary>
        /// Creates new instance of RenameFiles passing dependencies.
        /// </summary>
        /// <param name="silenceAbleConsole">Console to use for output.</param>
        /// <param name="wordProcessor">Optional parameter - can be null to use no additional word processing.</param>
        /// <param name="metadataRename">Instance of MetadataRename class to use for renaming.</param>
        public RenameFiles(IConsole silenceAbleConsole, IStringProcessor wordProcessor, MetadataRename metadataRename)
        {
            _silenceAbleConsole = silenceAbleConsole ?? throw new ArgumentNullException(nameof(silenceAbleConsole));
            _wordProcessor = wordProcessor ?? new DefaultNoProcessor();
            _metadataRename = metadataRename ?? throw new ArgumentNullException(nameof(metadataRename));
        }

        /// <summary>
        /// Renames multiple files.
        /// </summary>
        /// <param name="directoriesToProcess">Directories to include in operation.</param>
        /// <param name="propertiesToUse">Properties to use for renaming.</param>
        /// <param name="allDirectoriesOrBaseOnly">Should all or only top directory be processed?</param>
        public void RenameMultiple(IEnumerable<string> directoriesToProcess, IEnumerable<string> propertiesToUse,
            SearchOption allDirectoriesOrBaseOnly = SearchOption.AllDirectories)
        {
            Parallel.ForEach(directoriesToProcess, dirName =>
            {
                string[] files = Directory.GetFiles(dirName, "*.mp3", allDirectoriesOrBaseOnly);
                _metadataRename.RenameMultiple(files, _wordProcessor, propertiesToUse);
                _silenceAbleConsole.WriteLine($"Renaming in '{dirName}' complete.");
            });
            _silenceAbleConsole.WriteLine("Renaming finished.");
        }
    }
}