using System;
using System.Collections.Generic;
using MusicMetadataRenamer.Dynamic.Extensions;

namespace MusicMetadataRenamer
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0) Console.WriteLine("Enter full directory path:");
            string directoryWithMusic = args.Length > 0 ? args[0] : Console.ReadLine();
            var items = Shell.GetFolderItems(directoryWithMusic);
            Dictionary<dynamic, Dictionary<string, string>> propertiesMap =
                Metadata.GetProperties(items, new[] {PropertyNames.Artists, PropertyNames.Title});
            MetadataRename.RenameMultiple(propertiesMap);
            Console.WriteLine("Operation completed. Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
