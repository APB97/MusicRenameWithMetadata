﻿using System;
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
        public static async Task<HashSet<string>> GetCommonWordsFromAsync(string file)
        {
            return await Task.Run(() => GetCommonWordsFrom(file));
        }

        /// <summary>
        /// Get Set of common words from file using IgnoreCaseComparer.
        /// </summary>
        /// <param name="file">File to read words from.</param>
        /// <returns>Set of words compared using IgnoreCaseComparer.</returns>
        public static HashSet<string> GetCommonWordsFrom(string file)
        {
            return new HashSet<string>(File.Exists(file) ? File.ReadAllLines(file) : Array.Empty<string>(), new IgnoreCaseComparer());
        }
    }
}