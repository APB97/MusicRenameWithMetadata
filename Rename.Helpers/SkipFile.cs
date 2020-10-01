using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Console;

namespace Rename.Helpers
{
    public class SkipFile
    {
        private readonly IConsole _console;

        public SkipFile(IConsole console)
        {
            _console = console;
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
            _console.WriteLine($"Old skip file: {SelectedPath}");
            SelectedPath = selectedPath.First();
            _console.WriteLine($"New skip file: {SelectedPath}");
        }

        /// <summary>
        /// Perform interactive selection
        /// </summary>
        public void Prompt()
        {
            System.Console.WriteLine("Enter the skip file path to use or press Enter to use the default:");
            while (true)
            {
                string file = System.Console.ReadLine();
                if (string.IsNullOrEmpty(file))
                    break;
                if (!File.Exists(file))
                    continue;
                Select(new [] { file });
                System.Console.WriteLine("Press Enter once more to confirm or enter different path:");
            }
        }
    }
}