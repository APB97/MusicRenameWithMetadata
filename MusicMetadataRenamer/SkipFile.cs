using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace MusicMetadataRenamer
{
    public class SkipFile
    {
        public string SelectedPath { get; private set; } = "skip.txt";

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Select(string[] selectedPath)
        {
            SelectedPath = selectedPath.FirstOrDefault();
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
                SelectedPath = file;
                break;
            }
        }
    }
}