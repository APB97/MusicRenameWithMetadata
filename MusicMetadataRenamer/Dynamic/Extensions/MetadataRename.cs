using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicMetadataRenamer.Dynamic.Extensions
{
    public class MetadataRename
    {
        public static void RenameFilesUsingMetadata(dynamic items, List<(string title, string[] artists)> metadata)
        {
            char[] invalidPathChars = Path.GetInvalidFileNameChars();
            int index = 0;
            foreach (var folderItem in items)
            {
                if (metadata[index].title != null)
                    try
                    {
                        string path = Shell.Path(folderItem);
                        string directoryPath = Path.GetDirectoryName(path);
                        string extension = Path.GetExtension(path);

                        File.Move(path,
                            Path.Combine(directoryPath ?? throw new IOException($"{nameof(directoryPath)} was null."),
                                new string(
                                    $"{String.Join(", ", metadata[index].artists)} - {metadata[index].title}{extension}"
                                        .Where(c => !invalidPathChars.Contains(c)).ToArray())));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                index++;
            }
        }

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