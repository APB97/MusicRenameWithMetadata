using System;
using System.Collections.Generic;
using System.IO;
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
        public void RenameMultiple(IEnumerable<string> filePaths, IStringProcessor processor)
        {
            foreach (string filePath in filePaths)
            {
                RenameSingle(filePath, processor);
            }
        }
        
        public void RenameSingle(string filePath, IStringProcessor processor)
        {
            string extension = Path.GetExtension(filePath);
            string unprocessed = Mp3InfoReader.TitleOf(filePath);
            string withNoInvalid = new[] {unprocessed}.JoinForFilePath();
            File.Move(filePath,
                Path.Combine(Path.GetDirectoryName(filePath) ?? throw new Exception("path is not correct."),
                    $"{processor.Process(withNoInvalid)}{extension}"));
        }
    }
}