using System.Collections.Generic;
using System.Threading.Tasks;
using Console;
using FileMetadata.Dynamic;
using Rename.Helpers;
using StringProcessor;
using StringProcessor.SkipCommonWords;

namespace MusicMetadataRenamer
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            IConsole console = new ConsoleWrapper();
            PropertySelector propertySelector = new PropertySelector(console);
            DirectorySelector directorySelector = new DirectorySelector(console);
            SkipFile skipFile = new SkipFile(console);
            WordSkipping skippingThese = new WordSkipping();
            await skippingThese.GetCommonWordsFrom(skipFile.SelectedPath);
            IStringProcessor processor = new SkipCommonWordsProcessor{ CommonWords = skippingThese.CommonWords };

            switch (args.Length)
            {
                case 0:
                {
                    propertySelector.StartInteractive();
                    directorySelector.StartInteractive();
                
                    skipFile.Prompt();
                    processor = new SkipCommonWordsProcessor{ CommonWords = skippingThese.CommonWords };
                    
                    new RenameFiles(console).Execute(directorySelector, propertySelector, processor, new MetadataRename(console));
                    break;
                }
                case 1:
                {
                    var resolver = new ActionResolver(new []
                    {
                        new KeyValuePair<string, object>(nameof(PropertySelector), propertySelector),
                        new KeyValuePair<string, object>(nameof(DirectorySelector), directorySelector),
                        new KeyValuePair<string, object>("Console", console),
                        new KeyValuePair<string, object>(nameof(SkipFile), skipFile)
                    });
                    await resolver.Execute(args[0]);
                    new RenameOperation().ExecuteRenameOperation(console, directorySelector, propertySelector, processor);
                    break;
                }
            }
        }
    }
}
