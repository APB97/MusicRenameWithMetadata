using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileMetadata.Dynamic;
using StringProcessor.SkipCommonWords;

namespace MusicMetadataRenamer
{
    class Program
    {
        private static string _skipTxtFile = "skip.txt";

        private static async Task Main(string[] args)
        {
            if (args.Length == 0) Console.WriteLine("Enter full directory path:");
            string directoryWithMusic = args.Length > 0 ? args[0] : Console.ReadLine();
            var items = Shell.GetFolderItems(directoryWithMusic);
            Dictionary<dynamic, Dictionary<string, string>> propertiesMap =
                Metadata.GetProperties(items, new[] {PropertyNames.Artists, PropertyNames.Title});
            MetadataRename.RenameMultiple(propertiesMap, new SkipCommonWordsProcessor { CommonWords = new HashSet<string>(await File.ReadAllLinesAsync(_skipTxtFile))});
            Console.WriteLine("Operation completed. Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
