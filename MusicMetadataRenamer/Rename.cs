using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileMetadata.Dynamic;
using StringProcessor.SkipCommonWords;

namespace MusicMetadataRenamer
{
    public class Rename
    {
        public void Execute(DirectorySelector directorySelector, PropertySelector propertySelector)
        {
            var result = Parallel.ForEach(directorySelector.Directories, async dirName =>
            {
                var items = Shell.GetFolderItems(dirName);
                var propertiesMap = Metadata.GetProperties(items, propertySelector.Properties);
                
                MetadataRename.RenameMultiple(propertiesMap, new SkipCommonWordsProcessor()
                {
                    CommonWords = new HashSet<string>(await File.ReadAllLinesAsync("skip.txt"))
                });
                
                Console.WriteLine($"Renaming in '{dirName}' complete.");
            });
            Console.WriteLine("Renaming finished.");
        }
    }
}