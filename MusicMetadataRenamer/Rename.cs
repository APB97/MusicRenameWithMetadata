﻿using System.Threading.Tasks;
using Console;
using FileMetadata.Dynamic;
using Rename.Helpers.Interfaces;
using StringProcessor;

namespace MusicMetadataRenamer
{
    public class Rename
    {
        private readonly ConsoleWrapper _console;

        public Rename(ConsoleWrapper console)
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