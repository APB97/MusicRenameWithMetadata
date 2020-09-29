using System.Threading.Tasks;

namespace MusicMetadataRenamer
{
    static class Program
    {
        private static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                ConsoleWrapper console = new ConsoleWrapper();
                PropertySelector propertySelector = new PropertySelector(console);
                propertySelector.StartInteractive();

                DirectorySelector directorySelector = new DirectorySelector(console);
                directorySelector.StartInteractive();
                
                new Rename(console).Execute(directorySelector, propertySelector);
                
                return;
            }

            if (args.Length == 1)
            {
                var resolver = new RenameActionResolver();
                await resolver.Execute(args[0]);
            }
        }
    }
}
