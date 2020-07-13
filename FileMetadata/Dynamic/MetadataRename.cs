using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileMetadata.Extensions;
using StringProcessor;

namespace FileMetadata.Dynamic
{
    public static class MetadataRename
    {
        public static void RenameMultiple(Dictionary<dynamic, Dictionary<string, string>> filePropertiesMap,
            IStringProcessor processor)
        {
            foreach (var (file, properties) in filePropertiesMap)
            {
                RenameSingle(file, properties, processor);
            }
        }

        public static void RenameSingle(dynamic folderItem, Dictionary<string, string> properties, IStringProcessor processor)
        {
            string path = Shell.Path(folderItem);
            string extension = Path.GetExtension(path);
            string joinedNoIllegalCharacters = properties.Values.ToArray().JoinForFilePath();
            string additionallyProcessed = processor.Process(joinedNoIllegalCharacters);
            try
            {
                File.Move(path, Path.Combine(Path.GetDirectoryName(path) ?? throw new Exception("path is not correct."),
                    $"{additionallyProcessed}{extension}"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}