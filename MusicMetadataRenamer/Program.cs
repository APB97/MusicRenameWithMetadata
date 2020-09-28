using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileMetadata.Dynamic;
using StringProcessor.SkipCommonWords;

namespace MusicMetadataRenamer
{
    static class Program
    {
        private static string _skipTxtFile = "skip.txt";

        private static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                PropertySelector propertySelector = new PropertySelector();
                var propertiesToUse = propertySelector.StartInteractive();

                DirectorySelector directorySelector = new DirectorySelector();
                var inputDirectories = directorySelector.StartInteractive();
                
                new Rename().Execute(directorySelector, propertySelector);
                
                return;
            }

            if (args.Length == 1)
            {
                var resolver = new RenameActionResolver();
                await resolver.Execute(args[0]);
                
                return;
            }
            
            string directoryWithMusic = args.Length > 0 ? args[0] : Console.ReadLine();

            string[] properties = ChooseProperties();

            var items = Shell.GetFolderItems(directoryWithMusic);
            Dictionary<dynamic, Dictionary<string, string>> propertiesMap =
                Metadata.GetProperties(items, properties);
            MetadataRename.RenameMultiple(propertiesMap,
                new SkipCommonWordsProcessor
                    {CommonWords = new HashSet<string>(await File.ReadAllLinesAsync(_skipTxtFile))});
            
            Console.WriteLine();
            Console.WriteLine("Operation completed. Press [Enter] to exit.");
            Console.ReadLine();
        }

        private static string[] ChooseProperties()
        {
            List<string> chosenProperties = new List<string>();

            while (chosenProperties.Count < 2)
            {
                Console.WriteLine();
                Console.WriteLine("Choose one option from 1 to 3:");
                Console.WriteLine("1 - select Artists");
                Console.WriteLine("2 - select Title");
                Console.WriteLine("3 - end selection");

                char input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        chosenProperties.Add(PropertyNames.Artists);
                        break;
                    case '2':
                        chosenProperties.Add(PropertyNames.Title);
                        break;
                    case '3':
                        return chosenProperties.Count != 0 ? chosenProperties.ToArray() : new[] {PropertyNames.Title};
                    default:
                        return new[] {PropertyNames.Title};
                }
            }
            
            return chosenProperties.ToArray();
        }
    }
}
