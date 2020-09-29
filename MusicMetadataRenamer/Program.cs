using System.Threading.Tasks;
using FileMetadata.Dynamic;
using Rename.Helpers;

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
                    await skippingThese.GetCommonWordsFrom("skip.txt");
                    
                    new Rename(console).Execute(directorySelector, propertySelector, skippingThese, new MetadataRename(console));
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
