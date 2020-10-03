using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CommandClassInterface;
using Console;

namespace Rename.Helpers
{
    public class SkipFile : ICommandClass
    {
        private readonly ISilenceAbleConsole _silenceAbleConsole;

        public IEnumerable<string> CommandsForJson { get; }
        
        public string GetHelpFor(string command)
        {
            return Rename_Helpers_Commands.ResourceManager.GetString($"{nameof(SkipFile)}_{command}Help");
        }

        public SkipFile(ISilenceAbleConsole silenceAbleConsole)
        {
            _silenceAbleConsole = silenceAbleConsole ?? throw new ArgumentNullException(nameof(silenceAbleConsole));
            CommandsForJson = new[] {nameof(Select)};
        }

        public string SelectedPath { get; private set; } = "skip.txt";

        /// <summary>
        /// Select new path for the skip file.
        /// </summary>
        /// <param name="selectedPath">New skip file's path.</param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Select(string[] selectedPath)
        {
            // Return in case of null or empty selectedPath
            if (selectedPath == null || selectedPath.Length == 0)
                return;
            
            // Perform desired change
            _silenceAbleConsole.WriteLine($"Old skip file: {SelectedPath}");
            SelectedPath = selectedPath.First();
            _silenceAbleConsole.WriteLine($"New skip file: {SelectedPath}");
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

        public override string ToString()
        {
            return nameof(SkipFile);
        }
    }
}