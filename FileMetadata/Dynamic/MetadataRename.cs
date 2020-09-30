using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileMetadata.Extensions;
using Rename.Helpers;
using StringProcessor;

namespace FileMetadata.Dynamic
{
    public class MetadataRename
    {
        private readonly ConsoleWrapper _console;

        public MetadataRename(ConsoleWrapper console)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
        }
        
        /// <summary>
        /// Renames multiple files based on dictionary obtained via <see cref="Metadata"/>.<see cref="Metadata.GetProperties"/>.
        /// </summary>
        /// <param name="filePropertiesMap">Dictionary of pairs (file, propertyValues)</param>
        /// <param name="processor">Additional processor to apply to names of files</param>
        public void RenameMultiple(Dictionary<dynamic, Dictionary<string, string>> filePropertiesMap,
            IStringProcessor processor)
        {
            if (filePropertiesMap == null) throw new ArgumentNullException(nameof(filePropertiesMap));
            if (processor == null) throw new ArgumentNullException(nameof(processor));
            foreach (var (file, properties) in filePropertiesMap)
            {
                RenameSingle(file, properties, processor);
            }
        }

        private void RenameSingle(dynamic folderItem, Dictionary<string, string> properties, IStringProcessor processor)
        {
            // Get path of a folderItem
            string filePath = Shell.Path(folderItem);
            // Extract extension from filePath
            string extension = Path.GetExtension(filePath);
            // Combine properties' values and skip invalid fileName characters
            string fileNameWithSkippedChars = properties.Values.ToArray().JoinForFilePath();
            // Apply additional processing
            string additionallyProcessedName = processor.Process(fileNameWithSkippedChars);
            try
            {
                // Rename from filePath to new name
                File.Move(filePath, Path.Combine(Path.GetDirectoryName(filePath) ?? throw new Exception("path is not correct."),
                    $"{additionallyProcessedName}{extension}"));
            }
            catch (Exception e)
            {
                // Display exception if not in Silent Console mode
                _console.WriteLine(e);
            }
        }
    }
}