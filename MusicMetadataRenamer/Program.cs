using System.Collections.Generic;
using System.Threading.Tasks;
using Console;
using FileMetadata.Dynamic;
using JsonStructures;
using Rename.Helpers;
using StringProcessor;
using StringProcessor.SkipCommonWords;

namespace MusicMetadataRenamer
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            ISilenceAbleConsole silenceAbleConsole = new SilenceAbleConsole();
            PropertySelector propertySelector = new PropertySelector(silenceAbleConsole);
            DirectorySelector directorySelector = new DirectorySelector(silenceAbleConsole);
            SkipFile skipFile = new SkipFile(silenceAbleConsole);
            WordSkipping skippingThese = new WordSkipping();
            
            IStringProcessor processor = new SkipCommonWordsProcessor{ CommonWords = await skippingThese.GetCommonWordsFrom(skipFile.SelectedPath) };

            switch (args.Length)
            {
                case 0:
                {
                    directorySelector.StartInteractive();
                    propertySelector.StartInteractive();

                    string defaultPath = skipFile.SelectedPath;
                    skipFile.Prompt();
                    if (skipFile.SelectedPath != defaultPath)
                        processor = new SkipCommonWordsProcessor{ CommonWords = await skippingThese.GetCommonWordsFrom(skipFile.SelectedPath) };
                    
                    break;
                }
                case 1:
                {
                    var resolver = new ActionResolver(new []
                    {
                        new KeyValuePair<string, object>(nameof(PropertySelector), propertySelector),
                        new KeyValuePair<string, object>(nameof(DirectorySelector), directorySelector),
                        new KeyValuePair<string, object>(silenceAbleConsole.ToString(), silenceAbleConsole),
                        new KeyValuePair<string, object>(nameof(SkipFile), skipFile)
                    });
                    resolver.Execute(args[0]);
                    break;
                }
            }

            RenameFiles renameFiles = new RenameFiles(silenceAbleConsole, processor, new MetadataRename(silenceAbleConsole));
            renameFiles.RenameMultiple(directorySelector.Directories, propertySelector.Properties);
        }
    }
}
