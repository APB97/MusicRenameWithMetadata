using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandClassInterface;
using Console;

namespace Rename.Helpers
{
    /// <summary>
    /// Class allowing to select a SkipFile
    /// </summary>
    public class SkipFile : ICommandClass
    {
        private readonly IConsole _silenceAbleConsole;

        /// <inheritdoc />
        public IEnumerable<string> CommandsForJson { get; }

        /// <inheritdoc />
        public string GetHelpFor(string command)
        {
            return Rename_Helpers_Commands.ResourceManager.GetString($"{nameof(SkipFile)}_{command}Help");
        }

        /// <summary>
        /// Create instance of SkipFile with IConsole dependency for output.
        /// </summary>
        /// <param name="silenceAbleConsole">Console to use for output.</param>
        public SkipFile(IConsole silenceAbleConsole)
        {
            _silenceAbleConsole = silenceAbleConsole ?? throw new ArgumentNullException(nameof(silenceAbleConsole));
            CommandsForJson = new[] {nameof(Select)};
        }

        /// <summary>
        /// Path to selected SkipFile
        /// </summary>
        public string SelectedPath { get; private set; } = "skip.txt";

        /// <summary>
        /// Select new path for the skip file.
        /// </summary>
        /// <param name="selectedPath">New skip file's path.</param>
        public void Select(string[] selectedPath)
        {
            // Return in case of null or empty selectedPath
            if (selectedPath == null || selectedPath.Length == 0)
                return;
            
            // Perform desired change
            _silenceAbleConsole.WriteLine(string.Format(Rename_Helpers_Commands.SkipFile_Old_skip_file_format, SelectedPath));
            SelectedPath = selectedPath.First();
            _silenceAbleConsole.WriteLine(string.Format(Rename_Helpers_Commands.SkipFile_New_skip_file_format, SelectedPath));
        }

        /// <summary>
        /// Perform interactive selection
        /// </summary>
        public void Prompt()
        {
            System.Console.WriteLine(Rename_Helpers_Commands.SkipFile_Prompt_Enter_the_skip_file);
            while (true)
            {
                string file = System.Console.ReadLine();
                if (string.IsNullOrEmpty(file))
                    break;
                if (!File.Exists(file))
                    continue;
                Select(new [] { file });
                System.Console.WriteLine(Rename_Helpers_Commands.SkipFile_Prompt_Press_Enter_once_more);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return nameof(SkipFile);
        }
    }
}