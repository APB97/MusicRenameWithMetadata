using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Console;
using FileMetadata.Extensions;
using FileMetadata.Mp3;
using StringProcessor;

namespace FileMetadata.Dynamic
{
    public class MetadataRename
    {
        private readonly IConsole _console;

        public MetadataRename(IConsole console)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
        }

        /// <summary>
        /// Renames multiple files
        /// </summary>
        /// <param name="filePaths">Collection of file paths</param>
        /// <param name="processor">String processor instance</param>
        /// <param name="propertyNames"></param>
        public void RenameMultiple(IEnumerable<string> filePaths, IStringProcessor processor,
            IEnumerable<string> propertyNames = null)
        {
            IEnumerable<string> names = propertyNames as string[] ?? propertyNames?.ToArray() ?? new []{"Title"};
            foreach (string filePath in filePaths)
            {
                try
                {
                    RenameSingle(filePath, processor, names);
                }
                catch (Exception e)
                {
                    _console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public void RenameSingle(string filePath, IStringProcessor processor, IEnumerable<string> propertyNames)
        {
            string extension = Path.GetExtension(filePath);

            IEnumerable<string> names = propertyNames as string[] ?? propertyNames.ToArray();
            string[] propertyValues = new string[names.Count()];
            for (int i = 0; i < names.Count(); i++)
            {
                propertyValues[i] = typeof(Mp3InfoReader).GetMethod(names.ElementAt(i), BindingFlags.Static| BindingFlags.Public)
                    ?.Invoke(null, new object[] {filePath})?.ToString();
            }
            string withNoInvalid = propertyValues.JoinForFilePath();
            string destFileName = Path.Combine(Path.GetDirectoryName(filePath) ?? throw new Exception("path is not correct."),
                $"{processor.Process(withNoInvalid)}{extension}");
            if (!File.Exists(destFileName))
                File.Move(filePath, destFileName);
        }
    }
}