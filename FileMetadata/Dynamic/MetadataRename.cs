using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileMetadata.Extensions;

namespace FileMetadata.Dynamic
{
    public class MetadataRename
    {
        public static void RenameMultiple(Dictionary<dynamic, Dictionary<string, string>> filePropertiesMap)
        {
            foreach (var (file, properties) in filePropertiesMap)
            {
                RenameSingle(file, properties);
            }
        }

        public static void RenameSingle(dynamic folderItem, Dictionary<string, string> properties)
        {
            string path = Shell.Path(folderItem);
            string extension = Path.GetExtension(path);
            string joinedNoIllegalCharacters = properties.Values.ToArray().JoinForFilePath();
            try
            {
                File.Move(path, Path.Combine(Path.GetDirectoryName(path) ?? throw new Exception("path is not correct."),
                    $"{joinedNoIllegalCharacters}{extension}"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}