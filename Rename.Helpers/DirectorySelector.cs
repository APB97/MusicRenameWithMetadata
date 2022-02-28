using System.Collections.Generic;
using System.IO;
using System.Linq;
using Console;

namespace Rename.Helpers
{
    /// <summary>
    /// SelectorBase implementation for selecting directories.
    /// </summary>
    public class DirectorySelector : SelectorBase, ISilenceAble
    {
        /// <inheritdoc />
        /// Additionally assigns value to CommandsForJson
        public DirectorySelector(ISilenceAbleConsole silenceAbleConsole) : base(silenceAbleConsole)
        {
            CommandsForJson = new[] {nameof(Add)};
        }

        /// <inheritdoc />
        public override IEnumerable<string> CommandsForJson { get; }

        /// <inheritdoc />
        protected override HashSet<string> Commands { get; } = new HashSet<string>(
        new []
        {
            nameof(Add),
            nameof(BeSilent),
            nameof(Clear),
            nameof(Complete),
            nameof(ClearScreen),
            nameof(DontBeSilent),
            nameof(Help),
            nameof(List),
            nameof(Remove)
        });

        /// <summary>
        /// Clear set of selected directories.
        /// </summary>
        public void Clear()
        {
            Directories.Clear();
            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.Messages_Directory_list_cleared);
        }

        /// <summary>
        /// Set of selected directories.
        /// </summary>
        public HashSet<string> Directories { get; } = new HashSet<string>();

        /// <summary>
        /// Add directories to the set.
        /// </summary>
        /// <param name="dirs">Directories to add.</param>
        public void Add(params string[] dirs)
        {
            if (!dirs.All(Directory.Exists))
            {
                string joined = string.Join(" ", dirs);
                int index = 0;
                while (index < joined.Length)
                {
                    // Find next 2 quotation marks 
                    index = joined.IndexOf('\"', index);
                    int nextIndex = joined.IndexOf('\"', index + 1);
                    if (index < 0 || nextIndex < 0)
                        break;
                    // Grab directory within quotation marks
                    string dirBetween = joined.Substring(index + 1, nextIndex - index - 1);
                    // Remove directory and quotation marks from string
                    joined = joined.Remove(index, nextIndex - index + 1);
                    // Set index to last found quotation mark
                    index = nextIndex;
                    // Add Directory to list if it exists
                    if (Directory.Exists(dirBetween))
                        Directories.Add(dirBetween);
                }

                foreach (string dirPath in joined.Split(' '))
                    if (Directory.Exists(dirPath))
                        Directories.Add(dirPath);
                
            }
            else
            {
                foreach (string dir in dirs)
                    if (Directory.Exists(dir))
                        Directories.Add(dir);
            }

            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.Messages_Directories_added);
        }

        /// <summary>
        /// Displays set of selected directories.
        /// </summary>
        public void List()
        {
            foreach (string directory in Directories)
            {
                SilenceAbleConsole.WriteLine(directory);
            }
        }

        /// <summary>
        /// Removes given directories from selection.
        /// </summary>
        /// <param name="dirs"></param>
        public void Remove(params string[] dirs)
        {
            foreach (string directory in dirs)
            {
                Directories.Remove(directory);
            }
            
            SilenceAbleConsole.WriteLine(Rename_Helpers_Commands.Messages_Directories_removed);
        }

        /// <inheritdoc />
        public void BeSilent()
        {
            SilenceAbleConsole.BeSilent();
        }

        /// <inheritdoc />
        public void DontBeSilent()
        {
            SilenceAbleConsole.DontBeSilent();
        }
    }
}