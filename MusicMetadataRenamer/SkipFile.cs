using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Rename.Helpers;

namespace MusicMetadataRenamer
{
    public class SkipFile
    {
        private readonly ConsoleWrapper _console;

        public SkipFile(ConsoleWrapper console)
        {
            _console = console;
        }

        public string SelectedPath { get; private set; } = "skip.txt";

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Select(string[] selectedPath)
        {
            if (selectedPath == null || selectedPath.Length == 0)
                return;
            
            _console.WriteLine($"Old skip file: {SelectedPath}");
            SelectedPath = selectedPath.First();
            _console.WriteLine($"New skip file: {SelectedPath}");
        }

        public void Prompt()
        {
            Console.WriteLine("Enter the skip file path to use or press Enter to use the default:");
            while (true)
            {
                string file = Console.ReadLine();
                if (string.IsNullOrEmpty(file))
                    break;
                if (!File.Exists(file))
                    continue;
                Select(new [] { file });
                break;
            }
        }
    }
}