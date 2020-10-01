using System.Threading.Tasks;
using Console;
using FileMetadata.Dynamic;
using Rename.Helpers;
using Rename.Helpers.Interfaces;
using StringProcessor;
using StringProcessor.SkipCommonWords;

namespace MusicMetadataRenamer
{
    public class RenameOperation
    {
        public async Task ExecuteRenameOperation(ConsoleWrapper console, IDirectorySet directorySelector, IPropertyList propertySelector, SkipFile skipFile)
        {
            var wordsToSkip = new WordSkipping();
            await wordsToSkip.GetCommonWordsFrom(skipFile.SelectedPath);
            IStringProcessor processor = new SkipCommonWordsProcessor { CommonWords = wordsToSkip.CommonWords };
            new RenameFiles(console).Execute(directorySelector, propertySelector, processor, new MetadataRename(console));
        }
    }
}