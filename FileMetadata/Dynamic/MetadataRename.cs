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
    /// <summary>
    /// Class used to rename Single or Multiple files using file's property values.
    /// </summary>
    public class MetadataRename
    {
        private readonly IConsole _silenceAbleConsole;
        private readonly string _separator;
        
        /// <summary>
        /// Create new instance of MetadataRename for use when renaming files
        /// </summary>
        /// <param name="silenceAbleConsole">Console instance</param>
        /// <param name="separator">Separator used when joining multiple properties</param>
        public MetadataRename(IConsole silenceAbleConsole, string separator = null)
        {
            _silenceAbleConsole = silenceAbleConsole ?? throw new ArgumentNullException(nameof(silenceAbleConsole));
            _separator = separator ?? " - ";
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
                TryRenaming(filePath, processor, names);
        }

        private void TryRenaming(string filePath, IStringProcessor processor, IEnumerable<string> propertyNames)
        {
            try
            {
                RenameSingle(filePath, processor, propertyNames);
            }
            catch (Exception e)
            {
                _silenceAbleConsole.WriteLine(e);
            }
        }

        private void RenameSingle(string filePath, IStringProcessor processor, IEnumerable<string> propertyNames)
        {
            string[] propertyValues = GetPropertyValuesOf(filePath, propertyNames as string[] ?? propertyNames.ToArray());
            var destFileName = GetDestinationFileName(filePath, processor, propertyValues);
            MoveToDestinationIfDoesNotExist(filePath, destFileName);
        }

        private static string[] GetPropertyValuesOf(string filePath, IEnumerable<string> propertyNames)
        {
            return propertyNames.Select(name => GetPropertyValueOf(filePath, name)).ToArray();
        }

        private static string GetPropertyValueOf(string filePath, string propertyName)
        {
            MethodInfo method =
                typeof(Mp3InfoReader).GetMethod(propertyName, BindingFlags.Static | BindingFlags.Public);
            object[] parameters = {filePath};
            return method?.Invoke(null, parameters)?.ToString();
        }

        private string GetDestinationFileName(string filePath, IStringProcessor processor, string[] propertyValues)
        {
            string directoryName = Path.GetDirectoryName(filePath);
            if (directoryName == null)
                throw new InvalidOperationException($"Directory of {filePath} is incorrect.");
            
            string extension = Path.GetExtension(filePath);
            string withNoInvalid = propertyValues.JoinForFilePath(_separator);
            return Path.Combine(directoryName, $"{processor.Process(withNoInvalid)}{extension}");
        }

        private static void MoveToDestinationIfDoesNotExist(string filePath, string destFileName)
        {
            if (!File.Exists(destFileName))
                File.Move(filePath, destFileName);
        }
    }
}