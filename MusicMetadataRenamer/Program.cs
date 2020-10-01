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
            switch (args.Length)
            {
                case 0:
                {
                    ConsoleWrapper console = new ConsoleWrapper();
                    PropertySelector propertySelector = new PropertySelector(console);
                    propertySelector.StartInteractive();

                    DirectorySelector directorySelector = new DirectorySelector(console);
                    directorySelector.StartInteractive();
                
                    WordSkipping skippingThese = new WordSkipping();
                    SkipFile skipFile = new SkipFile(console);
                    skipFile.Prompt();
                    await skippingThese.GetCommonWordsFrom(skipFile.SelectedPath);
                    IStringProcessor processor = new SkipCommonWordsProcessor{ CommonWords = skippingThese.CommonWords };
                    
                    new Rename(console).Execute(directorySelector, propertySelector, processor, new MetadataRename(console));
                    break;
                }
                case 1:
                {
                    var resolver = new RenameActionResolver();
                    await resolver.Execute(args[0]);
                    break;
                }
            }
        }
    }
}
