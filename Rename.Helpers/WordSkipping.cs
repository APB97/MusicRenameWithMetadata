using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rename.Helpers
{
    /// <summary>
    /// Static class for reading CommonWords from file.
    /// </summary>
    public static class WordSkipping
    {
        /// <summary>
        /// Get Set of common words from file using IgnoreCaseComparer.
        /// </summary>
        /// <param name="file">File to read words from.</param>
        /// <returns>Set of words compared using IgnoreCaseComparer.</returns>
        public static async Task<HashSet<string>> GetCommonWordsFrom(string file)
        {
            return await Task.Run(() => new HashSet<string>(File.Exists(file) ? File.ReadAllLines(file) : new string[0], new IgnoreCaseComparer()));
        }
    }
}