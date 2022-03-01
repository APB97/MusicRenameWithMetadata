using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandClassInterface;
using Console;
using FileMetadata.Dynamic;
using JsonStructures;
using Newtonsoft.Json;
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

            IStringProcessor processor = new SkipCommonWordsProcessor { CommonWords = await WordSkipping.GetCommonWordsFromAsync(skipFile.SelectedPath) };

            switch (args.Length)
            {
                case 0:
                    {
                        foreach (ISupportsInteractiveMode selector in selectors)
                            selector.StartInteractive();

                        string defaultPath = skipFile.SelectedPath;
                        skipFile.Prompt();
                        if (skipFile.SelectedPath != defaultPath)
                        {
                            processor = new SkipCommonWordsProcessor { CommonWords = await WordSkipping.GetCommonWordsFromAsync(skipFile.SelectedPath) };
                        }


                        var definitons = new ActionDefinitions()
                        {
                            Actions = new[]
                            {
                                new ActionDefinition
                                {
                                    ActionClass = propertySelector.ToString(),
                                    ActionName = "Add",
                                    ActionParameters = propertySelector.Properties.ToArray()
                                },
                                new ActionDefinition
                                {
                                    ActionClass = directorySelector.ToString(),
                                    ActionName = "Add",
                                    ActionParameters = directorySelector.Directories.ToArray()
                                },
                                new ActionDefinition
                                {
                                    ActionClass = silenceAbleConsole.ToString(),
                                    ActionName = silenceAbleConsole.Silent ? "BeSilent" : "DontBeSilent"
                                },
                                new ActionDefinition
                                {
                                    ActionClass = skipFile.ToString(),
                                    ActionName = "Select",
                                    ActionParameters = new [] {skipFile.SelectedPath }
                                }
                            }
                        };
                        var json = JsonConvert.SerializeObject(definitons);
                        File.WriteAllText("LastActions.json", json);

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
