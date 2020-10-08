using System.Collections.Generic;
using System.Threading.Tasks;
using CommandClassInterface;
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
            PropertySelector propertySelector = new PropertySelector(silenceAbleConsole, silenceAbleConsole);
            DirectorySelector directorySelector = new DirectorySelector(silenceAbleConsole);
            ISupportsInteractiveMode[] selectors = { directorySelector, propertySelector };
            SkipFile skipFile = new SkipFile(silenceAbleConsole);
            
            IStringProcessor processor = new SkipCommonWordsProcessor{ CommonWords = await WordSkipping.GetCommonWordsFrom(skipFile.SelectedPath) };

            switch (args.Length)
            {
                case 0:
                {
                    foreach (ISupportsInteractiveMode selector in selectors)
                        selector.StartInteractive();

                    string defaultPath = skipFile.SelectedPath;
                    skipFile.Prompt();
                    if (skipFile.SelectedPath != defaultPath)
                        processor = new SkipCommonWordsProcessor{ CommonWords = await WordSkipping.GetCommonWordsFrom(skipFile.SelectedPath) };
                    
                    break;
                }
                case 1:
                {
                    var resolver = new ActionResolver(new[]
                    {
                        new KeyValuePair<string, ICommandClass>(propertySelector.ToString(), propertySelector),
                        new KeyValuePair<string, ICommandClass>(directorySelector.ToString(), directorySelector),
                        new KeyValuePair<string, ICommandClass>(silenceAbleConsole.ToString(), silenceAbleConsole),
                        new KeyValuePair<string, ICommandClass>(skipFile.ToString(), skipFile)
                    });
                    await resolver.Execute(args[0]);
                    break;
                }
            }

            RenameFiles renameFiles = new RenameFiles(silenceAbleConsole, processor, new MetadataRename(silenceAbleConsole));
            renameFiles.RenameMultiple(directorySelector.Directories, propertySelector.Properties);
        }
    }
}
