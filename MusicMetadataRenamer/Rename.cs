﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileMetadata.Dynamic;
using Rename.Helpers;
using Rename.Helpers.Interfaces;
using StringProcessor.SkipCommonWords;

namespace MusicMetadataRenamer
{
    public class Rename
    {
        private readonly ConsoleWrapper _console;

        public Rename(ConsoleWrapper console)
        {
            _console = console;
        }

        public void Execute(IDirectorySet directorySelector, IPropertyList propertySelector, MetadataRename metadataRename)
        {
            Parallel.ForEach(directorySelector.Directories, async dirName =>
            {
                var items = Shell.GetFolderItems(dirName);
                var propertiesMap = Metadata.GetProperties(items, propertySelector.Properties);
                
                metadataRename.RenameMultiple(propertiesMap, new SkipCommonWordsProcessor()
                {
                    CommonWords = new HashSet<string>(await File.ReadAllLinesAsync("skip.txt"))
                });
                
                _console.WriteLine($"Renaming in '{dirName}' complete.");
            });
            _console.WriteLine("Renaming finished.");
        }
    }
}